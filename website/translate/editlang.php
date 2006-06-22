<?php
session_start();
require_once('login.php');
require_once('helpers.php');
require_once('db.php');

$action = get('action');
$id = get('id');
if ($id == null) {
	die('No language ID specified');
}

$link = connect();
$num_rows = select("SELECT * FROM languages WHERE id = {$id}", $result_set, $link);
if ($num_rows == 0){
	die("Unable to find language {$id}!");
}

$result = next_result($result_set);
echo "<table border=1 cellpadding=5>\n";
echo "\t<tr><td><strong>Language Name:</strong></td><td>{$result['language']}</td></tr>\n";
//do we need the authors name?
//echo "\t<tr><td><strong>Author:</strong></td><td>{$result['author']}</td></tr>\n";
echo "\t<tr><td><strong>Comments:</strong></td><td><textarea cols=\"40\" name=\"comment\">{$result['comment']}</textarea></td></tr>\n";
echo "</table>";
echo "<br/><br/><br/>\n";
echo "<table border=1 cellpadding=5>\n";
echo "\t<tr><td><strong>Name</strong></td><td><strong>Default value</strong></td><td><strong>Translated value</strong></td></tr>\n";

if ($action == 'missing') {
	$num_rows = select("SELECT * FROM strings WHERE id NOT IN (SELECT comp_id FROM translation WHERE lang_id = {$id})", $result_set, $link);
	while (($result = next_result($result_set)) != null){
		echo "\t<tr><td>{$result['component']}</td><td>{$result['default']}</td><td><input name=\"{$result['component']}\" size=50\></td></tr>\n";
	}
}
//display the whole language
if ($action == null){
	$num_rows = select("SELECT * FROM strings WHERE id = {$id};", $result_set, $link);
	while (($result = next_result($result_set)) != null){
		//this should display the current value of the translated string too.
		//possibly move this into a function of its own.
		echo "\t<tr><td>{$result['component']}</td><td>{$result['default']}</td><td><input name=\"{$result['component']}\" size=50\></td></tr>\n";
	}
}



?>