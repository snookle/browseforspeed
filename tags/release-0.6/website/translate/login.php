<?php
require_once('helpers.php');
require_once('db.php');

$referer = get('referer') != null ? get('referer') : $_SERVER['PHP_SELF'];
if (!session_is_registered('loggedin')) {
	if (($username = post('username')) && ($password = post('password'))) {
		$link = connect();
		if (select("SELECT * FROM users WHERE name = '{$username}' AND password = '{$password}' LIMIT 1;", $result_set, $link) == 1){
			session_start();
			$_SESSION['loggedin'] = true;
			$row = next_result($result_set);
			$_SESSION['userid'] = $row['id'];
			$_SESSION['username'] = $row['name'];
			header('Location: ' . $referer);
		} else {
			echo 'Incorrect username/password';
		}
	}
	echo "<FORM action=\"login.php?referer={$referer}\" method=\"post\">\n";
	echo "\tUsername: <INPUT type=\"text\" name=\"username\"><BR/>\n";
	echo "\tPassword: <INPUT type=\"password\" name=\"password\"><BR/>\n";
	echo "\t<INPUT type=\"Submit\" name=\"login\" value=\"Login\"><BR/>\n";
	echo "</FORM>\n";
	die();
} else {

}
?>

