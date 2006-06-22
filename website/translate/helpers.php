<?php
function get($variable)
{
	return isset($_GET[$variable]) ? $_GET[$variable] : NULL;
}

function post($variable)
{
	return isset($_POST[$variable]) ? $_POST[$variable] : NULL;
}

function request($variable)
{
	return isset($_REQUEST[$variable]) ? $_REQUEST[$variable] : NULL;
}
?>