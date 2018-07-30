using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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
            if (ConnectionService.NetworkAccess != NetworkAccess.Internet) return null;
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
            if (ConnectionService.NetworkAccess != NetworkAccess.Internet) return null;
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
            if (ConnectionService.NetworkAccess != NetworkAccess.Internet) return null;
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
            if (ConnectionService.NetworkAccess != NetworkAccess.Internet) return null;
            var client = new BittrexSocketClient();
            {
                return await client.SubscribeToMarketSummariesUpdateAsync(callback);
            }
        }
        public IObservable<BittrexMarketSummary[]> GetMarketSummariesObservable()
        {
            return Observable.Empty<BittrexMarketSummary[]>();
        }

        public IObservable<BittrexOrderBookEntry[]> GetBuyOrderBooksUpdates(
            string marketName)
        {
            return Observable.Empty<BittrexOrderBookEntry[]>();
        }

        public IObservable<BittrexOrderBookEntry[]> GetSellOrderBooksUpdates(
            string marketName)
        {
            return Observable.Empty<BittrexOrderBookEntry[]>();
        }
    }
}
