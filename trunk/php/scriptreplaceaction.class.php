<?php

class ScriptReplaceAction extends ScriptAction
{
    public $pattern = "";
    public $replace = "";

    protected function executeInternal($deep)
    {
        $this->oData = preg_replace($this->pattern, $this->replace, $this->iData);
        
        if($deep)
            $this->executeChild();
        else
            $this->bData = $this->oData;
    }


    protected function getAttributes($element)
    {
        $this->pattern = "!".$element->getAttribute("pattern")."!siu";
        $this->replace = $element->getAttribute("replacement");
    }
}

?>
