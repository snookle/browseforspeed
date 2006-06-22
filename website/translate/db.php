<?php
define("DBHOST", "localhost");
define("DBUSERNAME", "root");
define("DBPASSWORD", "sn00kl3");
define("DBNAME", "bfslang");

function connect()
{
	$link = mysql_connect(DBHOST, DBUSERNAME, DBPASSWORD);
	select_db(DBNAME, $link);
	return $link;
}

function select_db($name, $link)
{
	mysql_select_db($name, $link) or die("FATAL ERROR: Could not select ". $name ."");
}

function select($query, &$result_set, $link)
{
	$result_set = mysql_query($query, $link) or die('Select query failed: '. mysql_error());
	return mysql_numrows($result_set);
}

function query($query, $link)
{
	mysql_query($query, $link) or die('Query failed: '. mysql_error());
}

function next_result(&$result_set)
{
	return mysql_fetch_array($result_set, MYSQL_BOTH);
}
?>