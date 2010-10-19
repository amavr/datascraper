<?php

include 'actionbuilder.class.php';

if($argc != 2) die("\n\nNothing to do!\n\nUSAGE:\nphp.exe dsx.php dsxfile\n\n");

function getTimeInSec()
{
	$mtime = explode(" ", microtime());
	return $mtime[1] + $mtime[0];
}

$builder = new ActionBuilder();
$builder->debugMode = FALSE;


$fname = $argv[1];

// echo(sprintf("\nScript param \$argv[1] = %s", $argv[1]));

$builder->setLogName($fname.date("Ymd").".log");
$builder->load($fname);

$tstart = getTimeInSec();
$builder->execute();
$tend = getTimeInSec();

echo(sprintf("\nScript executed %f seconds!", ($tend - $tstart)));
