package net.whatsbeef.client;
import java.util.ArrayList;
import com.google.gwt.user.client.ui.*;

public class faq {
	public static HTML getHTML () {
		ArrayList questions = new ArrayList();
		ArrayList answers = new ArrayList();
		questions.add("What can I do with Browse for Speed?");
		answers.add("Browse for Speed is a server browser for the popular online racing game <a href='http://www.liveforspeed.net'>Live for Speed</a>");
		questions.add("Why does it not tell me if my favourite servers are passworded until I get the detailed information?");
		answers.add("For reasons unknown to us, the rules information returned by each server does not include if the server is passworded. The master server lets you know which servers are passworded when returning a list of hosts, therefore it is not technically possible for us to do this, until you view the server's detailed information.");
		questions.add("Who developed it? Is it part of LFS?");
		answers.add("Three developers, who have absolutely no connection with the development of LFS. Richard Nelson wrote the majority of liblfsbrowser, Ben Kenny did the frontend, and Philip Nelson yelled abuse and helped with some of the development.");
		questions.add("Where is the server query protocol documented?");
		answers.add("Use the Source, Luke!");
		questions.add("My firewall complains that your application is trying to access the internet. Why?");
		answers.add("Querying remote servers traditionally requires some internet activity. The only servers Browse for Speed will contact are:<ul><li>The LFS Master Server</li><li>Each LFS game server</li><li>If you have version check enabled, it will query this website, purely for version checking.</li></ul>");
		questions.add("How can I make it faster? What is this 'Disable query wait' checkbox?");
		answers.add("Windows XP SP2 introduced a limit of 10 outgoing TCP connections per second (this is completely ridiculous). If you are not using XPSP2, you may be able to check this box, and the whole query process will be much, much faster. If you check this when you shouldn't, queries will start failing after a random amount of succeeded queries (Bill seems unable to test this limit very accurately. See the following page for more information, and a possibly way to remove this limit on XPSP2: <a href='http://www.speedguide.net/read_articles.php?id=1497'>http://www.speedguide.net/read_articles.php?id=1497</a>)");
		questions.add("I want to use your library in my own application. Can I do so?");
		answers.add("Of course, but remember it is GPL'd. Do not violate the GPL. If you have ideas for a better server browser though, we would much prefer that you sent suggestions, or even better, patches (diff format is preferred).");
		questions.add("What advantage does it have over the in-game browser, or LFSWorld with Join2LFS?");
		answers.add("It is currently a lot faster than the in-game browser, provides ping times (unlike LFSWorld/Join2LFS). It also has a number of extra features, such as a favourite server list (which means you do not have to get a list from the server every time), car and track filtering. It can also show you the players currently in a server.");
		questions.add("What licence is the software and its source avaliable under?");
		answers.add("We're currently licencing the software under version 2 of the GNU GPL. A copy of this licence can be found in the file called COPYING inside the Browse For Speed distribution. You can also view the licence online at <a href='http://www.gnu.org/licenses/gpl.txt'>http://www.gnu.org/licenses/gpl.txt</a>");
		questions.add("This site layout looks awfully similar to some of Google's stuff. Why?");
		answers.add("Google Web Toolkit was used to make it. I saw it was released the other day, and wanted to give it a whirl. This is the first static site I have ever had to compile ;)");
		questions.add("Do you guys play LFS? What are your game usernames?");
		answers.add("wabz (Richard) and snookle (Ben). We play on Australian servers. Philip is too poor to buy LFS.");
		String html = "";
		for (int i = 0; i < questions.size(); i++) {
			html += "<div class='page'><div class='faq-question'>" + (String)questions.get(i) + "</div>\n";
			html += "<div>" + (String)answers.get(i) + "</div></div>\n";
		}
		return new HTML(html);
	}
}
