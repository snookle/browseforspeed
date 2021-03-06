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
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using libbrowseforspeed;
using System.Runtime.InteropServices;

namespace BrowseForSpeed.Frontend
{
public class PreStartProgram
{
	public string path;
	public string name;
	public string options;
	public bool enabled;
	
	public PreStartProgram()
	{
		this.path = "";
		this.name = "";
		this.options = "";
		enabled = false;
	}
	
	public PreStartProgram(string name, string path, string options)
	{
		this.name = name;
		this.path = path;
		this.options = options;
		enabled = true;
	}
	
	public override string ToString()
	{
		return this.name + " - " + (this.enabled ? MainForm.languages.GetString("Global.Enabled") : MainForm.languages.GetString("Global.Disabled"));
	}
	
}
public class Configuration
{
	/*[DllImport("user32.dll")]
	public static extern IntPtr FindWindow(string lpClassName,string lpWindowName);
    
	[DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);*/
	
		public string userName;
		public string password;
		public string lfsPath;
		public string filename;
		public string language;
		public bool disableWait;
		public bool checkNewVersion;
		public int queryWait;
		public bool joinOnClick;
		public int insimPort;
		public bool filter_empty;
		public bool filter_full;
		public bool filter_private;
		public bool filter_public;
		public bool filter_cruise;
		public bool filter_reset;
		public bool hide_offline;
		public int ping_threshold;
		public string filter_track;
		public bool fav_refresh;
		public bool friend_refresh;
		public string filter_version;
		public ulong filter_cars_allow;
		public ulong filter_cars_disallow;
		public bool fancy_hostnames;
		public bool debug_window;
		public List<PreStartProgram> psp = new List<PreStartProgram>();

		public Configuration(string filename) {
			this.filename = filename;			
		}

		private void DebugVisible(bool visible)
		{
			/*IntPtr handle = FindWindow(null, System.Windows.Forms.Application.ExecutablePath); //put your console window caption here
			if(handle != IntPtr.Zero) {
				ShowWindow(handle, (visible ? 1 : 0)); // 0 = SW_HIDE, 1 = SW_SHOW or something
			}*/                

		}
		
