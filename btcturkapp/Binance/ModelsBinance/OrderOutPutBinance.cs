using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.ModelsBinance
{
    public class OrderOutputBinance
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

		//public override string ToString()
  //      {
  //          return $" Id:{Id},\n Datetime: {Datetime},\n Type: {Type},\n Method: {Method},\n Price: {Price},\n Amount: {Amount},\n Quantity: {Quantity},\n PairSymbol: {PairSymbol},\n PairSymbolNormalized: {PairSymbolNormalized}";
  //      }
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
