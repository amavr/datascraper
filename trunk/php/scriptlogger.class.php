<?php

class ScriptLogger
{
	private $path = "";
	private $logfile = null;

	public function __construct($FileName)
	{
	    $this->path = $FileName;
	}
	
	public function Open()
	{
		$this->logfile = fopen($this->path, "a+");
	}
	
	public function Write($Message)
	{
		$s = "\n".date("H:i:s")."\t".$Message;
		echo($s);
 		fwrite($this->logfile, $s);
	}
	
	public function Close()
	{
	    fclose($this->logfile);
	}
}

?>