		public bool LoadXML() {
			try {
				XmlDocument doc = new XmlDocument();				
				doc.Load(filename);
				XmlNodeList list = doc.GetElementsByTagName("bfsconfig");
				String docVersion = ((XmlElement)list[0]).GetAttribute("version");
				try {
					debug_window = ((XmlElement)list[0]).GetElementsByTagName("debug_window")[0].FirstChild.Value == "True";
					DebugVisible(debug_window);
				} catch (Exception e) {
					debug_window = false;
					DebugVisible(debug_window);
				}

				try {
					lfsPath = ((XmlElement)list[0]).GetElementsByTagName("exepath")[0].FirstChild.Value;
				} catch (Exception e) { lfsPath = ""; }
				try {
					disableWait = ((XmlElement)list[0]).GetElementsByTagName("qwait")[0].Attributes["enabled"].Value == "True";
				} catch (Exception e) { disableWait = false; }
				try {
					queryWait = Convert.ToInt32(((XmlElement)list[0]).GetElementsByTagName("qwait")[0].FirstChild.Value);
				} catch (Exception e) { queryWait = 150; }
				try {
					checkNewVersion = ((XmlElement)list[0]).GetElementsByTagName("checkversion")[0].FirstChild.Value == "True";
				} catch (Exception e) { checkNewVersion = false; }
				try {
					filter_empty = ((XmlElement)list[0]).GetElementsByTagName("filter_empty")[0].FirstChild.Value == "True";
				} catch (Exception e) { filter_empty = true; }
				try {
					filter_full = ((XmlElement)list[0]).GetElementsByTagName("filter_full")[0].FirstChild.Value == "True";
				} catch (Exception e) { filter_full = true; }
				try {
					filter_private = ((XmlElement)list[0]).GetElementsByTagName("filter_private")[0].FirstChild.Value == "True";
				} catch (Exception e) { filter_private = true; }
				try {
					filter_public = ((XmlElement)list[0]).GetElementsByTagName("filter_public")[0].FirstChild.Value == "True";
				} catch (Exception e) { filter_public = true; }
				try {
					filter_reset = ((XmlElement)list[0]).GetElementsByTagName("filter_cruise")[0].FirstChild.Value == "True";
				} catch (Exception e) { filter_reset = false; }
				try {
					filter_cruise = ((XmlElement)list[0]).GetElementsByTagName("filter_reset")[0].FirstChild.Value == "True";
				} catch (Exception e) { filter_cruise = false; }
				try {
					hide_offline = ((XmlElement)list[0]).GetElementsByTagName("hide_offline")[0].FirstChild.Value == "True";
				} catch (Exception e) { hide_offline = false; }
				try {
					fav_refresh = ((XmlElement)list[0]).GetElementsByTagName("fav_refresh")[0].FirstChild.Value == "True";
				} catch (Exception e) { fav_refresh = false; }
				try {
					friend_refresh = ((XmlElement)list[0]).GetElementsByTagName("friend_refresh")[0].FirstChild.Value == "True";
				} catch (Exception e) { friend_refresh = false; }
				try {
					fancy_hostnames = ((XmlElement)list[0]).GetElementsByTagName("fancy_hostnames")[0].FirstChild.Value == "True";
				} catch (Exception e) { fancy_hostnames = true; }
				try {
					filter_cars_allow = Convert.ToUInt64(((XmlElement)list[0]).GetElementsByTagName("filter_cars_allow")[0].FirstChild.Value);
				} catch (Exception e) { filter_cars_allow = 0; }
				try {
					filter_cars_disallow = Convert.ToUInt64(((XmlElement)list[0]).GetElementsByTagName("filter_cars_disallow")[0].FirstChild.Value); 
				} catch (Exception e) { filter_cars_disallow = 0; }
				try {
					language = ((XmlElement)list[0]).GetElementsByTagName("language")[0].FirstChild.Value;
				} catch (Exception e) { language = ""; }
				try {
					filter_track = ((XmlElement)list[0]).GetElementsByTagName("filter_track")[0].FirstChild.Value;
				} catch (Exception e) { filter_track = ""; }
				try {
					lfsPath = ((XmlElement)list[0]).GetElementsByTagName("exepath")[0].FirstChild.Value;
				} catch (Exception e) { lfsPath = ""; }
				try {
					filter_version = ((XmlElement)list[0]).GetElementsByTagName("filter_version")[0].FirstChild.Value;
				} catch (Exception e) { filter_version = "S2"; }
				try {
					ping_threshold = Convert.ToInt32(((XmlElement)list[0]).GetElementsByTagName("ping_threshold")[0].FirstChild.Value);
				} catch (Exception e) { ping_threshold = 5000; }
				if (docVersion == "1"){
					PreStartProgram p = new PreStartProgram();
						XmlNode spot = ((XmlElement)list[0]).GetElementsByTagName("spotter")[0];
						p.enabled = (spot.Attributes["enabled"].Value == "True");
						if (p.enabled != false) {
							p.name = "Pit Spotter";
							p.options = "";
							try {
								p.path = ((XmlElement)spot).GetElementsByTagName("path")[0].FirstChild.Value;
							} catch (Exception e) {
								p.path = "";
							}
							psp.Add(p);
						}
				} else {
					insimPort = Convert.ToInt32(((XmlElement)list[0]).GetElementsByTagName("insimport")[0].FirstChild.Value);
					XmlNodeList pspList = ((XmlElement)list[0]).GetElementsByTagName("psp");
					foreach (XmlElement pspXML in pspList) {
						PreStartProgram p = new PreStartProgram();
						try {
							p.enabled = (pspXML.GetAttribute("enabled") == "True");
							p.name = pspXML.GetElementsByTagName("name")[0].FirstChild.Value;					
							p.path = pspXML.GetElementsByTagName("path")[0].FirstChild.Value;					
							try {
								p.options = pspXML.GetElementsByTagName("options")[0].FirstChild.Value;
							} catch (Exception e) {
								p.options = "";
							}

						} finally {
							psp.Add(p);
						}
						
				}
				}
				try {
					joinOnClick = ((XmlElement)list[0]).GetElementsByTagName("listclick")[0].FirstChild.Value == "join";
				} catch (Exception e) { joinOnClick = true; }
				return true;
			} catch (FileNotFoundException fe) {
				MessageBox.Show("No existing configuration data found.\nPlease set this up now.", "", MessageBoxButtons.OK);
				return false;
			}
			/*catch (Exception e) {
				MessageBox.Show("Error loading configuration: " + e.Message + "\nA copy was saved as "+filename +".backup", "", MessageBoxButtons.OK);
				File.Copy(filename, filename +".backup", true);
				return false;
			}*/
		}


