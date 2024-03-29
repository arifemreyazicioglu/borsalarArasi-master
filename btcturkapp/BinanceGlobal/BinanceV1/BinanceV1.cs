﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;
using Binance.ModelsBinance;
using Binance.HelpersBinance;
using System.Linq;
using btcturkapp.Binance.ModelsBinance;


namespace Binance.BinanceV1
{
    public class BinanceV1 : IBinanceClient
    {
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly string _resourceUrlBinance;
        private readonly string _resourceUrlBinanceTr;

        public BinanceV1(string publicKey, string privateKey, string resourceUrlBinance, string resourceUrlBinanceTr)
        {
            _publicKey = publicKey;
            _privateKey = privateKey;
            _resourceUrlBinance = resourceUrlBinance;
            _resourceUrlBinanceTr = resourceUrlBinanceTr;
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
        }

        /// <summary>
        /// Cancels order with given order Id
        /// </summary>
        /// <returns>True if order was cancelled, false otherwise</returns>
        //public async Task<bool> CancelOrder(long id)
        //{
        //    var requestUrl = $"api/v1/order?id={id}";

        //    var response = await SendRequest(HttpVerbsBinance.Delete, requestUrl, requiresAuthentication: true);

        //    var returnModel =  response.ToReturnModel<string>();

        //    return returnModel.Success;
        //}


        ///// <summary>
        ///// Creates given Order. Requires authentication.
        ///// </summary>
        ///// <param name="orderInput">Order to be created</param>
        ///// <returns>An object of OrderOutPut for the created order information</returns>
        ///

        public async Task<CreateOrderBinance> CreateOrderBinance(string symbol, string side, decimal quantity, decimal price)
        {
            string requestUrl = $"{_resourceUrlBinanceTr}api/v1/orders";

            string query = $"symbol={symbol}&side={side}&type=LIMIT&timeInForce=GTC&quantity=8&price=14&timestamp={GetStamp()}";

            var signature =  getSignature(_privateKey, query);
            query += "&signature=" + signature;

            requestUrl += "?" + query;
            var response = await WebRequest(requestUrl, "POST", _publicKey);

            var returnModel = JsonConvert.DeserializeObject<CreateOrderBinance>(response);

            return returnModel;
        }

        public async Task<CancelOrderBinance> CancelOrderBinance(long id,string symbol)
        {
            string requestUrl = $"{_resourceUrlBinanceTr}api/v3/order";

            string query = $"symbol={symbol}&timestamp={GetStamp()}&orderId={id}";
      
            var signature = getSignature(_privateKey, query);
            query += "&signature=" + signature;

            requestUrl += "?" + query;

            var response = await  WebRequest(requestUrl, "DELETE", _publicKey);

            var returnModel = JsonConvert.DeserializeObject<CancelOrderBinance>(response);

            return returnModel;
        }

        /// <summary>
        /// Get the authenticated account's balances
        /// </summary>
        /// <returns>A list of type UserBalance for each currency. Null if account balance cannot be retreived </returns>
        public async Task<ReturnModelBinance<IList<UserBalanceBinance>>> GetBalances(string symbol)
        {
            string requestUrl = $"/open/v1/account/spot";

            string query = "";
            query = $"timestamp={GetStamp()}";

            var signature = getSignature(_privateKey, query);
            query += "&signature=" + signature;

            requestUrl += "?" + query;
            var response = await SendRequest(HttpVerbsBinance.Get, requestUrl,_resourceUrlBinanceTr, requiresAuthentication : true);

            var returnModel = response.ToReturnModelBinance<IList<UserBalanceBinance>>();

            return returnModel;

        }

        ///// <summary>
        ///// Gets the daily open, high, low, close, average etc. data in the market 
        ///// </summary>
        ///// <param name="pairSymbol">pair symbol</param>
        ///// <param name="last">The number of days to request</param>
        ///// <returns>The OHLC data for the last given number of days</returns>
        //public async Task<ReturnModelBinance<IList<OHLCBinance>>> GetDailyOhlc(string pairSymbol, int last)
        //{
        //    var requestUrl = $"api/v2/ohlc?pairSymbol={pairSymbol}&last={last}";

        //    var response = await SendRequest(HttpVerbsBinance.Get, requestUrl);

        //    var returnModel = response.ToReturnModel<IList<OHLCBinance>>();

        //    return returnModel;
        //}

        //public async Task<ReturnModelBinance<IList<UserTradeBinance>>> GetUserTrades(string[] types, string[] symbols, long startDate, long endDate)
        //{
        //    var typeBuilder = new StringBuilder();
        //    foreach (var type in types)
        //    {
        //        typeBuilder.Append($"type={type}&");
        //    }

        //    var symbolBuilder = new StringBuilder();

        //    foreach (var symbol in symbols)
        //    {
        //        symbolBuilder.Append($"symbol={symbol}&");
        //    }
        //    var requestUrl = $"api/v1/users/transactions/trade?{typeBuilder}{symbolBuilder}startDate={startDate}&endDate={endDate}";

