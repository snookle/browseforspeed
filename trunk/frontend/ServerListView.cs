﻿// Copyright (C) 2006 Richard Nelson, Ben Kenny, Philip Nelson
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
using System.Windows.Forms;
using System.Collections.Generic;
using libbrowseforspeed;
using System.Xml;
using System.Net;

namespace BrowseForSpeed.Frontend
{
	public enum FilterType{None, Track, Ping, Cars, Empty, Full, Private, Public};
	public struct Filter {
		public FilterType type;
		public Object value;
		public Filter(Object v){
			type = FilterType.None;
			value = null;
		}
	}

	
	public class ServerListView : ListView
	{
		protected List<ServerListItem> serverList = new List<ServerListItem>();
		protected ListSorter sorter = new ListSorter();
		protected override bool DoubleBuffered {
			get {
				return base.DoubleBuffered;
			}
			set {
				base.DoubleBuffered = value;
			}
		} 
		
		public ServerListView() {
			this.DoubleBuffered = true;
		}
		
		public void AddServer(ServerInformation info)
		{
			ServerListItem item = new ServerListItem(info);
			//iterate through the list, and see if we have a match.
			for (int i = 0; i < serverList.Count; ++i){
				if (serverList[i].host == item.host) {
					serverList[i] = item;
					Display(item).Tag = i;
					return;
				}
			}
			item.filtered = false;
			serverList.Add(item);
			if (this is MainListView)
				FilterServer(item);
			Display(item);
			this.Sort();
		}
		public ServerListItem GetSelectedServer()
		{
			if (SelectedItems.Count > 0) {
				int index = (int)SelectedItems[0].Tag;
				if (index == -1)
					return null;
				ServerListItem item = serverList[index];
				item.index = SelectedItems[0].Index;
				return item;
			}
			else
				return null;
		}

		public ServerListItem getServer(string hostname) {
			foreach (ServerListItem s in serverList) {
				if (s.hostname == hostname)
					return s;
			}
			return null;
		}
		public void RemoveServer(ServerListItem item)
		{
			Items.RemoveAt(item.index);
			serverList.Remove(item);
			for (int i = 0; i < this.Items.Count; ++i){
				if ((int)Items[i].Tag > item.index){
					int tmp = (int)(Items[i].Tag); Items[i].Tag = --tmp;
				}
			}
		}
		public void ClearServers()
		{
			serverList.Clear();
			serverList.TrimExcess();
			this.Items.Clear();
		}
		public void DisplayAll()
		{
			Items.Clear();
			foreach(ServerListItem item in serverList.FindAll(ServersNotFiltered)){
				Display(item);
			}
			Sort();

		}
		public List<ServerListItem> AllServers()
		{
			return serverList;
		}
		private static bool ServersNotFiltered(ServerListItem info)
		{
			return !info.filtered;
		}

