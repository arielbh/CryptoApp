using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using Bittrex.Net.Objects;
using CryptoApp.Service;
using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Essentials;

namespace CryptoApp.ViewModels
{
    public class OrderBooksViewModel : BindableBase
    {
        private BittrexMarket[] _fullList;
        private readonly ExchangeService _exchangeService;

        public OrderBooksViewModel(ExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
            GetOrderBooksCommand.ObservesProperty(() => SelectedMarket);
            GetOrderBooksCommand.ObservesProperty(() => Connection);
            GetMarketsCommand.ObservesProperty(() => Connection);
        }


        public NetworkAccess Connection => Connectivity.NetworkAccess;

        private DelegateCommand _getMarketCommand;

        public DelegateCommand GetMarketsCommand
        {
            get
            {
                return _getMarketCommand ?? (_getMarketCommand = new DelegateCommand(
                           async () =>
                           {
                               _fullList = await _exchangeService.GetMarketsAsync();
                               Markets = _fullList.ToArray();
                           }
                       , () => Connection == NetworkAccess.Internet));
            }
        }

        private string _filterText;

        public string FilterText
        {
            get => _filterText;
            set
            {
                if (value != _filterText)
                {
                    _filterText = value;
                    RaisePropertyChanged();
                    Filter(FilterText);
                }
            }
        }

        private void Filter(string filterText)
        {
            if (_fullList == null) return;
            Markets = _fullList.Where(m => m.MarketName.Contains(filterText)).ToArray();
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
                    RaisePropertyChanged();
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
                    RaisePropertyChanged();
                }
            }
        }

        private DelegateCommand _getOrderBooksCommand;

        public DelegateCommand GetOrderBooksCommand
        {
            get
            {
                return _getOrderBooksCommand ?? (_getOrderBooksCommand = new DelegateCommand(
                           async () =>
                           {
                               var data = await _exchangeService.GetOrderBooksAsync(SelectedMarket.MarketName);
                               Buy = data.Buy;
                               Sell = data.Sell;

                           },
                           () => SelectedMarket != null && Connection == NetworkAccess.Internet));
            }
        }

        private List<BittrexOrderBookEntry> _buy;

        public List<BittrexOrderBookEntry> Buy
        {
            get { return _buy; }
            set
            {
                if (value != _buy)
                {
                    _buy = value;
                    RaisePropertyChanged();
                }
            }
        }

        private List<BittrexOrderBookEntry> _sell;

        public List<BittrexOrderBookEntry> Sell
        {
            get { return _sell; }
            set
            {
                if (value != _sell)
                {
                    _sell = value;
                    RaisePropertyChanged();
                }
            }
        }



    }
}