        //    var response = await SendRequest(HttpVerbsBinance.Get, requestUrl,requiresAuthentication: true);

        //    var returnModel =  response.ToReturnModel<IList<UserTradeBinance>>();

        //    return returnModel;
        //}

        //public async Task<ReturnModelBinance<IList<UserTransactionBinance>>> GetUserFiatTransactions(string[] types, string[] symbols, long startDate, long endDate)
        //{
        //    var typeBuilder = new StringBuilder();
        //    foreach (var type in types)
        //    {
        //        typeBuilder.Append($"type={type}&");
        //    }

        //    var symbolBuilder = new StringBuilder();

        //    foreach (var symbol in symbols)
        //    {
        //        symbolBuilder.Append($"symbol={symbol}&");
        //    }

        //    var requestUrl = $"api/v1/users/transactions/fiat?{typeBuilder}{symbolBuilder}startDate={startDate}&endDate={endDate}";

        //    var response = await SendRequest(HttpVerbsBinance.Get, requestUrl,requiresAuthentication: true);

        //    var returnModel = response.ToReturnModel<IList<UserTransactionBinance>>();

        //    return returnModel;
        //}

        //public async Task<ReturnModelBinance<IList<UserTransactionBinance>>> GetUserCryptoTransactions(string[] types, string[] symbols, long startDate, long endDate)
        //{
        //    var typeBuilder = new StringBuilder();
        //    foreach (var type in types)
        //    {
        //        typeBuilder.Append($"type={type}&");
        //    }

        //    var symbolBuilder = new StringBuilder();

        //    foreach (var symbol in symbols)
        //    {
        //        symbolBuilder.Append($"symbol={symbol}&");
        //    }

        //    var requestUrl = $"api/v1/users/transactions/crypto?{typeBuilder}{symbolBuilder}startDate={startDate}&endDate={endDate}";

        //    var response = await SendRequest(HttpVerbsBinance.Get, requestUrl,requiresAuthentication: true);

        //    var returnModel = response.ToReturnModel<IList<UserTransactionBinance>>();

        //    return returnModel;
        //}
        ///// <summary>
        ///// Get the last trades in the market by pairsymbol.
        ///// </summary>
        ///// <param name="pairSymbol">a pair symbol ex. BTCTRY</param>
        ///// <param name="numberOfTrades">The number of trades that will be requested.</param>
        ///// <returns>The requested number of last trades in the market.</returns>
        //public async Task<ReturnModelBinance<IList<TradesBinance>>> GetLastTrades(string pairSymbol, int numberOfTrades)
        //{
        //    var requestUrl = $"api/v1/trades?pairSymbol={pairSymbol}&last={numberOfTrades}";

        //    var response = await SendRequest(HttpVerbsBinance.Get, requestUrl);

        //    var returnModel = response.ToReturnModel<IList<TradesBinance>>();

        //    return returnModel;
        //}

        ///// <summary>
        ///// Get all open orders of the user
        ///// </summary>
        ///// <returns>Users open orders listed. Null if there was an error</returns>
        //public async Task<ReturnModelBinance<OpenOrderOutputBinance>> GetOpenOrders(string pairSymbol = null)
        //{
        //    var requestUrl = $"api/v1/openOrders?pairSymbol={pairSymbol}";

        //    var response = await SendRequest(HttpVerbsBinance.Get, requestUrl, requiresAuthentication: true);

        //    var returnModel = response.ToReturnModel<OpenOrderOutputBinance>();

        //    return returnModel;
        //}

        ///// <summary>
        ///// Gets orderbook by pair
        ///// </summary>
        ///// <param name="pairSymbol">pair symbol</param>
        ///// <param name="limit">number of returned orders for buy/sell</param>
        ///// <returns>OrderBook</returns>
        public async Task<OrderBookBinance> GetOrderBook(string pairSymbol, int limit)
        {
            var requestUrl = $"api/v3/depth?symbol={pairSymbol}&limit={limit}";

            var response = await SendRequest(HttpVerbsBinance.Get, requestUrl, _resourceUrlBinance);

            var returnModel = response.ToReturnModelOrderBookBinance<OrderBookBinance>();

            return returnModel;

        }

        ///// <summary>
        ///// Gets all pairs ticker values 
        ///// </summary>
        ///// <returns>List of ticker values</returns>
        //public async Task<ReturnModelBinance<IList<TickerBinance>>> GetTicker()
        //{
        //    var requestUrl = "api/v2/ticker";

        //    var response = await SendRequest(HttpVerbsBinance.Get, requestUrl);

        //    var returnModel = response.ToReturnModel<IList<TickerBinance>>();

        //    return returnModel;
        //}

