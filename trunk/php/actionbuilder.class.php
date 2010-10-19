<?php

include 'vartable.class.php';
include 'scriptaction.class.php';
include 'scripttextaction.class.php';
include 'scriptdateaction.class.php';
include 'scriptdownloadaction.class.php';
include 'scriptsetvaraction.class.php';
include 'scriptgetvaraction.class.php';
include 'scriptloadaction.class.php';
include 'scriptsaveaction.class.php';
include 'scriptreplaceaction.class.php';
include 'scriptfindaction.class.php';
include 'scriptnexturlaction.class.php';
include 'scriptlogger.class.php';


class ActionBuilder
{
	private $logger;
	private $xmldoc = "";

	public $actions = array();
	public $debugMode = false;

	public function setLogName($LogFileName)
	{
		$this->logger = new ScriptLogger($LogFileName);
	}

	public function execute()
	{
		$this->logger->Open();
		$num = count($this->actions);
		$i = 0;
		foreach($this->actions as $action)
		{
			$i++;
			$action->execute(true, sprintf("%d/%d", $i, $num));
		}
		$this->logger->Close();
	}

	public function load($file_name)
	{
		$this->xmldoc = new DOMDocument();
		$this->xmldoc->load($file_name);
		$root = $this->xmldoc->documentElement;
		$this->actions = $this->makeChilds($root);
	}

	private function makeChilds($parent)
	{
		$list = array();
		$i = 0;
		foreach ($parent->childNodes as $item)
		{
			if($item->nodeType != 1) continue;

			$action = $this->makeAction($item);
			if($action)
			{
				$list[$i] = $action;
				$i++;
			}
		}
		return $list;
	}

	private function makeAction($element)
	{
		$action = null;

		$type = $element->getAttribute("type");
		switch($type)
		{
			case "text":
				$action = new ScriptTextAction();
				break;

			case "date":
				$action = new ScriptDateAction();
				break;
				
			case "download":
				$action = new ScriptDownloadAction();
				break;
			
			case "load":
				$action = new ScriptLoadAction();
				break;

			case "save":
				$action = new ScriptSaveAction();
				break;

			case "set-var":
				$action = new ScriptSetVarAction();
				break;

			case "get-var":
				$action = new ScriptGetVarAction();
				break;

			case "find":
				$action = new ScriptFindAction();
				break;

			case "replace":
				$action = new ScriptReplaceAction();
				break;

			case "next-page":
				$action = new ScriptNextURLAction();
				break;
		}

		if($action)
		{
			$action->setLogger($this->logger);
			$action->debugMode = $this->debugMode;
			$action->load($element);
			$action->childs = $this->makeChilds($element);
		}

		return $action;
	}

	public function write()
	{
		print $this->xmldoc->saveXML();
	}
}

?>