		public ListViewItem Display(ServerListItem item)
		{
			if (item.filtered)
				return new ListViewItem();
			try {
				ListViewItem lvi;
				lvi = this.Items.Add(item.host.ToString());
				lvi.Tag = serverList.IndexOf(item);
				//item.hostname = LFSQuery.removeColourCodes(item.hostname);
				lvi.SubItems.Insert(0, new ListViewItem.ListViewSubItem(lvi, item.hostname));
				string cars = MainForm.CarsToString(LFSQuery.getCarNames(item.cars));
				string rules = MainForm.RulesToString(item.rules);
				lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, item.ping.ToString()));
				if (this is FavouriteListView){
					lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, MainForm.VersionToString(item.version)));
				} else {
					lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, item.passworded == true ? MainForm.languages.GetString("Global.Yes") : MainForm.languages.GetString("Global.No")));
				}
				lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, item.players.ToString() +"/" + item.slots.ToString()));
				lvi.SubItems.Insert(4, new ListViewItem.ListViewSubItem(lvi, rules));
				lvi.SubItems.Insert(5, new ListViewItem.ListViewSubItem(lvi, item.track));
				lvi.SubItems.Insert(6, new ListViewItem.ListViewSubItem(lvi, cars));
				return lvi;
			}catch (Exception ex) {
				return null;
			}
		}
		private Filter[] filters = new Filter[10];
		public void Filter(FilterType filter, Object value)
		{
			filters[(int)filter].type = filter;
			filters[(int)filter].value = value;
			foreach(ServerListItem item in serverList){
				FilterServer(item);
			}
		}
		private void FilterServer(ServerListItem item)
		{
			item.filtered = false;
			foreach (Filter f in filters){
				if (f.type == FilterType.None)
					continue;
				switch (f.type){
						case FilterType.Track: item.filtered = item.filtered || !(item.track.Contains((string)f.value)); break;
						case FilterType.Empty: item.filtered = (item.filtered || ((item.players == 0) && !(bool)f.value)); break;
						case FilterType.Full : item.filtered = (item.filtered || ((item.players == item.slots) && !(bool)f.value)); break;
						case FilterType.Private : item.filtered = (item.filtered || ((item.passworded == true) && !(bool)f.value)); break;
						case FilterType.Public : item.filtered = (item.filtered || ((item.passworded == false) && !(bool)f.value)); break;
						case FilterType.Ping : item.filtered = item.filtered || (item.ping > (int)f.value); break;
						case FilterType.Cars :
							if (item.filtered) break;
							foreach (CheckBox car in (CheckBox[])(f.value)) {
								ulong i = LFSQuery.CAR_BITS[(int)car.Tag] & item.cars;
								if ((i != 0 && car.CheckState == CheckState.Unchecked) ||
								    (i == 0 && car.CheckState == CheckState.Checked)) {
									item.filtered = true;
								}
							}
							break;
				}
			}
		}
	}
	
	public class FavouriteListView : ServerListView
	{
		private String Filename = Application.StartupPath + "\\favourite.servers";
		private String XMLFilename = Application.StartupPath + "\\favourites.xml";

		public ServerInformation[] GetAllHosts()
		{
			return serverList.ToArray();
		}
		
		public void Save() {
			try {
				XmlTextWriter tw = new XmlTextWriter(XMLFilename, null);
				tw.Formatting = Formatting.Indented;
				tw.WriteStartDocument();
				tw.WriteStartElement("favourites");
				tw.WriteAttributeString("version", "2");
				foreach (ServerListItem info in AllServers()){
					tw.WriteStartElement("favourite");
					tw.WriteElementString("version", MainForm.VersionToString(info.version));
					tw.WriteElementString("ip", info.host.Address.ToString());
					tw.WriteElementString("port", info.host.Port.ToString());
					tw.WriteElementString("name", info.hostname);
					tw.WriteElementString("password", info.password);
					tw.WriteElementString("insimPort", info.insimPort.ToString());
					tw.WriteElementString("adminPassword", info.adminPassword);
					tw.WriteEndElement();
				}
				tw.WriteFullEndElement();
    			tw.Close();
			}
    		catch (Exception ex) {
				string message = String.Format(MainForm.languages.GetString("SaveFavError"), ex.Message);
				MessageBox.Show(message, MainForm.languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		public bool LoadXML() {
			try {
				XmlDocument doc = new XmlDocument();
				doc.Load(XMLFilename);
				XmlNodeList list = doc.GetElementsByTagName("favourites");
				String docVersion = ((XmlElement)list[0]).GetAttribute("version");
				list = ((XmlElement)list[0]).GetElementsByTagName("favourite");
				foreach (XmlElement favourite in list) {
					try {
						ServerInformation info = new ServerInformation();
						if (docVersion == "2") {
							info.version = MainForm.StringToVersion(favourite.GetElementsByTagName("version")[0].FirstChild.Value);
						} else {
							info.version = LFSQuery.VERSION_S2;
						}
						info.host = new IPEndPoint(IPAddress.Parse(favourite.GetElementsByTagName("ip")[0].FirstChild.Value), Convert.ToInt32(favourite.GetElementsByTagName("port")[0].FirstChild.Value));
						info.hostname = favourite.GetElementsByTagName("name")[0].FirstChild.Value;
						try {
							info.adminPassword = favourite.GetElementsByTagName("adminPassword")[0].FirstChild.Value;
							info.insimPort = Int32.Parse(favourite.GetElementsByTagName("insimPort")[0].FirstChild.Value);
						} catch (Exception e) {
							info.adminPassword = "";
							info.insimPort = 29999;
						}
						try {
							info.password = favourite.GetElementsByTagName("password")[0].FirstChild.Value;
						} catch (Exception e) {
							info.password = "";
						}
						AddServer(info);
					} catch (Exception e) { }
				}
				return true;
			} catch (FileNotFoundException fnfe) {
				return false;
			} catch (Exception e) {
				File.Copy(XMLFilename, XMLFilename +".backup", true);
				string message = String.Format(MainForm.languages.GetString("LoadFavError"), e.Message, XMLFilename);
				MessageBox.Show(message, MainForm.languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		public bool LoadOld()
		{
			try {
				TextReader tr = new StreamReader(Filename);
				string s = tr.ReadToEnd();
				foreach(String server in s.Split(new Char[]{'\n'}))
				{
					if (server != ""){
						String[] favInfoTmp = server.Split(new Char[]{' '}, 2);
						String[] ipAddress = favInfoTmp[0].Split(':');
						ServerInformation info = new ServerInformation();
						info.hostname = favInfoTmp[1].Trim();
						info.host = new IPEndPoint(IPAddress.Parse(ipAddress[0]), Convert.ToInt32(ipAddress[1]));
						AddServer(info);
					}
				}
				tr.Close();
				File.Delete(Filename); //XML'ised!
				return true;
			} catch (Exception e) {
				return false;
			}
		}
		
		
	}
	public class MainListView : ServerListView{}


	public class ServerListItem : ServerInformation
	{
		public bool filtered;
		public int index;
		public ServerListItem(ServerInformation info)
		{
			this.cars = info.cars;
			this.connectFailed = info.connectFailed;
			this.filtered = false;
			this.host = info.host;
			this.hostname = info.hostname;
			this.password = info.password;
			this.passworded = info.passworded;
			this.ping = info.ping;
			this.players = info.players;
			this.racerNames = info.racerNames;
			this.readFailed = info.readFailed;
			this.rules = info.rules;
			this.slots = info.slots;
			this.success = info.success;
			this.totalServers = info.totalServers;
			this.track = info.track;
			this.version = info.version;
			this.adminPassword = info.adminPassword;
			this.insimPort = info.insimPort;
		}
	}
}
