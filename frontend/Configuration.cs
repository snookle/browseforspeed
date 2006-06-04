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

namespace BrowseForSpeed.Frontend
{
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
		public bool startPS;
		public string psPath;
		public int psInsimPort;

		public Configuration(string filename) {
			this.filename = filename;			
		}

		public bool LoadXML() {
			try {
				XmlDocument doc = new XmlDocument();				
				doc.Load(filename);
				XmlNodeList list = doc.GetElementsByTagName("bfsconfig");
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
					XmlNode spot = ((XmlElement)list[0]).GetElementsByTagName("spotter")[0];
					startPS = (spot.Attributes["enabled"].Value == "True");
					psInsimPort = Convert.ToInt32(((XmlElement)spot).GetElementsByTagName("insimport")[0].FirstChild.Value);
					psPath = ((XmlElement)spot).GetElementsByTagName("path")[0].FirstChild.Value;
				} catch (Exception e) {
					startPS = false;
					psInsimPort = 29999;
					psPath = "";
				}
				try {
					joinOnClick = ((XmlElement)list[0]).GetElementsByTagName("listclick")[0].FirstChild.Value == "join";
				} catch (Exception e) { joinOnClick = true; }
				return true;
			} catch (FileNotFoundException fe) {
				MessageBox.Show("No existing configuration data found.\nPlease set this up now.", "", MessageBoxButtons.OK);
				return false;
			}
			catch (Exception e) {
				MessageBox.Show("Error loading configuration: " + e.Message + "\nA copy was saved as "+filename +".backup", "", MessageBoxButtons.OK);
				File.Copy(filename, filename +".backup", true);
				return false;
			}
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
					this.psInsimPort = Convert.ToInt32(tr.ReadLine());
				} catch (Exception e) {
					this.psInsimPort = 29999;
				}
				this.psPath = tr.ReadLine();
				this.startPS = (tr.ReadLine() == "True");
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
				tw.WriteAttributeString("version", "1");
				tw.WriteElementString("exepath", lfsPath);
				tw.WriteStartElement("qwait");
				tw.WriteAttributeString("enabled", disableWait.ToString());
				tw.WriteString(queryWait.ToString());
				tw.WriteEndElement();				
				tw.WriteElementString("checkversion", checkNewVersion.ToString());
				tw.WriteStartElement("spotter");
				tw.WriteAttributeString("enabled", startPS.ToString());
				tw.WriteElementString("insimport", psInsimPort.ToString());
				tw.WriteElementString("path", psPath);				
				tw.WriteEndElement();
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
