/*
 * Created by SharpDevelop.
 * User: Ben
 * Date: 3/06/2006
 * Time: 10:10 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections.Generic;

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
		return this.name + " - " + (this.enabled ? "Enabled" : "Disabled");
	}
	
}
public class Configuration
{
		public string userName;
		public string password;
		public string lfsPath;
		public string filename;
		public bool disableWait;
		public bool checkNewVersion;
		public int queryWait;
		public bool joinOnClick;
		public int insimPort;
		public List<PreStartProgram> psp = new List<PreStartProgram>();

		public Configuration(string filename) {
			this.filename = filename;			
		}

		public bool LoadXML() {
			try {
				XmlDocument doc = new XmlDocument();				
				doc.Load(filename);
				XmlNodeList list = doc.GetElementsByTagName("bfsconfig");
				String docVersion = ((XmlElement)list[0]).GetAttribute("version");
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
