<?php

class ScriptSetVarAction extends ScriptAction
{
    public $child_flow = false;
	public $break_flow = false;

	protected function executeInternal($deep)
    {
		// �������� �����
		$this->oData = $this->iData;

		// ���� ������ ������������ (���������� ��� input) �����,
		// �� �������� � ������ ���
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

		// ���� ������ ������� (���������� ��� back) �����,
		// �� �������� � ������ ���
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
