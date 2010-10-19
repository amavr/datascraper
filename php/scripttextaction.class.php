<?php

class ScriptTextAction extends ScriptAction
{
    public $text = "";

    protected function executeInternal($deep)
    {
        $this->oData = VarTable::ParseVariables($this->text);
        if($deep)
            $this->executeChild();
        else
            $this->bData = $this->oData;
    }

    protected function getAttributes($element)
    {
        $this->text = $element->getAttribute("text");
    }
}

?>
