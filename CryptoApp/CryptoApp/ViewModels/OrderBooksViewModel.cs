using System;
using System.Collections.Generic;
using System.Text;
using Bittrex.Net.Objects;
using CryptoApp.Service;
using Prism.Commands;
using Prism.Mvvm;

namespace CryptoApp.ViewModels
{
    public class OrderBooksViewModel : BindableBase
    {
        private readonly ExchangeService _exchangeService;

        public OrderBooksViewModel(ExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
            GetOrderBooksCommand.ObservesProperty(() => SelectedMarket);

        }
        private DelegateCommand _getMarketCommand;

        public DelegateCommand GetMarketsCommand
        {
            get
            {
                return _getMarketCommand ?? (_getMarketCommand = new DelegateCommand(
                           async () =>
                           {
                               Markets = await _exchangeService.GetMarketsAsync();
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
                           () => SelectedMarket != null));
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }



    }
}
