using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace CryptoApp.Service
{
    public static class ConnectionService
    {
        public static void ToggleNetwork()
        {
            NetworkAccess = NetworkAccess == NetworkAccess.Internet ? NetworkAccess.Local : NetworkAccess.Internet;
            ConnectivityChanged(new MyConnectivityChangedEventArgs(NetworkAccess));
        }

        public static NetworkAccess NetworkAccess { get; set; } = NetworkAccess.Internet;
        public static event MyConnectivityChangedEventHandler ConnectivityChanged = delegate { };
    }
    public delegate void MyConnectivityChangedEventHandler(MyConnectivityChangedEventArgs e);

    public class MyConnectivityChangedEventArgs : EventArgs
    {
        internal MyConnectivityChangedEventArgs(NetworkAccess access)
        {
            this.NetworkAccess = access;
        }
        public NetworkAccess NetworkAccess { get; }
    }
}
