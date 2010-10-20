<?php

class ScriptCookieAction extends ScriptAction
{
    public $text = "";

    protected function executeInternal($deep)
    {
        $cookies = VarTable::ParseVariables($this->text);
        CookieSorage::getInstance()->cookies = $cookies;
        
        // file_put_contents(ScriptLoadAction::$cookie_file_name, $cooks);
        
        $this->oData = $this->iData;
        
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