        /// <summary>
        /// Gets ticker by pair
        /// </summary>
        /// <param name="pairSymbol">pair symbol</param>
        /// <returns>Ticker values</returns>
        public async Task<TickerBinance> GetTicker(string pairSymbol)
        {
            var requestUrl = $"api/v3/ticker/24hr?symbol={pairSymbol}";

            var response = await SendRequest(HttpVerbsBinance.Get, requestUrl,_resourceUrlBinance);

            var returnModel = response.ToReturnModelTickerBinance<TickerBinance>();

            return returnModel;

        }

        /// <summary>
        /// Generate a signature by giving timestamp
        /// </summary>
        /// <param name="stamp">long tickes for current time</param>
        /// <returns>string segnature</returns>
        private string getSignature(string SecretKey, string query)
        {
            Encoding encoding = Encoding.UTF8;
            var keyByte = encoding.GetBytes(SecretKey);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                hmacsha256.ComputeHash(encoding.GetBytes(query));
                return ByteToString(hmacsha256.Hash);
            }

        }
        public static string ByteToString(byte[] buff)   //from StackOverflow
        {
            string str = "";
            for (int i = 0; i < buff.Length; i++)
                str += buff[i].ToString("X2");
            return str;
        }

        private static long GetStamp()
        {
            var timeSt = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return timeSt;
        }

        ///// <summary>
        ///// Handles the response received from the application. You can change this method to have custom error handling in your app.
        ///// </summary>
        ///// <returns>Returns false if there were no errors. True if request failed.</returns>
        //private static bool RequestSucceeded(HttpResponseMessage response)
        //{
        //    var result = true;
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        Debug.WriteLine("Received error. Status code: " + response.StatusCode + ". Error message: " +
        //                        response.ReasonPhrase);
        //        result = false;
        //    }
        //    else
        //    {
        //        var json = response.Content.ReadAsStringAsync().Result;
        //        var obj = JsonConvert.DeserializeObject<dynamic>(json);
        //        if (obj is JObject && obj["error"] != null)
        //        {
        //            Debug.WriteLine("Received error. Status code: " + (obj["error"]["code"].ToString() as string) +
        //                            ". Error message: " + (obj["error"]["message"].ToString() as string));
        //            result = false;
        //        }
        //    }

        //    return result;
        //}

        private async Task<string> WebRequest(string requestUrl, string method, string ApiKey)
        {
            try
            {
                var request =  (HttpWebRequest)System.Net.WebRequest.Create(requestUrl);
                request.Method = method;
                request.Timeout = 1000;  //very long response time from Singapore. Change in Boston accordingly.
                if (ApiKey != null)
                {
                    request.Headers.Add("X-MBX-APIKEY", ApiKey);
                }

                var webResponse =  (HttpWebResponse)( await request.GetResponseAsync());
                if (webResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Did not return OK 200. Returned: {webResponse.StatusCode}");
                }

                var encoding = ASCIIEncoding.ASCII;
                string responseText = null;

                using (var reader = new System.IO.StreamReader(webResponse.GetResponseStream(), encoding))
                {
                    
                    responseText = reader.ReadToEnd();
                }
                
                return responseText;
            }
            catch (WebException webEx)
            {
                if (webEx.Response != null)
                {
                    Encoding encoding = ASCIIEncoding.ASCII;
                    using (var reader = new System.IO.StreamReader(webEx.Response.GetResponseStream(), encoding))
                    {
                        string responseText = reader.ReadToEnd();
                        throw new Exception(responseText);
                    }
                }
                throw;
            }
            catch
            {
                return "Error";
            }
        }

        private async Task<HttpResponseMessage> SendRequest(HttpVerbsBinance action, string url,string resourceUrl, object inputModel = null, bool requiresAuthentication = true)
        {

            HttpResponseMessage response = null;

            using (var client = new HttpClient { BaseAddress = new Uri(resourceUrl), Timeout = TimeSpan.FromSeconds(1000) })
            {
                if (requiresAuthentication)
                {
                    client.DefaultRequestHeaders.Add("X-MBX-APIKEY", _publicKey);
                    //var stamp = GetStamp();
                    //client.DefaultRequestHeaders.Add("X-Stamp", stamp.ToString(CultureInfo.InvariantCulture));
                    //var signature = getSignature(_privateKey, url);
                    //client.DefaultRequestHeaders.Add("X-Signature", signature);
                }

                try
                {
                    switch (action)
                    {
                        case HttpVerbsBinance.Post:
                            var postContent = inputModel.ToHttpContent();
                            response = await client.PostAsync(url, postContent);
                            break;
                        case HttpVerbsBinance.Get:

                            response = await client.GetAsync(url);
                            break;
                        case HttpVerbsBinance.Delete:

                            response = await client.DeleteAsync(url);
                            break;
                    }
                }

                catch (Exception ex)
                {

                    Debug.WriteLine(ex.Message);
                    response = null;
                }

                if (response == null)
                {
                    Console.WriteLine("Cannot get response from server.");
                    return null;
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine($"Token is unauthorized to do this action: [{action.ToString().ToUpper()}] /{url}. Please check your bearer token in request header.");
                }

                return response;
            }
        }

    }
}
