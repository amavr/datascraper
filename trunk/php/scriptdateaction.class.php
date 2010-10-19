<?php

class ScriptDateAction extends ScriptAction
{
	private $format = "d.m.Y";
	private $period = "DAY";
	private $offset = 0;
	private $align = true;

	protected function executeInternal($deep)
	{
		$this->oData = $this->calcDate();
		if($deep)
			$this->executeChild();
		else
			$this->bData = $this->oData;
	}

	private function calcDate()
	{
		$dt = mktime(0, 0, 0, date("m"), date("d"), date("Y"));
		$days = 0;
		switch($this->period)
		{
			case "DAY":
				$dt = mktime(0, 0, 0, date("m"), date("d") + $this->offset, date("Y"));
				break;
				
			case "WEEK":
				$dt = mktime(0, 0, 0, date("m"), date("d") + $this->offset * 7, date("Y"));
				if($this->align)
				{
					$dw = getdate($dt);
					$days = $dw["wday"] - 1;
				}
				break;
				
			case "MONTH":
				$dt = mktime(0, 0, 0, date("m") + $this->offset, date("d"), date("Y"));
				if($this->align)
				{
					$dw = getdate($dt);
					$days = $dw["mday"] - 1;
				}
				break;
		}
		
		$date = getdate($dt);
		return date($this->format, mktime(0, 0, 0, $date["mon"], $date["mday"] - $days, $date["year"]));
	}

	protected function getAttributes($element)
	{
		$this->format = $element->getAttribute("format_php");
		$this->period = $element->getAttribute("period");
		$this->offset = $element->getAttribute("offset");
		$this->align = (strtoupper($element->getAttribute("align")) == "TRUE") ? true : false;
	}
}

?>
