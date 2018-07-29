using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bittrex.Net;
using Bittrex.Net.Objects;
using CryptoApp.Model;
using CryptoExchange.Net;
using Prism.Commands;
using ReactiveUI;
using Xamarin.Forms;

namespace CryptoApp.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private DelegateCommand _getMarketCommand;

        public DelegateCommand GetMarketsCommand
        {
            get
            {
                return _getMarketCommand ?? (_getMarketCommand = new DelegateCommand(
                           async () =>
                           {
                               using (var client = new BittrexClient())
                               {
                                   var result = await client.GetMarketsAsync();
                                   if (result.Success)
                                   {
                                       Markets = result.Data;
                                   }
                               }
                           }
                           ));
            }
        }

        private BittrexMarket[] _markets;

        public BittrexMarket[] Markets
        {
            get => _markets;
            set
            {
                if (value != _markets)
                {
                    _markets = value;
                    OnPropertyChanged();
                }
            }
        }

        private BittrexMarket _selectedMarket;

        public BittrexMarket SelectedMarket
        {
            get => _selectedMarket;
            set
            {
                if (value != _selectedMarket)
                {
                    _selectedMarket = value;
                    OnPropertyChanged();
                }
            }
        }

        private DelegateCommand _getSummariesCommand;

        public DelegateCommand GetSummariesCommand
        {
            get
            {
                return _getSummariesCommand ?? (_getSummariesCommand = new DelegateCommand(
                           async () =>
                           {
                               using (var client = new BittrexClient())
                               {
                                   var result = await client.GetMarketSummariesAsync();
                                   if (result.Success)
                                   {
                                       CreateOrUpdate(result.Data);
                                   }
                               }
                           }));
            }
        }

        private void CreateOrUpdate(BittrexMarketSummary[] resultData)
        {
            if (Summaries == null)
            {
                Summaries = resultData.ToDictionary(d => d.MarketName, MarketSummary.CreateSummary);
            }

            foreach (var summary in resultData)
            {
                if (Summaries.ContainsKey(summary.MarketName))
                {
                    Summaries[summary.MarketName].UpdateSummary(summary);
                }
            }
        }

        private void CreateOrUpdate(List<BittrexStreamMarketSummary> resultData)
        {
            if (Summaries == null)
            {
                Summaries = resultData.ToDictionary(d => d.MarketName, MarketSummary.CreateSummary);
            }

            foreach (var summary in resultData)
            {
                if (Summaries.ContainsKey(summary.MarketName))
                {
                    Summaries[summary.MarketName].UpdateSummary(summary);
                }
            }
        }

    

        
        private DelegateCommand _subscribeSummariesCommand;

        public DelegateCommand SubscribeSummariesCommand
        {
            get
            {
                return _subscribeSummariesCommand ?? (_subscribeSummariesCommand = new DelegateCommand(
                           async () =>
                           {
                               var client = new BittrexSocketClient();
                               {
                                   var result = await client.SubscribeToMarketSummariesUpdateAsync(data =>
                                   {
                                       CreateOrUpdate(data);
                                   });
                           
                               }
                           }));
            }
        }

 


        private Dictionary<string, MarketSummary> _summaries;

        public Dictionary<string, MarketSummary> Summaries
        {
            get => _summaries;
            set
            {
                if (value != _summaries)
                {
                    _summaries = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
