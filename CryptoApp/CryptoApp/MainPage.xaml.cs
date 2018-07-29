using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryptoApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            Appearing += MainPage_Appearing;
		}

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            int counter = 0;
            Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1)).
               ObserveOn(SynchronizationContext.Current).Subscribe(l => Label.Text += (++counter).ToString());
        }
    }
}
