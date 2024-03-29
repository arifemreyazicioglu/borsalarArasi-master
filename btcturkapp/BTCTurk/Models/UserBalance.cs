﻿namespace APIClient.Models
{
    public class UserBalance
    {
        public string Asset { get; set; }
        public string AssetName { get; set; }
        public decimal Balance { get; set; }
        public string Locked { get; set; }
        public string Free { get; set; }
        public override string ToString()
        {
            return /*$"Asset: {Asset}, AssetName: {AssetName}, Balance:*/ Balance.ToString() /*Locked: {Locked}, Free: {Free}"*/;
        }
    }
}
