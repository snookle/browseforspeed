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
using System.Collections;
using System.Collections.Generic;
using libbrowseforspeed;

namespace BrowseForSpeed.Frontend
{
public enum SortType{Hostname, Ping, Players, Track};

public class ListSorter: IComparer<ServerListItem>
{
	public SortOrder order;
	public SortType sortType;
	private CaseInsensitiveComparer ObjectCompare;

	public ListSorter()
	{
		order = SortOrder.None;
		sortType = SortType.Ping;
		ObjectCompare = new CaseInsensitiveComparer();
	}

	public int Compare(ServerListItem itemX, ServerListItem itemY)
	{
		int compareResult = 0;
			switch (sortType) {
				case SortType.Hostname:
					compareResult = ObjectCompare.Compare(LFSQuery.removeColourCodes(itemX.hostname), LFSQuery.removeColourCodes(itemY.hostname)); break;
				case SortType.Ping:
					compareResult = ObjectCompare.Compare(Convert.ToInt32(itemX.ping), Convert.ToInt32(itemY.ping)); break;
				case SortType.Players:
					compareResult = ObjectCompare.Compare(Convert.ToInt32(itemX.players), Convert.ToInt32(itemY.players)); break;
				case SortType.Track:
					compareResult = ObjectCompare.Compare(itemX.track, itemY.track); break;
			}

		if (order == SortOrder.Ascending) {
			return compareResult;
		}
		else if (order == SortOrder.Descending) {
			return (-compareResult);
		}
		return 0;
	}
}

/// Horray for code nicked from the MSDN!
public class ListViewColumnSorter : IComparer
{
	private int ColumnToSort;
	private SortOrder OrderOfSort;
	private CaseInsensitiveComparer ObjectCompare;

	public ListViewColumnSorter()
	{
		ColumnToSort = 0;
		OrderOfSort = SortOrder.None;
		ObjectCompare = new CaseInsensitiveComparer();
	}

	public int Compare(object x, object y)
	{
		int compareResult;
		ListViewItem listviewX, listviewY;
		listviewX = (ListViewItem)x;
		listviewY = (ListViewItem)y;
		try {
		string columnName = listviewX.ListView.Columns[ColumnToSort].Text;
		if (columnName == MainForm.languages.GetString("MainForm.columnHeaderFavPing")) { //if we're sorting the ping column
			compareResult = ObjectCompare.Compare(Convert.ToInt32(listviewX.SubItems[ColumnToSort].Text), Convert.ToInt32(listviewY.SubItems[ColumnToSort].Text));
		} else if (columnName == MainForm.languages.GetString("MainForm.columnHeaderFavSlots")) { //sorting connection column
				int playersX = Convert.ToInt32(listviewX.SubItems[ColumnToSort].Text.Split('/')[0]);
				int playersY = Convert.ToInt32(listviewY.SubItems[ColumnToSort].Text.Split('/')[0]);
				compareResult = ObjectCompare.Compare(playersX, playersY);
		} else  if (columnName == MainForm.languages.GetString("MainForm.columnFriendServer") && (listviewX.SubItems[ColumnToSort].Text == MainForm.languages.GetString("MainForm.Offline")) || (listviewY.SubItems[ColumnToSort].Text == MainForm.languages.GetString("MainForm.Offline"))) {
				compareResult = (listviewX.SubItems[ColumnToSort].Text == MainForm.languages.GetString("MainForm.Offline") ? listviewY.SubItems[ColumnToSort].Text == MainForm.languages.GetString("MainForm.Offline") ? 0 : 1 : -1);
		} else {
			compareResult = ObjectCompare.Compare(LFSQuery.removeColourCodes(listviewX.SubItems[ColumnToSort].Text), LFSQuery.removeColourCodes(listviewY.SubItems[ColumnToSort].Text));
		}
		if (OrderOfSort == SortOrder.Ascending) {
			return compareResult;
		}
		else if (OrderOfSort == SortOrder.Descending) {
			return (-compareResult);
		}

		} catch (Exception ex) {
			//suppress any exceptions relating to indexes out of range.
		}
		return 0;
	}

	public int SortColumn
	{
		set	{
			ColumnToSort = value;
		}
		get	{
			return ColumnToSort;
		}
	}

	public SortOrder Order
	{
		set	{
			OrderOfSort = value;
		}
		get	{
			return OrderOfSort;
		}
	}

}

}
