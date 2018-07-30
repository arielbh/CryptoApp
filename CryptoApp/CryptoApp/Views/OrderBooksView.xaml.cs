using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderBooksView : ContentPage
	{
		public OrderBooksView ()
		{
			InitializeComponent ();
		}

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        ConnectionService.ToggleNetwork();

        }
	}
}