<?php

class ScriptSetVarAction extends ScriptAction
{
    public $child_flow = false;
	public $break_flow = false;

	protected function executeInternal($deep)
    {
		// —квозной поток
		$this->oData = $this->iData;

		// ≈сли указан родительский (нисход€щий или input) поток,
		// то охран€ем в пам€ти его
		if($this->child_flow == false)
		{
			VarTable::getInstance()->vars[$this->name] = $this->iData;
			if($this->break_flow) 
				$this->oData = "";
		}

        if($deep)
            $this->executeChild();
        else
            $this->bData = $this->oData;

		// ≈сли указан детский (восход€щий или back) поток,
		// то охран€ем в пам€ти его
		if($this->child_flow == true)
		{
			VarTable::getInstance()->vars[$this->name] = $this->bData;
			if($this->break_flow) 
				$this->bData = "";

		}
    }

    protected function getAttributes($element)
    {
		$this->child_flow = (strtoupper($element->getAttribute("ascending")) == "TRUE") ? true : false;
		$this->break_flow = (strtoupper($element->getAttribute("break")) == "TRUE") ? true : false;
    }
}

?>
