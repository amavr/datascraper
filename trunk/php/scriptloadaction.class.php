<?php

class ScriptLoadAction extends ScriptAction
{
	private $enc = "utf-8";
	private $agent = "";
	
	private $cookie_file_name = "tmp.cookie";

	protected function executeInternal($deep)
	{
		if(stripos(trim($this->iData), "http://") === 0)
			$this->oData = $this->loadText($this->iData);
		else
			$this->oData = $this->read($this->iData);

		if($deep)
			$this->executeChild();
		else
			$this->bData = $this->oData;
	}

	protected function getAttributes($element)
	{
		$this->enc = $element->getAttribute("encoding");
		$this->agent = $element->getAttribute("agent");
	}

	public function read($url)
	{
		$this->trace($this->name.": ".$url);
		$msg = "";
		$i = 5;
		while($i > 0)
		{
			$data = file_get_contents($url);
			if($data == false)
			{
				$i--;
				$fmt = "ERROR ActionLoad->read(%s)";
				$msg = sprintf($fmt, $url);
				$this->trace($msg);
			}
			else
				return $data;
		}
		
		throw new Exception($msg, 1);
	}
	
	private function loadText($url)
	{
		$this->trace($this->name.": ".$url);
	
		$ch = curl_init();  
		curl_setopt($ch, CURLOPT_URL, $url); // set url to post to 
		curl_setopt($ch, CURLOPT_TIMEOUT, 900);
		curl_setopt($ch, CURLOPT_USERAGENT, $this->uagent);
		curl_setopt($ch, CURLOPT_COOKIEJAR, $cookie_file_name);
		curl_setopt($ch, CURLOPT_COOKIEFILE, $cookie_file_name);
		curl_setopt($ch, CURLOPT_FAILONERROR, false);  
		curl_setopt($ch, CURLOPT_FOLLOWLOCATION, 1);// allow redirects  
		curl_setopt($ch, CURLOPT_RETURNTRANSFER,1); // return into a variable  
		$result = curl_exec($ch); // run the whole process  
		curl_close($ch);   
		return $result;  	
	}
}

?>
