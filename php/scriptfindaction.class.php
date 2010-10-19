<?php

class ScriptFindAction extends ScriptAction
{
	public $fmtstr = "";
	public $pattern = "";
	public $isUnique = false;
	private $vals = array();

	protected function executeInternal($deep)
	{
		// $this->trace($this->name.": pattern=".$this->pattern);
		preg_match_all($this->pattern, $this->iData, $mm);
		for($i = 0; $i < count($mm[0]); $i++)
		{
			$s = $this->fmtstr;
			for($j = 0; $j < count($mm); $j++)
				$s = str_replace("{".$j."}", $mm[$j][$i], $s);

			// $this->trace($this->name.": ".$s);

			if($this->isUnique)
			{
				$ups = strtoupper($s);
				if(in_array($ups, $this->vals))
					continue;
				else
					array_push($this->vals, $ups);
			}

			$this->oData = $s;
			if($deep)
				$this->executeChild();
			else
				$this->bData = $this->oData;
		}
	}

    protected function getAttributes($element)
    {
        $this->pattern = "!".$element->getAttribute("pattern")."!si";
        $this->fmtstr = $element->getAttribute("format-string");
        $this->isUnique = (strtoupper($element->getAttribute("unique")) == "TRUE");
    }
    
}

?>
