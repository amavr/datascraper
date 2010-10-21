<?php

class ProxyList
{
    private static $instance = null;
	// записываются строки в формате: "ip:port"
    public $proxies = array();

    public static function getInstance ( )
    {
        if ( self::$instance == null )
        {
            self::$instance = new ProxyList ( );
        }
        return self::$instance;
    }

	public function HasProxy()
	{
		return count($this->proxies) > 0;
	}

	public function Load($FileName)
	{
		if(file_exists($FileName))
		{
			$s = file_get_contents($FileName);
			$s = str_replace("\r", "", $s);
			$this->proxies = explode("\n", $s);
		}
		else
		{
			trigger_error("Proxy file $FileName not exists");
		}
	}

	public function Current()
	{
		if($this->HasProxy())
			return $this->proxies[0];
		else
			return FALSE;
	}

	public function Next()
	{
		if($this->HasProxy())
			return array_shift($this->proxies);
		else
			return FALSE;
	}
    
    function __construct()
    {
	    $this->vars = array();
    }
}

?>
