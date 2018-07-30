using System;
using System.Collections.Generic;
using System.Text;
using Bittrex.Net.Objects;
using Prism.Mvvm;
using Xamarin.Forms;

namespace CryptoApp.Model
{
    public class MarketSummary : BindableBase
    {
        private string _marketName;

        public string MarketName
        {
            get { return _marketName; }
            set
            {
                if (value != _marketName)
                {
                    _marketName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private Decimal? _high;

        public Decimal? High
        {
            get { return _high; }
            set
            {
                if (value != _high)
                {
                    _high = value;
                    RaisePropertyChanged();
                }
            }
        }

        private Decimal? _low;

        public Decimal? Low
        {
            get { return _low; }
            set
            {
                if (value != _low)
                {
                    _low = value;
                    RaisePropertyChanged();
                }
            }
        }


        private Decimal? _volume;

        public Decimal? Volume
        {
            get { return _volume; }
            set
            {
                if (value != _volume)
                {
                    _volume = value;
                    RaisePropertyChanged();
                }
            }
        }



        private Decimal? _last;

        public Decimal? Last
        {
            get { return _last; }
            set
            {
                if (value != _last)
                {
                    _last = value;
                    RaisePropertyChanged();
                }
            }
        }

        private DateTime _timeStamp;

        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set
            {
                if (value != _timeStamp)
                {
                    _timeStamp = value;
                    RaisePropertyChanged();
                }
            }
        }

        private Decimal _bid;

        public Decimal Bid
        {
            get { return _bid; }
            set
            {
                if (value != _bid)
                {
                    _bid = value;
                    RaisePropertyChanged();
                }
            }
        }

        private Decimal _ask;

        public Decimal Ask
        {
            get { return _ask; }
            set
            {
                if (value != _ask)
                {
                    _ask = value;
                    RaisePropertyChanged();
                }
            }
        }

        internal void UpdateSummary(BittrexStreamMarketSummary summary)
        {
            Ask = summary.Ask;
            Bid = summary.Bid;
            High = summary.High;
            Low = summary.Low;
            Last = summary.Last;
            MarketName = summary.MarketName;
            TimeStamp = summary.TimeStamp;
            Volume = summary.Volume;
        }

        internal void UpdateSummary(BittrexMarketSummary summary)
        {
            Ask = summary.Ask;
            Bid = summary.Bid;
            High = summary.High;
            Low = summary.Low;
            Last = summary.Last;
            MarketName = summary.MarketName;
            TimeStamp = summary.TimeStamp;
            Volume = summary.Volume;
        }
        internal static MarketSummary CreateSummary(BittrexStreamMarketSummary summary)
        {
            return new MarketSummary
            {
                Ask = summary.Ask,
                Bid = summary.Bid,
                High = summary.High,
                Low = summary.Low,
                Last = summary.Last,
                MarketName = summary.MarketName,
                TimeStamp = summary.TimeStamp,
                Volume = summary.Volume
            };
        }



        internal static MarketSummary CreateSummary(BittrexMarketSummary summary)
        {
            return new MarketSummary
            {
                Ask = summary.Ask,
                Bid = summary.Bid,
                High = summary.High,
                Low = summary.Low,
                Last = summary.Last,
                MarketName = summary.MarketName,
                TimeStamp = summary.TimeStamp,
                Volume = summary.Volume
            };
        }

    }
}
