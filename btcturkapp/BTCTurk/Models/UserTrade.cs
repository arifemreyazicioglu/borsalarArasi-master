﻿namespace APIClient.Models
{
    public class UserTrade
    {
        public long OrderId { get; set; }
        public decimal Price { get; set; }
        public string NumeratorSymbol { get; set; }
        public string DenominatorSymbol { get; set; }
        public string OrderType { get; set; }
        public long Timestamp { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal Tax { get; set; }

        public override string ToString()
        {
            return $"Id: {OrderId}, Price: {Price}, " +
                   $" OrderType: {OrderType}, Timestamp: {Timestamp}, Amount: {Amount}, Fee: {Fee}, Tax: {Tax}";
        }
    }
}
