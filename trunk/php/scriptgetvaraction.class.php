<?php

class ScriptGetVarAction extends ScriptAction
{
    protected function executeInternal($deep)
    {
		$this->oData = VarTable::getInstance()->vars[$this->name];

        if($deep)
            $this->executeChild();
        else
            $this->bData = $this->oData;
    }

    protected function getAttributes($element)
    {
    }
}

?>
