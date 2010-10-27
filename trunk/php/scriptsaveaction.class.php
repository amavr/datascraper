<?php

class ScriptSaveAction extends ScriptAction
{
	public $fname = "";
	public $enc = "";
	public $adddata = false;
	public $backflow = true;
	
	protected function executeInternal($deep)
	{
		$this->oData = $this->iData;

		if(!$this->backflow) $this->write($this->iData);

		if($deep)
			$this->executeChild();
		else
			$this->bData = $this->oData;

		if($this->backflow) $this->write($this->bData);
	}


	protected function getAttributes($element)
	{
		$this->fname = $element->getAttribute("file");
		$this->fname = str_replace("/", DIRECTORY_SEPARATOR, $this->fname);
		$this->fname = str_replace("\\", DIRECTORY_SEPARATOR, $this->fname);
		$this->enc = $element->getAttribute("encoding");
		$this->adddata = (strtoupper($element->getAttribute("append")) == "TRUE") ? true : false;
		$this->backflow = (strtoupper($element->getAttribute("ascending")) == "TRUE") ? true : false;
	}

	private function write($data)
	{
		$data = iconv($this->def_encoding, $this->enc, $data);
		
		if($this->adddata)
			file_put_contents($this->fname, $data, FILE_APPEND);
		else
			file_put_contents($this->fname, $data);
	}

}

?>
