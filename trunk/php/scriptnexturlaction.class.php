<?php

class ScriptNextURLAction extends ScriptAction
{
    public $fmtstr = "";
	public $pattern = "";
	public $do_equal = true;
	
	private $page_num = 1;

    protected function executeInternal($deep)
    {
		$stop = false;
        $url = VarTable::ParseVariables($this->iData);
		// $url = $this->iData;
		$old = "";
		$this->bData = "";
		
		$this->page_num = 1;
		
		do
		{
			$this->oData = $this->read($url);
			if($deep)
				$this->executeChild();
			else
				$this->bData = $this->oData;
				
			$old = $url;
			$url = $this->findURL();
			$stop =($url == "") || (($url == $old) && $this->do_equal);
			
			$this->page_num++;
			
		}while($stop == false);
    }
    
    private function findURL()
    {
		try
		{
			preg_match_all($this->pattern, $this->oData, $mm);
			for($i = 0; $i < count($mm[0]); $i++)
			{
				$s = $this->fmtstr;
				for($j = 0; $j < count($mm); $j++)
					$s = str_replace("{".$j."}", $mm[$j][$i], $s);

				$s = str_replace("&amp;", "&", $s);
	            return $s;
			}
			return "";
		}
		catch(Exception $e)
		{
			echo "pattern = $this->pattern";
			throw new Exception($e->getMessage());
		}
    }

	protected function getActionInfoInternal()
	{
	    return "%s-%s(%d/%d/".sprintf("%d", $this->page_num).")";
	}

    protected function getAttributes($element)
    {
        $this->pattern = "!".$element->getAttribute("pattern")."!si";
        $this->fmtstr = $element->getAttribute("format-string");
        $this->do_equal = (strtoupper($element->getAttribute("append")) == "TRUE") ? true : false;
    }

	public function read($url)
	{
		// return file_get_contents ($url);
		
		$this->trace("NextURLAction load from: ".$url);
		$msg = "";
		$i = 5;
		while($i > 0)
		{
			$data = file_get_contents($url);
			if($data == false)
			{
				$i--;
				$fmt = "ERROR ActionNextURL->read(%s)";
				$msg = sprintf($fmt, $url);
				$this->trace($msg);
			}
			else
				return $data;
		}
		
		throw new Exception($msg, 1);
	}

}

?>
