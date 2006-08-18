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
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using libbrowseforspeed;

namespace BrowseForSpeed.Frontend
{
	public enum FriendStatus{Online = 1, Offline = 0, Error = -1}
	
	public class FriendListItem
	{
		public ServerInformation server;
		public FriendStatus status;
		public string name;
		public FriendListItem(string name)
		{
			this.name = name;
		}
	}
	

	public class FriendListView : ListView
	{
		private Dictionary<string, FriendListItem> friends = new Dictionary<string, FriendListItem>();
		private string filename = Application.StartupPath + "\\friends.xml";
		
		protected override bool DoubleBuffered {
			get {
				return base.DoubleBuffered;
			}
			set {
				base.DoubleBuffered = value;
			}
		}
		
		private bool hideOffline;
		public bool HideOffline {
			get {
				return hideOffline;
			}
			set {
				hideOffline = value;
				DisplayAll();
			}
		}
		
		public FriendListView()
		{
			this.DoubleBuffered = true;
		}
		
		public bool AddFriend(string name)
		{
			if (!friends.ContainsKey(name)) {
				friends.Add(name, new FriendListItem(name));
				return true;
			} else {
				//friend already existed.
				return false;
			}
		}
		
		public FriendListItem GetFriend(string name)
		{
			FriendListItem friend;
			return friends.TryGetValue(name, out friend) ? friend : null;
		}
		
		public void DisplayAll()
		{
			new Thread(new ThreadStart(DisplayAllT)).Start();
		}
		
		private void DisplayAllT()
		{
			List<ListViewItem> items = new List<ListViewItem>();
			foreach(FriendListItem f in friends.Values){
				ListViewItem item = BuildListItem(f);
				if (item == null) continue;
				if (f.status == FriendStatus.Online)
					items.Insert(0, item);
				else
					items.Add(item);
			}
			Items.Clear();
			if (items.Count == 0) {
				Items.Add("No Friends Online");
			} else {
				Items.AddRange(items.ToArray());
			}
		}
		
		private ListViewItem BuildListItem(FriendListItem friend)
		{
			ListViewItem lvi = new ListViewItem();
			try {
			RefreshFriend(ref friend);

			if (friend.status != FriendStatus.Online && hideOffline)
				return null;
			if (friend.status == FriendStatus.Offline) { //offline
				lvi.SubItems.Insert(0, new ListViewItem.ListViewSubItem(lvi, friend.name));
				lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, MainForm.languages.GetString("MainForm.Offline")));
				lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, "N/A"));
				lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, "N/A"));
			} else if (friend.status == FriendStatus.Online) {
				lvi.SubItems.Insert(0, new ListViewItem.ListViewSubItem(lvi, friend.name));
				lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, LFSQuery.removeColourCodes(friend.server.hostname)));
				lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, (friend.server.passworded ? MainForm.languages.GetString("Global.Yes") : MainForm.languages.GetString("Global.No"))));
				String players = "";
				foreach (string player in friend.server.racerNames) {
					players += player + ", ";
				}
				players = players.Remove(players.Length - 2, 2).ToString();
				if (friend.server.racerNames == null)
					lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, "null racerNames"));

				lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, players));
			} else {
				lvi.SubItems.Insert(0, new ListViewItem.ListViewSubItem(lvi, friend.name));
				lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, MainForm.languages.GetString("ServerInformationForm.PubstatError")));
	            lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, MainForm.languages.GetString("Global.Error")));
				lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, MainForm.languages.GetString("Global.Error")));
			}
				lvi.Tag = (string)friend.name;
				return lvi;
			} catch (Exception ex) {
				lvi.SubItems.Insert(0, new ListViewItem.ListViewSubItem(lvi, friend.name));
				lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, MainForm.languages.GetString("Global.Error")));
	            lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, ex.Message));
				lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, ex.StackTrace));
				return lvi;
			}
		}
				
		public void DisplayFriend(string name)
		{
			FriendListItem friend;
			friends.TryGetValue(name, out friend);
			DisplayFriend(friend);
		}
		
		private void RefreshFriend(ref FriendListItem friend)
		{
			friend.status = (FriendStatus)LFSQuery.getPubStatInfo(friend.name, out friend.server);
		}
		
		public FriendListItem GetSelectedFriend()
		{
			if (SelectedItems.Count > 0) {
				FriendListItem friend;
				friends.TryGetValue(SelectedItems[0].Text, out friend);
				return friend;
			}
			else
				return null;
		}
		
		public void RemoveFriend(FriendListItem friend)
		{
			friends.Remove(friend.name);
			Items.RemoveByKey(friend.name);
			DisplayAll();
		}
		
		public void DisplayFriend(FriendListItem friend)
		{
			ListViewItem i = BuildListItem(friend);
			if (i != null)
				Items.Add(i);
		}

		public void Save()
		{
			try {
				XmlTextWriter tw = new XmlTextWriter(filename, null);
				tw.Formatting = Formatting.Indented;
				tw.WriteStartDocument();
				tw.WriteStartElement("friends");
				tw.WriteAttributeString("version", "2");
				foreach (FriendListItem friend in friends.Values) {
					tw.WriteStartElement("friend");
					tw.WriteAttributeString("license", friend.name);
					tw.WriteEndElement();
				}
				tw.WriteFullEndElement();
				tw.Close();
			} catch (Exception ex) {
				string message = String.Format(MainForm.languages.GetString("SaveFriendsError"), ex.Message);
				MessageBox.Show(message, MainForm.languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}
		
		public void Load()
		{
			try{
				XmlDocument doc = new XmlDocument();
				doc.Load(filename);
				XmlNodeList list = doc.GetElementsByTagName("friends");
				String docVersion = ((XmlElement)list[0]).GetAttribute("version");
				list = ((XmlElement)list[0]).GetElementsByTagName("friend");
				foreach (XmlElement friend in list) {
					if (docVersion == "2")
						AddFriend(friend.GetAttribute("license"));
					else
						AddFriend(friend.GetAttribute("name"));
				}
			} catch (FileNotFoundException fnfe){
			} catch (Exception e) {
				File.Copy(filename, filename +".backup", true);
				string message = String.Format(MainForm.languages.GetString("LoadFriendsError"), e.Message, filename);
				MessageBox.Show(message, MainForm.languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			Sort();
		}
	}
	
}
