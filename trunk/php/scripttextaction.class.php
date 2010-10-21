<?php

class ScriptTextAction extends ScriptAction
{
    public $text = "";

    protected function executeInternal($deep)
    {
        $this->oData = VarTable::ParseVariables(iconv("utf-8", "windows-1251", $this->text));
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
