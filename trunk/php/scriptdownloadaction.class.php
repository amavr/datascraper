<?php

class ScriptDownloadAction extends ScriptAction
{
	private $original = false;
	private $dir = ".";
	private $override = false;

	protected function executeInternal($deep)
	{
		$this->oData = $this->download($this->iData);

		if($deep)
			$this->executeChild();
		else
			$this->bData = $this->oData;
	}

    protected function download($sour) 
    {
		$sour = $this->correct_url($sour);
		// $this->trace($this->name." from: ".$sour);

		$this->dir = str_replace("/", DIRECTORY_SEPARATOR, $this->dir);
		$this->dir = str_replace("\\", DIRECTORY_SEPARATOR, $this->dir);
		$this->dir .= DIRECTORY_SEPARATOR;
		$this->dir = str_replace(DIRECTORY_SEPARATOR.DIRECTORY_SEPARATOR, DIRECTORY_SEPARATOR, $this->dir);
		
		$dest = "";
		
		if($this->original)
		{
			$dest = urldecode($sour);
			$dest = $this->extractFileName($dest);
			// $this->trace($this->name." to: ".$dest);
			
			$exists = file_exists($this->dir.$dest);
			
			if(!$this->override && $exists)
			{
				$this->trace($this->name.": $sour --X ".$this->dir.$dest);
				return $dest;
			}
		}
		else
		{
	        $fsour = fopen($sour, 'r'); 
			$dest .= uniqid('gen-');
			
			// Пытаемся определить mime-type для определения расширения
			$fheads = stream_get_meta_data($fsour);
			foreach($fheads["wrapper_data"] as $fhead)
			{
				if(strpos($fhead, 'Content-Type:') !== false)
				{
					$s = explode(' ', trim($fhead));
					if(count($s) == 2)
					{
						$s = explode('/', $s[1]);
						if(count($s) == 2) $dest .= ".".$s[1];
					}
					break;
				}
			}
			fclose($fsour); 
		}
		
		/*
        $fsour = fopen($sour, 'r'); 
		$fdest = fopen($this->dir.$dest, 'w+'); 
		$len = stream_copy_to_stream($fsour, $fdest); 
		fclose($fsour); 
		fclose($fdest); 
		*/
		
		$proxy = "";
		$max_times = 20;
		while($max_times > 0)
		{
			$this->trace($this->name.": $sour --> ".$this->dir.$dest);
			
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
			
			curl_setopt($ch, CURLOPT_URL, $sour);
			curl_setopt($ch, CURLOPT_TIMEOUT, 30);
			curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
			curl_setopt($ch, CURLOPT_USERAGENT, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)");
			
			$st = curl_exec($ch);
			
			$fd = @fopen($this->dir.$dest, "w");
			fwrite($fd, $st);
			@fclose($fd);
			
			$errno = curl_errno($ch);
			$error = curl_error($ch);
			curl_close($ch);
			
			if($errno == 0) break;
			
			$this->trace($this->name." ERROR: ".$error);
			
			$max_times--;
			if($max_times > 0)
				$this->trace($this->name." change proxy: $proxy -> ".ProxyList::getInstance()->Next());
		}
			
			
		/*
		$ch = curl_init();
		curl_setopt($ch, CURLOPT_URL, $sour);
		curl_setopt($ch, CURLOPT_TIMEOUT, 30);
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
		curl_setopt($ch, CURLOPT_USERAGENT, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)");


		$st = curl_exec($ch);
		$fd = @fopen($this->dir.$dest, "w");
		fwrite($fd, $st);
		@fclose($fd);

		curl_close($ch);
			
		*/
		
		return $dest;
    }	
	
	private function correct_url($url)
	{
		$url = urlencode(iconv("UTF-8", "windows-1251", $url));
		$url = str_replace("%2F", "/", $url);
		$url = str_replace("%3A", ":", $url);
		$url = str_replace("+", "%20", $url);
		return $url;
	}
	
	private function getContentType($content)
	{
		preg_match_all('|Content-Type: (.*);|U', $content, $results);
		self::getInstance()->cookies = implode(';', $results[1]);
	}	
	
	protected function extractFileName($full)
	{
		$s = basename($full);
		$a = explode('?', $s);
		if($a === false)
		{
			throw new Exception(sprintf("ERROR ActionDownload->extractFileName(%s)", $full), 1);
		}
		$s = $a[0];
		return $s;
	}	
	
	protected function getAttributes($element)
	{
		$this->original = (strtoupper($element->getAttribute("original-name")) == "TRUE") ? true : false;
		$this->override = (strtoupper($element->getAttribute("override")) == "TRUE") ? true : false;
		$this->dir = $element->getAttribute("dir");
	}
	
}

?>
