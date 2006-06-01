package net.whatsbeef.client;

import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.user.client.ui.*;

/**
 * Entry point classes define <code>onModuleLoad()</code>.
 */
public class browseforspeed implements EntryPoint {

  /**
   * This is the entry point method.
   */
	public void onModuleLoad() {
		TabPanel tp = new TabPanel();
		tp.add(news.getHTML(), "News");
		tp.add(screenshots.getHTML(), "Screenshots");
		tp.add(faq.getHTML(), "FAQ");
		tp.add(contact.getHTML(), "Contact");
		tp.selectTab(0);
		tp.setWidth("100%");
		RootPanel.get().add(tp);
  }
}
