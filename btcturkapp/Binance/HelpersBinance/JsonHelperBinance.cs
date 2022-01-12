using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Binance.ModelsBinance;
using Newtonsoft.Json;

namespace Binance.HelpersBinance
{
    public static class JsonHelperBinance
    {
        public static TickerBinance ToReturnModelTickerBinance<T>(this HttpResponseMessage response) where T : class
        {
            TickerBinance returnModel;

            
            var result = response.Content.ReadAsStringAsync().Result;
              
            try
            {
                returnModel = JsonConvert.DeserializeObject<TickerBinance>(result);
            }
            catch (Exception e)
            {               
                Console.WriteLine(e);
                var message = "Cannot deserialize response to ReturnModel: \n" + result;
                throw new Exception(message);
            }
            return returnModel;
        }
        public static AccountInformation ToReturnModelBalanceBinance<T>(this HttpResponseMessage response) where T : class
        {
            AccountInformation returnModel;

            var result = response.Content.ReadAsStringAsync().Result;

            try
            {
                returnModel = JsonConvert.DeserializeObject<AccountInformation>(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var message = "Cannot deserialize response to ReturnModel: \n" + result;
                throw new Exception(message);
            }
            return returnModel;
        }
        

        public static HttpContent ToHttpContent(this object o)
        {
            var content = JsonConvert.SerializeObject(o);

            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("apikeys/json");

            return byteContent;
        }
    }
}

