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
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Xml;

namespace BrowseForSpeed.Frontend
{
		
	public class Language
	{
		public string name;
		public string filename;
		public string email;
		public string comment;
		public string author;
		public Hashtable strings = new Hashtable();
	}
	

	public class LanguageManager
	{
		private Hashtable languages = new Hashtable();
		private Language lang;
		private string directory;
		public int Count = 0;
		
		public LanguageManager(string langDirectory)
		{
			directory = langDirectory;
			try {
				DirectoryInfo dir = new DirectoryInfo(langDirectory);
				FileInfo[] files = dir.GetFiles("*.lang.xml");
				foreach (FileInfo file in files){
					try {
						Language l = ParseLanguage(file.FullName, false);
						languages.Add(l.name, l);
						Count++;
					} catch (Exception e) {
						MessageBox.Show("Error loading " +file.FullName+ "\n" + e.Message+"\nThis language will not be avaliable.", MainForm.appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			} catch (Exception e) {
				MessageBox.Show("Error loading languages. The only available language will be English" + e.Message , MainForm.appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			ChangeLanguage("English");//languages["English"] ?? ((string)Languages.ToArray()[0]));
		}
		
		private Language ParseLanguage(string filename, bool getStrings)
		{
			Language l = new Language();
			l.filename = filename;
			XmlDocument doc = new XmlDocument();
			try {
				System.IO.TextReader r = new System.IO.StreamReader(filename, System.Text.Encoding.Default);
				doc.Load(r);
				XmlNodeList list = doc.GetElementsByTagName("language");
				l.name = ((XmlElement)list[0]).GetElementsByTagName("name")[0].FirstChild.Value;
				l.email = ((XmlElement)list[0]).GetElementsByTagName("email")[0].FirstChild.Value;
				l.author = ((XmlElement)list[0]).GetElementsByTagName("author")[0].FirstChild.Value;
				l.comment = ((XmlElement)list[0]).GetElementsByTagName("comment")[0].FirstChild.Value;
				if (getStrings) {
					list = ((XmlElement)list[0]).GetElementsByTagName("component");
					foreach (XmlElement elem in list) {
						l.strings.Add(elem.GetAttribute("name"), elem.FirstChild.Value);
					}
				}
			} catch (Exception e) {
				MessageBox.Show(e.Message, MainForm.appTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return l;
		}
		
		public void ChangeLanguage(string language)
		{
			if (Count > 0) {
				lang = ParseLanguage(((Language)languages[language]).filename, true);
			}
		}
		
		public string GetString(string componentName)
		{
			return ((string)lang.strings[componentName] ?? componentName).Replace(@"\n", "\n");
		}
		
		public List<string> Languages{
			get {
				List<string> temp = new List<string>();
				foreach(Language l in languages.Values){
					temp.Add(l.name);
				}
				return temp;
			}
		}
		
		public string Author {
			get {
				return lang.author;
			}
		}
		
		public string Comment {
			get {
				return lang.comment;
			}
		}
		
		public string Email {
			get {
				return lang.email;
			}
		}
	}
}
