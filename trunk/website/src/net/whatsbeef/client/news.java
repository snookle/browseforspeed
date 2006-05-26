package net.whatsbeef.client;
import java.util.ArrayList;
import com.google.gwt.user.client.ui.*;

public class news {
	public static HTML getHTML () {
		String html = "<div class='page'><H2>Browse For Speed 0.1 - Released (Not quite)!</H2><p><a href='/files/browseforspeed-0.1.zip'>Browse for Speed 0.1</a> - All you need to run it.</p>"
			+ "<p><a href='/files/browseforspeed-0.1-src.zip'>Browse for Speed 0.1 Source Code</a> - Only download this if you want to peruse the source, make changes and submit patches :)</p>"
			+ "<p><a href='http://msdn.microsoft.com/netframework/downloads/updates/default.aspx'>.NET 2.0</a> - If you don't have this already, you will need it. Go there and download the 'Redistributable Package'.</p>"
			+ "<p> The library was developed with <a href='http://www.monodevelop.com/Main_Page'>MonoDevelop</a>, and requires .NET 1.1. The front-end code requires .NET 2.0. This is the first project we have ever written in c#."
			+ "<font color=\"#FF0000\"><p>!!!! NOTE: This is developed against an undocumented protocol. It might break tomorrow, or even right now. !!!!</p></font></div>";
		return new HTML(html);
	}
}
