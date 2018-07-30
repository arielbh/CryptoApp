using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bittrex.Net;
using Bittrex.Net.Objects;
using CryptoApp.Model;
using CryptoApp.Service;
using CryptoExchange.Net;
using Prism.Commands;
using ReactiveUI;
using Xamarin.Forms;

namespace CryptoApp.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private readonly ExchangeService _exchangeService = new ExchangeService();

        public MainViewModel()
        {
            OrderBooksViewModel = new OrderBooksViewModel(_exchangeService);
            MarketsViewModel = new MarketsViewModel(_exchangeService);
        }

        public MarketsViewModel MarketsViewModel { get; }
        public OrderBooksViewModel OrderBooksViewModel { get; }

    }
}