		public bool Load() {
			try {
				TextReader tr = new StreamReader(this.filename);
				//this.userName = tr.ReadLine();
				//this.password = decrypt(tr.ReadLine());
				this.lfsPath = tr.ReadLine();
				this.disableWait = (tr.ReadLine() == "True");
				this.checkNewVersion = (tr.ReadLine() == "True");
				try {
					this.queryWait = Convert.ToInt32(tr.ReadLine());
				} catch (Exception e) {
					this.queryWait = 150;
				}
				try {
					this.joinOnClick = (tr.ReadLine() != "view");
				} catch (Exception e) {
					this.joinOnClick = true;
				}
				try {
					this.insimPort = Convert.ToInt32(tr.ReadLine());
				} catch (Exception e) {
					this.insimPort = 29999;
				}
				PreStartProgram p = new PreStartProgram();
				p.name = "Pit Spotter";
				p.path = tr.ReadLine();
				p.enabled = (tr.ReadLine() == "True");
				psp.Add(p);
				tr.Close();
				return true;
			} catch (Exception e) {
				return false;
			}
		}

		public void Save(string filename)
		{
			try {				
				XmlTextWriter tw = new XmlTextWriter(filename, null);
				tw.Formatting = Formatting.Indented;
				tw.WriteStartDocument();
				tw.WriteStartElement("bfsconfig");
				tw.WriteAttributeString("version", "2");
				tw.WriteElementString("exepath", lfsPath);
				tw.WriteStartElement("qwait");
				tw.WriteAttributeString("enabled", disableWait.ToString());
				tw.WriteString(queryWait.ToString());
				tw.WriteEndElement();				
				tw.WriteElementString("insimport", insimPort.ToString());
				tw.WriteElementString("checkversion", checkNewVersion.ToString());
				tw.WriteElementString("filter_public", filter_public.ToString());
				tw.WriteElementString("filter_private", filter_private.ToString());
				tw.WriteElementString("filter_empty", filter_empty.ToString());
				tw.WriteElementString("filter_full", filter_full.ToString());
				tw.WriteElementString("filter_reset", filter_reset.ToString());
				tw.WriteElementString("filter_cruise", filter_cruise.ToString());
				tw.WriteElementString("filter_version", filter_version.ToString());
				tw.WriteElementString("hide_offline", hide_offline.ToString());
				tw.WriteElementString("ping_threshold", ping_threshold.ToString());
				tw.WriteElementString("fav_refresh", fav_refresh.ToString());
				tw.WriteElementString("filter_track", filter_track.ToString());
				tw.WriteElementString("friend_refresh", friend_refresh.ToString());
				tw.WriteElementString("fav_refresh", fav_refresh.ToString());
				tw.WriteElementString("filter_cars_allow", filter_cars_allow.ToString());
				tw.WriteElementString("filter_cars_disallow", filter_cars_disallow.ToString());
				tw.WriteElementString("fancy_hostnames", fancy_hostnames.ToString());
				tw.WriteElementString("debug_window", debug_window.ToString());
				tw.WriteElementString("language", this.language);
				foreach(PreStartProgram p in psp){
					tw.WriteStartElement("psp");
					tw.WriteAttributeString("enabled", p.enabled.ToString());
					tw.WriteElementString("name", p.name);				
					tw.WriteElementString("path", p.path);
					tw.WriteElementString("options", p.options);
					tw.WriteEndElement();
				}
				tw.WriteElementString("listclick", joinOnClick ? "join" : "view");
				tw.WriteEndElement();
				tw.Close();
			}
			catch (Exception ex) {
				MessageBox.Show("There was an error writing to the config file:" +ex.Message, MainForm.appTitle, MessageBoxButtons.OK);
			}

		}


	}
}
