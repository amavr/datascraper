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
		$patt = "!\{(\D[^\}]*)\}!siu";
		preg_match_all($patt, $string, $mm, PREG_SET_ORDER);
		for($i = 0; $i < count($mm); $i++)
		{
			if(array_key_exists($mm[$i][1], $a))
			{
				// $string = mb_ereg_replace("!{".$mm[1][$i]."}!", $a[$mm[1][$i]], $string);
				$string = str_replace("{".$mm[$i][1]."}", $a[$mm[$i][1]], $string);
			}
		}
		return $string;
	}


    
    function __construct()
    {
	    $this->vars = array();
    }
}

?>
