<?php
require 'Widget.php';

$a = new Widget();

$a->DisplayName();

// this doesn't work without error
// the error is that the use of ::func() only works if the function is static
//$a::DisplayName();
?>
