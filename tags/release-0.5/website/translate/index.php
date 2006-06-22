<?php
session_start();
require_once('login.php');
require_once('db.php');

echo "Hi {$_SESSION['username']}!<br/>\n";
$link = connect();
$result_set = 0;
$num_rows = select("SELECT * FROM languages WHERE author = '{$_SESSION['userid']}' LIMIT 1;", $result_set, $link);
echo "You are currently maintaining {$num_rows} language.<br/>\n";
echo "<ul>\n";
while (($row = next_result($result_set)) != null) {
	echo "\t<li><a href=\"editlang.php?id={$row['id']}\">{$row['language']}</a> - {$row['comment']}</li>\n";
	select("SELECT count(*) FROM strings WHERE id NOT IN (SELECT comp_id FROM translation WHERE lang_id = {$row['id']})", $tmp, $link);
	$missing_strings = next_result($tmp);
	$missing_strings = $missing_strings['count(*)'];
	if ($missing_strings != 0){
		echo "\t<ul><li><font color=\"red\">Strings needing translation: {$missing_strings}</font><a href=\"editlang.php?id={$row['id']}&action=missing\">[Translate missing strings]</a></li></ul>\n";
	} else {
		echo "\t<ul><li><font color=\"green\">All strings up to date</font></li></ul>\n";
	}
}
echo "</ul>\n";
?>