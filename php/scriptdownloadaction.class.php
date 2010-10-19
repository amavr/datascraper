<?php

class ScriptDownloadAction extends ScriptAction
{
	private $original = false;
	private $dir = ".";

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
		$this->trace($this->name.": ".$sour);
        $fsour = fopen($sour, 'r'); 
        
		$this->dir = str_replace("/", DIRECTORY_SEPARATOR, $this->dir);
		$this->dir = str_replace("\\", DIRECTORY_SEPARATOR, $this->dir);
		$this->dir .= DIRECTORY_SEPARATOR;
		$this->dir = str_replace(DIRECTORY_SEPARATOR.DIRECTORY_SEPARATOR, DIRECTORY_SEPARATOR, $this->dir);
		
		$dest = "";

		if($this->original)
		{
			$dest .= $this->extractFileName($sour);
		}
		else
		{
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
		}

		
		
		$fdest = fopen($this->dir.$dest, 'w+'); 
		$len = stream_copy_to_stream($fsour, $fdest); 
		fclose($fsour); 
		fclose($fdest); 
		
		return $dest;
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
		$this->dir = $element->getAttribute("dir");
	}
	
}

?>
