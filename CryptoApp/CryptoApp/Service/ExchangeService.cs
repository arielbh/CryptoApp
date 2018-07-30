using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bittrex.Net;
using Bittrex.Net.Objects;
using CryptoExchange.Net;
using Xamarin.Essentials;

namespace CryptoApp.Service
{
    public class ExchangeService
    {
        public async Task<BittrexMarket[]> GetMarketsAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) return null;
            using (var client = new BittrexClient())
            {
                var result = await client.GetMarketsAsync();
                if (result.Success)
                {
                    return result.Data;
                }

                return null;
            }
                
        }

        public async Task<BittrexMarketSummary[]> GetMarketSummariesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) return null;
            using (var client = new BittrexClient())
            {
                var result = await client.GetMarketSummariesAsync();
                if (result.Success)
                {
                    return result.Data;
                }

                return null;
            }
        }

        public async Task<BittrexOrderBook> GetOrderBooksAsync(string market)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) return null;
            using (var client = new BittrexClient())
            {
                var result = await client.GetOrderBookAsync(market);
                if (result.Success)
                {
                    return result.Data;
                }

                return null;
            }
        }

        public async Task<CallResult<int>> SubscribeToMarkets(Action<List<BittrexStreamMarketSummary>> callback)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) return null;
            var client = new BittrexSocketClient();
            {
                return await client.SubscribeToMarketSummariesUpdateAsync(callback);
            }
        }
    }
}
