package net.whatsbeef.client;
import com.google.gwt.user.client.ui.*;

public class news {
	public static HTML getHTML () {
		String html = "<div class='page'><H2>Browse For Speed 0.2 - Released!</H2><p><a href='/files/browseforspeed-0.2.zip'>Browse for Speed 0.2</a> - The main program files. This requires .NET 2.0 - see below for a direct download link.</p>"
			+ "<p>Here's version 0.2 of Browse For Speed. Release 0.1 turned out to be quite successful, with only a couple of minor bugs found. Thanks to everyone who provided feedback, we have implemented most (all?) of the requested features. 0.2 can launch <a href=\"http://www.kegetys.net/lfs/\">Pit Spotter</a> before starting LFS, passwords for favourite servers are saved. List font sizes can be changed by pressing ctrl and +/- (numpad plus/minus), and crashes have been fixed. NOTE: 0.2 uses xml for its config and favourite files. It will convert your current settings and favourites, but please back them up first (simply copy config.cfg and favourite.servers to another location before running 0.2).</p>"
			+ "<p><strong>Upgrade instructions:</strong> Simply unzip into your existing folder, overwriting any files.</p>"
			+ "<h4>Additional links:</h4>"
			+ "<p><a href='/files/browseforspeed-0.2-src.zip'>Browse for Speed 0.2 Source Code</a> - Only download this if you want to peruse the source, make changes and submit patches :) If you want to send us patches, contact us for SVN access.</p>"
			+ "<p><a href='http://msdn.microsoft.com/netframework/downloads/updates/default.aspx'>.NET 2.0</a> - If you don't have this already, you will need it. Go there and download the 'Redistributable Package'.</p>"
			+ "<p> The library was developed with <a href='http://www.monodevelop.com/Main_Page'>MonoDevelop</a>, and requires .NET 1.1. The front-end code requires .NET 2.0. This is the first project we have ever written in c#."
			+ "<font color=\"#FF0000\"><p>!!!! NOTE: This is developed against an undocumented protocol. It might break tomorrow, or even right now. !!!!</p></font></div>";
		return new HTML(html);
	}
}
