<?php

class CookieSorage
{
    private static $instance = null;
    public $cookies = "";

    public static function getInstance ( )
    {
        if ( self::$instance == null )
        {
            self::$instance = new CookieSorage ( );
        }
        return self::$instance;
    }

	public static function ParseCookie($content)
	{
		preg_match_all('|Set-Cookie: (.*);|U', $content, $results);
		self::getInstance()->cookies = implode(';', $results[1]);
	}

    function __construct()
    {
	    
    }
}

?>
