<?php

// include 'ScriptLogger.php';

abstract class ScriptAction
{
	public $debugMode = false;
	public $name = "";
	public $type = "";
	public $childs = array();
	
	public $work_dir = "";

	public $iData = "";
	public $oData = "";
	public $bData = "";

	public $logger;
	private $info = "";
	
	protected $def_encoding = "UTF-8";

	abstract protected function executeInternal($deep);
	abstract protected function getAttributes($element);

	public function execute($deep, $info)
	{
		$this->info = $info;
		//$this->trace(sprintf("%s (%s)", $info, $this->type));

		$this->oData = "";
		$this->bData = "";
		try
		{
			if($this->debugMode)
			{
				$msg  ="\n--------- BEG iData ---------";
				$msg .= $this->name;
				$msg .="\n".$this->iData;
				$msg .="\n--------- END iData ---------";
				$this->trace($msg);
			}
			
			$this->executeInternal($deep);
			
			if($this->debugMode)
			{
				$msg  ="\n--------- BEG oData ---------";
				$msg .= $this->name;
				$msg .="\n".$this->oData;
				$msg .="\n--------- END oData ---------";
				$this->trace($msg);
			}
			
			return true;
		}
		catch(Exception $e)
		{
			// необработаннае исключение имеет код = 1
			if($e->getCode() == 0) throw $e;

			$msg = $e->getMessage();
			$msg .= "\naction type: %s";
			$msg .= "\naction name: %s";
			$msg .= "\n------ BEGIN InputData --------";
			$msg .= "\n%s";
			$msg .= "\n-------- END InputData --------";

			$msg = sprintf($msg, $this->type, $this->name, $this->iData);
			$this->trace($msg);

			throw new Exception($e->getMessage(), 0);
		}
	}

	public function executeChild()
	{
		$num = count($this->childs);
		if ($num > 0)
		{
			$i = 0;
			foreach ($this->childs as $action)
			{
				$action->iData = $this->oData;
				$i++;
				$info = sprintf($this->getActionInfoInternal(), $this->info, $this->type, $i, $num);
				$action->execute(true, $info);
				$this->bData .= $action->bData;
			}
		}
		else
			$this->bData .= $this->oData;
	}

	protected function getActionInfoInternal()
	{
		return "%s-%s(%d/%d)";
	}

	public function load($element)
	{
		$this->name = $element->getAttribute("name");
		$this->type = $element->getAttribute("type");
		$this->getAttributes($element);
		// echo("\nload: " . $this->name);
	}

	public function setLogger($Logger)
	{
		$this->logger = $Logger;
	}

	public function trace($msg)
	{
		if($this->logger != null)
		$this->logger->Write($msg);
	}
}


?>
