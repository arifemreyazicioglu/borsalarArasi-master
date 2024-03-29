﻿using BinanceTR.Business.Abstract;
using BinanceTR.Core.Results.Abstract;
using BinanceTR.Core.Results.Concrete;
using BinanceTR.Core.Utilities;
using BinanceTR.Models;
using BinanceTR.Models.Account;
using BinanceTR.Models.Common;
using BinanceTR.Models.Enums;
using BinanceTR.Models.Order;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceTR.Business.Concrete
{
    public class BinanceTrManager : IBinanceTrService
    {
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly string _resourceUrlBinance;
        private readonly string _resourceUrlBinanceTr;

        public BinanceTrManager(string publicKey, string privateKey, string resourceUrlBinance, string resourceUrlBinanceTr)
        {
            _publicKey = publicKey;
            _privateKey = privateKey;
            _resourceUrlBinance = resourceUrlBinance;
            _resourceUrlBinanceTr = resourceUrlBinanceTr;
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
        }
        private async Task<string> SendRequestAsync(HttpMethod method, string url, Dictionary<string, string> parameters = null, CancellationToken ct = default)
        {

            try
            {
                var httpClient = new HttpClient();
                var requestUri = BinanceTrHelper.GetRequestUrl(url, true);
                var requestMessage = new HttpRequestMessage(method, requestUri);
                requestMessage.Headers.Add("X-MBX-APIKEY", _publicKey);

                if (method == HttpMethod.Get)
                    requestMessage.RequestUri = new Uri(requestMessage.RequestUri.OriginalString + BinanceTrHelper.CreateQueryString(BinanceTrHelper.BuildRequest(_privateKey, parameters)));
                else
                    requestMessage.Content = new FormUrlEncodedContent(BinanceTrHelper.BuildRequest(_privateKey, parameters));

                var response = await httpClient.SendAsync(requestMessage, ct).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static async Task<string> SendRequestWithoutAuth(string url, Dictionary<string, string> parameters = null, bool baseUrl = false, CancellationToken ct = default)
        {
            try
            {
                var httpClient = new HttpClient();
                var requestUri = BinanceTrHelper.GetRequestUrl(url, baseUrl);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
                requestMessage.RequestUri = new Uri(requestMessage.RequestUri.OriginalString + BinanceTrHelper.CreateQueryString(BinanceTrHelper.BuildRequest(null, parameters)));

                var response = await httpClient.SendAsync(requestMessage, ct).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string CheckResult(string result)
        {
            if (result != "[]" && result != "" && result.Contains("msg"))
            {
                if (result.StartsWith("["))
                {
                    var messages = "";
                    JsonSerializer.Deserialize<List<ErrorModel>>(result).ForEach(p => messages += p.Msg + "\n");
                    result = messages;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<ErrorModel>(result);
                    return error.Code > 0 ? error.Msg : result;
                }
            }
            return result;
        }

        public async Task<IDataResult<long>> TestConnectivityAsync(CancellationToken ct = default)
        {
            try
            {
                var result = await SendRequestWithoutAuth("/open/v1/common/time", null, true, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                var model = JsonSerializer.Deserialize<TimeModel>(data);
                return new SuccessDataResult<long>(model.Timestamp, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<long>(ex.Message);
            }
        }

        public async Task<IDataResult<List<SymbolDataList>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            try
            {
                var result = await SendRequestWithoutAuth("/open/v1/common/symbols", null, true, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                var model = JsonSerializer.Deserialize<SymbolModel>(data);
                return new SuccessDataResult<List<SymbolDataList>>(model.SymbolData.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<SymbolDataList>>(ex.Message);
            }
        }

        public async Task<IDataResult<OrderBookData>> GetOrderBookAsync(string symbol, int limit = 100, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()}
                };
                var result = await SendRequestWithoutAuth("/open/v1/market/depth", parameters, true, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<OrderBookData>(data);

                var model = JsonSerializer.Deserialize<OrderBookModel>(result);
                return new SuccessDataResult<OrderBookData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OrderBookData>(ex.Message);
            }
        }

        public async Task<IDataResult<List<RecentTradesModel>>> GetRecentTradesAsync(string symbol, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()}
                };
                var result = await SendRequestWithoutAuth("/trades", parameters, ct: ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<RecentTradesModel>>(data);

                var model = JsonSerializer.Deserialize<List<RecentTradesModel>>(result);
                return new SuccessDataResult<List<RecentTradesModel>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<RecentTradesModel>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<AggregateTradesModel>>> GetAggregateTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()}
                };

                if (startTime.HasValue && endTime.HasValue)
                {
                    parameters["startTime"] = BinanceTrHelper.GetTimestamp(startTime.Value).ToString();
                    parameters["endTime"] = BinanceTrHelper.GetTimestamp(endTime.Value).ToString();
                }

                var result = await SendRequestWithoutAuth("/aggTrades", parameters, ct: ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<AggregateTradesModel>>(data);

                var model = JsonSerializer.Deserialize<List<AggregateTradesModel>>(result);
                return new SuccessDataResult<List<AggregateTradesModel>>(model);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<AggregateTradesModel>>(ex.Message);
            }
        }

        public async Task<IDataResult<string>> GetKlinesAsync(string symbol, KLineIntervalEnum interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString()},
                    { "interval", interval.GetDisplayName()},
                };

                if (startTime.HasValue && endTime.HasValue)
                {
                    parameters["startTime"] = BinanceTrHelper.GetTimestamp(startTime.Value).ToString();
                    parameters["endTime"] = BinanceTrHelper.GetTimestamp(endTime.Value).ToString();
                }

                var result = await SendRequestWithoutAuth("/klines", parameters, ct: ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<string>(data);

                return new SuccessDataResult<string>(data);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<string>(ex.Message);
            }
        }

        public async Task<IDataResult<List<AccountAsset>>> GetAccountInformationAsync(CancellationToken ct = default)
        {
            try
            {
                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/account/spot", ct: ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<AccountAsset>>(data);

                var model = JsonSerializer.Deserialize<AccountInformationModel>(result);
                return new SuccessDataResult<List<AccountAsset>>(model.AccountData.AccountAssets, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<AccountAsset>>(ex.Message);
            }
        }

        public async Task<IDataResult<AssetInformationData>> GetAssetIformationAsync(string assetName, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "asset", assetName }
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/account/spot/asset", parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<AssetInformationData>(data);

                var model = JsonSerializer.Deserialize<AssetInformationModel>(result);
                return new SuccessDataResult<AssetInformationData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AssetInformationData>(ex.Message);
            }
        }

        public async Task<IDataResult<OrderDetailData>> PostNewLimitOrderAsync(string symbol, OrderSideEnum side, decimal origQuoteQty, string price, CancellationToken ct = default)
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", side.GetDisplayName()},
                    { "type", OrderTypeEnum.LIMIT.GetDisplayName() },
                    { "quantity", origQuoteQty.ToString(CultureInfo.InvariantCulture) },
                    { "price", price.ToString()  }
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders", parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<OrderDetailData>(data);

                var model = JsonSerializer.Deserialize<OrderDetailModel>(result);
                return new SuccessDataResult<OrderDetailData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OrderDetailData>(ex.Message);
            }
        }

        public async Task<IDataResult<PostOrderModelData>> PostBuyMarketOrderAsync(string symbol, decimal origQty, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", OrderSideEnum.BUY.GetDisplayName() },
                    { "type", OrderTypeEnum.MARKET.GetDisplayName() },
                    { "quoteOrderQty", origQty.ToString(CultureInfo.InvariantCulture) },
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders", parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<PostOrderModelData>(data);

                var model = JsonSerializer.Deserialize<PostOrderModel>(result);
                return new SuccessDataResult<PostOrderModelData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostOrderModelData>(ex.Message);
            }
        }

        public async Task<IDataResult<PostOrderModelData>> PostSellMarketOrderAsync(string symbol, decimal origQuoteQty, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", OrderSideEnum.SELL.GetDisplayName() },
                    { "type", OrderTypeEnum.MARKET.GetDisplayName() },
                    { "quantity", origQuoteQty.ToString(CultureInfo.InvariantCulture) },
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders", parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<PostOrderModelData>(data);

                var model = JsonSerializer.Deserialize<PostOrderModel>(result);
                return new SuccessDataResult<PostOrderModelData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostOrderModelData>(ex.Message);
            }
        }

        public async Task<IDataResult<PostOrderModelData>> PostStopLimitOrderAsync(string symbol, OrderSideEnum side, decimal origQuoteQty, decimal limitPrice, decimal stopPrice, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", side.GetDisplayName() },
                    { "type", OrderTypeEnum.STOP_LOSS_LIMIT.GetDisplayName() },
                    { "quantity", origQuoteQty.ToString(CultureInfo.InvariantCulture) },
                    { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) },
                    { "price", limitPrice.ToString(CultureInfo.InvariantCulture) },
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders", parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<PostOrderModelData>(data);

                var model = JsonSerializer.Deserialize<PostOrderModel>(result);
                return new SuccessDataResult<PostOrderModelData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostOrderModelData>(ex.Message);
            }
        }

        public async Task<IDataResult<OrderDetailData>> GetOrderByIdAsync( long orderId, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "orderId", orderId.ToString() }
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/orders/detail", parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<OrderDetailData>(data);

                var model = JsonSerializer.Deserialize<OrderDetailModel>(result);
                return new SuccessDataResult<OrderDetailData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OrderDetailData>(ex.Message);
            }
        }

        public async Task<IDataResult<CancelOrderData>> CancelOrderByIdAsync(long orderId, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "orderId", orderId.ToString() }
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders/cancel",parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<CancelOrderData>(data);

                var model = JsonSerializer.Deserialize<CancelOrderModel>(result);
                return new SuccessDataResult<CancelOrderData>(model.Data, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CancelOrderData>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOrdersAsync( string symbol, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() }
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/orders", parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<OpenOrderList>>(data);

                var model = JsonSerializer.Deserialize<AllOrdersModel>(result);
                return new SuccessDataResult<List<OpenOrderList>>(model.Data.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenOrdersAsync( string symbol, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.All.GetDisplayName() },
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/orders", parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<OpenOrderList>>(data);

                var model = JsonSerializer.Deserialize<AllOrdersModel>(result);
                return new SuccessDataResult<List<OpenOrderList>>(model.Data.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenBuyOrdersAsync(string symbol, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.Open.GetDisplayName() },
                    { "side", OrderSideEnum.BUY.GetDisplayName() },
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/orders", parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<OpenOrderList>>(data);

                var model = JsonSerializer.Deserialize<AllOrdersModel>(result);
                return new SuccessDataResult<List<OpenOrderList>>(model.Data.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<List<OpenOrderList>>> GetAllOpenSellOrdersAsync( string symbol, int limit = 500, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "limit", limit.ToString() },
                    { "type", AllOrdersEnum.Open.GetDisplayName() },
                    { "side", OrderSideEnum.SELL.GetDisplayName() },
                };

                var result = await SendRequestAsync(HttpMethod.Get, "/open/v1/orders", parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<List<OpenOrderList>>(data);

                var model = JsonSerializer.Deserialize<AllOrdersModel>(result);
                return new SuccessDataResult<List<OpenOrderList>>(model.Data.List, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OpenOrderList>>(ex.Message);
            }
        }

        public async Task<IDataResult<OcoOrderData>> PostOcoOrderAsync(BinanceTrOptions options, string symbol, OrderSideEnum side, decimal quantity, decimal price, decimal stopPrice, decimal stopLimitPrice, CancellationToken ct = default)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "symbol", symbol },
                    { "side", side.GetDisplayName() },
                    { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                    { "price", price.ToString(CultureInfo.InvariantCulture) },
                    { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) },
                    { "stopLimitPrice", stopLimitPrice.ToString(CultureInfo.InvariantCulture) }
                };

                var result = await SendRequestAsync(HttpMethod.Post, "/open/v1/orders/oco",parameters, ct).ConfigureAwait(false);
                var data = CheckResult(result);
                if (!BinanceTrHelper.IsJson(data))
                    return new ErrorDataResult<OcoOrderData>(data);

                var model = JsonSerializer.Deserialize<OcoOrderModel>(result);
                return new SuccessDataResult<OcoOrderData>(model.OcoOrderData, model.Msg, model.Code);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OcoOrderData>(ex.Message);
            }
        }
    }
}
