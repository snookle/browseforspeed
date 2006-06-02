// Copyright (C) 2006 Richard Nelson, Ben Kenny, Philip Nelson
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

// project created on 21/05/2006 at 8:20 A

using System;
using System.Collections.Generic;
using System.Threading;

namespace LFS.BrowseForSpeed.Network
{
    public class QueryThreadManager
    {
        public QueryThreadManager()
        {
            _hostQueue = new Queue<HostInfo>();
            _queryThreads = new List<Thread>(MasterServerQuery.MaximumQueryThreads);
            _mainQueryThread = new Thread(new ThreadStart(Run));
            _mainQueryThread.IsBackground = true;
            _mainQueryThread.Start();
        }

        #region Public Methods
        public void Add(HostInfo host)
        {
            _hostQueue.Enqueue(host);
        }

        public void Close()
        {
            try
            {
                Terminate();
            }
            finally
            {
                _mainQueryThread.Join();
            }
        }

        public event EventHandler<ServerInformationEventArgs> HostQueried;
        #endregion

        #region Private Methods
        private void Run()
        {
            while (MasterServerQuery.Instance.ContinueProcessing)
            {
                Thread.Sleep(5);

                if (_hostQueue.Count <= 0)
                    continue;

                if (MasterServerQuery.Instance.UseQueryDelay)
                    Thread.Sleep(MasterServerQuery.Instance.QueryDelayMilliseconds);

                int threadSlot = -1;
                for (int count = 0; count < _queryThreads.Count; count++)
                {
                    if ((_queryThreads[count].ThreadState == ThreadState.Unstarted) ||
                        (_queryThreads[count].ThreadState == ThreadState.Stopped))
                    {
                        threadSlot = count;
                        break;
                    }
                }

                MasterServerQueryReader reader = new MasterServerQueryReader(_hostQueue.Dequeue());
                reader.HostQueried += new EventHandler<ServerInformationEventArgs>(MasterServerQueryReaderHostQueried);

                Thread query = new Thread(new ThreadStart(reader.Perform));
                query.IsBackground = true;
                query.Start();

                if (threadSlot > -1)
                {
                    // If we found a slot with a previous thread, release it first.
                    _queryThreads[threadSlot] = null;
                    _queryThreads[threadSlot] = query;
                }
                else
                    _queryThreads.Add(query);
            }

            if (!MasterServerQuery.Instance.ContinueProcessing)
            {
                _hostQueue.Clear();

                // Might have been finished by MasterServerQuery.ContinueProcessing. 
                Terminate();
            }
        }

        private void Terminate()
        {
            // Need to join left over threads.
            for (int count = 0; count < _queryThreads.Count; count++)
                if (_queryThreads[count].ThreadState != ThreadState.Stopped)
                    _queryThreads[count].Join();
        }
        #endregion

        #region Private Event Handlers
        private void MasterServerQueryReaderHostQueried(object sender, ServerInformationEventArgs args)
        {
            if (HostQueried != null)
                HostQueried(this, args);
        }
        #endregion

        #region Fields
        private List<Thread> _queryThreads;
        private Queue<HostInfo> _hostQueue;
        private Thread _mainQueryThread;
        #endregion
    }
}
