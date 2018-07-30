using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bittrex.Net;
using CryptoApp.ViewModels;
using Xamarin.Forms;

namespace CryptoApp
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
		    BindingContext = new MainViewModel();
		}

       
    }
}
