using Binance.BinanceV1;
using Binance.ModelsBinance;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace btcturkapp.BinanceFunctions
{

    public class binanceFunctions
    {
       
        public async Task<string> BinanceGetValueAsync(string curr)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("apikeys.json").Build();
            var publicKey = configuration["publicKey"];
            var privateKey = configuration["privateKey"];
            var resourceUrl = configuration["resourceUrl"];

            var binanceV1 = new BinanceV1(publicKey, privateKey, resourceUrl);
            var tickerList = await binanceV1.GetTicker(curr);

            return tickerList.ToString();
        }
    }
}
