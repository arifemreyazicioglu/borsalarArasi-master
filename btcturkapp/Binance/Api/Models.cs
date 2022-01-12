using System.Collections.Generic;

namespace TradingBot.Models
{
	public class ServerTime
	{
		public long Time { get; set; }
	}

	public class SymbolPrice
	{
		public string Symbol { get; set; }
		public double Price { get; set; }
	}

	public class SymbolTicker
	{
		public string Symbol { get; set; }
		public double BidPrice { get; set; }
		public double BidQty { get; set; }
		public double AskPrice { get; set; }
		public double AskQty { get; set; }
	}

	public class Ticker24hr
	{
		public string Symbol { get; set; }
		public double PriceChange { get; set; }
		public double PriceChangePercent { get; set; }
		public double WeightedAvgPrice { get; set; }
		public double PrevClosePrice { get; set; }
		public double LastPrice { get; set; }
		public double LastQty { get; set; }
		public double BidPrice { get; set; }
		public double BidQty { get; set; }
		public double AskPrice { get; set; }
		public double AskQty { get; set; }
		public double OpenPrice { get; set; }	
		public double HighPrice { get; set; }
		public double LowPrice { get; set; }
		public double Volume { get; set; }
		public double QuoteVolume { get; set; }
		public long OpenTime { get; set; }
		public long CloseTime { get; set; }
		public long FirstId { get; set; }
		public long LastId { get; set; }
		public long Count { get; set; }
	}



    public class AccountInformation
	{
		public double MakerCommission { get; set; }
		public double TakerCommission { get; set; }
		public double BuyerCommission { get; set; }
		public double SellerCommission { get; set; }
		public bool CanTrade { get; set; }
		public bool CanWithdraw { get; set; }
		public bool CanDeposit { get; set; }
		public List<AssetBalances> Balances { get; set; }
	}

	public class AssetBalances
	{
		public string Asset { get; set; }
		public double Free { get; set; }
		public double Locked { get; set; }
        public AssetBalances Balances { get; internal set; }
    }

	public class Trades
	{
		public long Id { get; set; }
		public long OrderId { get; set; }
		public double Price { get; set; }
		public double Qty { get; set; }
		public string Commission { get; set; }
		public string commissionAsset { get; set; }
		public long Time { get; set; }
		public bool isBuyer { get; set; }
		public bool isMaker { get; set; }
		public bool isBestMatch { get; set; }
	}

	public class Order
	{
		public string Symbol { get; set; }
		public long OrderId { get; set; }
		public string ClientOrderid { get; set; }
		public double Price { get; set; }
		public double OrigQty { get; set; }
		public string ExecutedQty { get; set; }
		public OrderStatuses Status { get; set; }
		public TimesInForce TimeInForce { get; set; }
		public OrderTypes Type { get; set; }
		public OrderSides Side { get; set; }
		public double StopPrice { get; set; }
		public double IcebergQty { get; set; }
		public long Time { get; set; }

		public override string ToString()
		{
			return $"Id: {OrderId}, Price: {Price}, Quantity: {OrigQty}";
		}
	}


	public enum OrderStatuses
	{
		NEW,
		PARTIALLY_FILLED,
		FILLED,
		CANCELED,
		PENDING_CANCEL,
		REJECTED,
		EXPIRED,
	}
	public enum OrderTypes
	{
		LIMIT,
		MARKET,
		STOP_LOSS,
		STOP_LOSS_LIMIT,
		TAKE_PROFIT,
		TAKE_PROFIT_LIMIT,
		LIMIT_MAKER
	}
	public enum OrderSides
	{
		BUY,
		SELL
	}
	public enum TimesInForce
	{
		GTC,
		IOC,
		FOK
	}
}
