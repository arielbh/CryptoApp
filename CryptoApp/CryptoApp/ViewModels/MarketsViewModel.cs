using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using Bittrex.Net;
using Bittrex.Net.Objects;
using CryptoApp.Model;
using CryptoApp.Service;
using Prism.Commands;
using Prism.Mvvm;

namespace CryptoApp.ViewModels
{
    public class MarketsViewModel : BindableBase
    {
        private readonly ExchangeService _exchangeService;

        public MarketsViewModel(ExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        private DelegateCommand _getSummariesCommand;

        public DelegateCommand GetSummariesCommand
        {
            get
            {
                return _getSummariesCommand ?? (_getSummariesCommand = new DelegateCommand(
                           async () =>
                           {
                               _exchangeService.GetMarketSummariesObservable().Retry(30).ObserveOn(SynchronizationContext.Current).
                                   Subscribe(CreateOrUpdate);
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
                               await _exchangeService.SubscribeToMarkets(CreateOrUpdate);
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
                    RaisePropertyChanged();
                }
            }
        }
    }
}
