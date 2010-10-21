<?php

class ScriptLoadAction extends ScriptAction
{
	private $enc = "utf-8";
	private $agent = "";
	
	public static $cookie_file_name = "tmp.cookie";

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

	protected function read($url)
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
				$fmt = "ERROR ".$this->name." read(%s)";
				$msg = sprintf($fmt, $url);
				$this->trace($msg);
			}
			else
			{
				$data = iconv($this->enc, "windows-1251", $data);
				return $data;
			}
		}
		
		throw new Exception($msg, 1);
	}
	
	protected function loadText($url)
	{
		$proxy = "";
		$max_times = 20;
		while($max_times > 0)
		{
			$this->trace($this->name.": ".$url);
			
			$ch = curl_init();  
			curl_setopt($ch, CURLOPT_COOKIE,  CookieSorage::getInstance()->cookies);
			// curl_setopt($ch, CURLOPT_COOKIEJAR, self::$cookie_file_name);
			// curl_setopt($ch, CURLOPT_COOKIEFILE, self::$cookie_file_name);
			
			if(ProxyList::getInstance()->HasProxy())
			{
				$proxy = ProxyList::getInstance()->Current();
				curl_setopt($ch, CURLOPT_PROXY, $proxy);
				// curl_setopt($ch, CURLOPT_HTTPPROXYTUNNEL, 1);
			}
			
			curl_setopt($ch, CURLOPT_URL, $url); // set url to post to 
			curl_setopt($ch, CURLOPT_TIMEOUT, 900);
			curl_setopt($ch, CURLOPT_USERAGENT, $this->agent);
			curl_setopt($ch, CURLOPT_FAILONERROR, false);  
			curl_setopt($ch, CURLOPT_FOLLOWLOCATION, 1);// allow redirects  
			curl_setopt($ch, CURLOPT_RETURNTRANSFER,1); // return into a variable  
			$result = curl_exec($ch); // run the whole process
			$errno = curl_errno($ch);
			$error = curl_error($ch);
			curl_close($ch);
			
			if($errno == 0) break;
			
			$this->trace($this->name." ERROR: $errno $error");
			
			$max_times--;
			if($max_times > 0)
				$this->trace($this->name." change proxy: $proxy -> ".ProxyList::getInstance()->Next());
		}

		$result = iconv($this->enc, "windows-1251", $result);

		CookieSorage::getInstance()->ParseCookie($result);
		
		return $result;  	
	}
}

?>
