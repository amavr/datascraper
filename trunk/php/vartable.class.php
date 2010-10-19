<?php

class VarTable
{
    private static $instance = null;
    public $vars = array();

    public static function getInstance ( )
    {
        if ( self::$instance == null )
        {
            self::$instance = new VarTable ( );
        }
        return self::$instance;
    }

	public static function ParseVariables($string)
	{
		$a = self::getInstance()->vars;
		$patt = "!\{(\D[^\}]*)\}!si";
		preg_match_all($patt, $string, $mm);
		for($i = 0; $i < count($mm[1]); $i++)
		{
			if(array_key_exists($mm[1][$i], $a))
				$string = str_replace("{".$mm[1][$i]."}", $a[$mm[1][$i]], $string);
		}
		return $string;
	}


    
    function __construct()
    {
	    $this->vars = array();
    }
}

?>
