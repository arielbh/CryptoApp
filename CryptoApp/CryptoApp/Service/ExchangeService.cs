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
            return Observable.Create<BittrexMarketSummary[]>(async observer =>
            {
                using (var client = new BittrexClient())
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        if (ConnectionService.NetworkAccess != NetworkAccess.Internet)
                        {
                            throw new Exception("Fake: No Internet");
                        }
                        var result = await client.GetMarketSummariesAsync();
                        if (result.Success)
                        {
                            observer.OnNext(result.Data);
                            observer.OnCompleted();
                        }
                        else
                        {
                            observer.OnError(new Exception(result.Error.Message));
                        }
                    }
                    catch (Exception e)
                    {
                        observer.OnError(e);
                    }
                }
            });
        }

        public IObservable<BittrexOrderBookEntry[]> GetBuyOrderBooksUpdates(
            string marketName)
        {
            return Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1)).Select(_ =>
            {
                using (var client = new BittrexClient())
                {
                    var result = client.GetBuyOrderBook(marketName);
                    if (result.Success)
                    {
                        return result.Data;
                    }

                    return null;
                }
            });

        }

        public IObservable<BittrexOrderBookEntry[]> GetSellOrderBooksUpdates(
            string marketName)
        {
            return Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1)).Select(_ =>
            {
                using (var client = new BittrexClient())
                {
                    var result = client.GetSellOrderBook(marketName);
                    if (result.Success)
                    {
                        return result.Data;
                    }

                    return null;
                }
            }).Buffer(TimeSpan.FromSeconds(5)).Select(r => r.SelectMany(list => list).Distinct().ToArray());
        }
    }
}
