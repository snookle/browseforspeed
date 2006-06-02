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

using System;
using System.Drawing;
using System.Collections;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Microsoft.Win32;

using LFS.BrowseForSpeed.Network;

namespace LFS.BrowseForSpeed.Windows.Forms 
{
	public partial class ServerInformationForm
	{
		public void RefreshPlayerList()
		{
			SetControlProperty(buttonInfoJoin, "Enabled", false);
			SetControlProperty(buttonInfoRefresh, "Enabled", false);
			listPlayers.Items.Clear();
            int i = LFSWorldQuery.FetchPublicStatsInfo(ref _info);
			if (_exiting) 
                return;
			if (i == 1) { //MasterServerQuery.getPubStatInfo(ref info)) {
				labelPrivate.Text = _info.Passworded ? "Yes" : "No";
                if (_info.Players > 0)
                {
                    foreach (string player in _info.RacerNames)
                    {
						listPlayers.Items.Add(player);
					}
				} else {
					listPlayers.Items.Add("No players currently on the server.");
				}
			} else {
				if (i == 0) {
					listPlayers.Items.Add("Couldn't find server on pubstat!");
				} else if (i == -1) {
					listPlayers.Items.Add("Error querying pubstat!");
				}
				labelPrivate.Text = "N/A";				
			}
			SetControlProperty(buttonInfoRefresh, "Enabled", true);
			SetControlProperty(buttonInfoJoin, "Enabled", true);
			SetControlProperty(buttonInfoRefresh, "Text", "&Refresh");
        }

        #region Public Properties
        public ServerInformation Info
        {
            get { return _info; }
            set
            {
                if (value == null) 
                    return;

                _info = value;
                textInfoPassword.Text = _info.Password;
            }
		}
        #endregion

        public ServerInformationForm(MainForm m) 
        {
			_main = m;
			_exiting = false;
			InitializeComponent();
        }

        #region Protected Methods
        protected override void OnLoad(EventArgs e)
        {
 	        base.OnLoad(e);

            CenterToParent();
            _exiting = false;

            //MasterServerQuery.queried += new ServerQueried(QueryCallback);
            //MasterServerQuery.HostQueried += new EventHandler<ServerInformationEventArgs>(QueryCallback);
            RefreshButtonClick(this, e);
        }
        
        //protected void QueryCallback(object sender, ServerInformation info, object callback)
        protected void QueryCallback(object sender, ServerInformationEventArgs args)
        {
            //if ((int)callback != 3) 
              //  return;

            if ((args != null) && (args.Server != null))
                RefreshServer(args.Server);
        }
        #endregion

        #region Private Event Handlers
        private void ButtonInfoCloseClick(object sender, System.EventArgs e) 
        {
			labelPrivate.Text = "Dunno yet LOL";
			_exiting = true;
			buttonInfoRefresh.Enabled = true;
			buttonInfoJoin.Enabled = true;
			buttonInfoRefresh.Text = "&Refresh";
			
            Close();
		}

        private void RefreshButtonClick(object sender, System.EventArgs e)
        {
            Thread t;
            if (buttonInfoRefresh.Text == "&Refresh")
            {
                t = new Thread(new ThreadStart(MakeMainQuery));
                t.Start();
            }
            else
                MasterServerQuery.Instance.Stop();
        }

        private void TextInfoPasswordLeave(object sender, System.EventArgs e)
        {
            _info.Password = textInfoPassword.Text;
        }

        private void ContextMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            btnAddFriend.Enabled = ((listPlayers.SelectedIndex != -1) && (listPlayers.Items[listPlayers.SelectedIndex].ToString() != "No players currently on the server."));
        }

        private void BtnAddFriendClick(object sender, System.EventArgs e)
        {
            if (listPlayers.SelectedIndex == -1)
                return;

            _main.AddFriend(listPlayers.Items[listPlayers.SelectedIndex].ToString(), true);
        }
        #endregion

        #region Private Methods
        void MakeMainQuery()
        {
            SetControlProperty(buttonInfoRefresh, "Text", "&Stop");
            SetControlProperty(buttonInfoJoin, "Enabled", false);
            try
            {
                IPEndPoint[] server = new IPEndPoint[1];
                server[0] = _info.Host;
                MasterServerQuery.Instance.Perform(0, 0, server, QueryType.Main);
                Thread t = new Thread(new ThreadStart(RefreshPlayerList));
                t.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " - " + e.StackTrace, "", MessageBoxButtons.OK);
            }
        }

        private void RefreshServer(ServerInformation info)
        {
            try
            {
                if (info.Success)
                {
                    _info = info;
                    _info.Hostname = Utility.RemoveColorCodes(_info.Hostname);
                    labelServerName.Text = _info.Hostname;
                    labelCars.Text = _main.CarsToString(_info.Cars);
                    labelInfo.Text = _main.RulesToString(_info.Rules);
                    labelPing.Text = _info.Ping.ToString();
                    labelTrack.Text = _info.Track;
                }
                else
                {
                    labelServerName.Text = "Query timed out";
                    labelPing.Text = "9999";
                    labelCars.Text = "N/A";
                    labelTrack.Text = "N/A";
                }
            }
            catch (Exception e) { }
        }
        #endregion

        delegate void SetValueDelegate(Object obj, Object val, Object[] index);
		
		public void SetControlProperty(Control ctrl, String propName, Object val) {
			System.Reflection.PropertyInfo propInfo = ctrl.GetType().GetProperty(propName);
            Delegate dgtSetValue = new SetValueDelegate(propInfo.SetValue);
      		ctrl.Invoke(dgtSetValue, new Object[3] { ctrl, val, /*index*/null });
		}
				
		delegate void RefreshServerDelegate(ServerInformation info);

		#region Fields
        private ServerInformation _info;
        private bool _exiting;
        private MainForm _main;
        #endregion
    }
}
