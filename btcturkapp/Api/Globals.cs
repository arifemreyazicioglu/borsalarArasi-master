using System;


namespace TradingBot
{
	public static class Globals
	{

		public static string[] keys = System.IO.File.ReadAllLines(@"/Users/arife/Desktop/masaüstü/kripto/abritajBinance/abritajBinance/bin/Debug/net5.0-windows/apikeys.txt");

		//Api Key
		public static string ApiKey { get; set; } = "WYGMa28CkeNnltbwYm9ukA6YV9m4hH85jevS93rSctXJSRpXx5vRgOtFFheeOc0r";

		//Secret Key
		public static string SecretKey { get; set; } = "EvZenamVBwar05FxIo8qKPfJn7XoHUbK4iN6gaTzdoN92S8AQHcPEVtBpjjkrBQo";

		//Currency1
		public static string C1 = "USDT";

		//Currency2 - should be a trade option that exists for Currency1, such as BTC or ETH
		public static string C2 = "BTC";

		//Minimum quantity relies on currency traded
		public static double quatityPerTrade = 5;

		//Percentage threshold for trade to execute
		public static double percentageChange = 0.5;

		//Interval after which algorithm should run
		public static double timerInterval = 0.3;  //in minutes

		//Set to true to make a test order. 
		//Console will write trade details of last real trade if test
		public static bool testCase = true;

	}
}
