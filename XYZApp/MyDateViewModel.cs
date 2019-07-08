using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace XYZApp
{
    public class MyDateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        DateTime dateTime = DateTime.Now; 
        public DateTime DateTime
        {
            get
            {
                return dateTime; 
            }
            private set
            {
                if(dateTime != value)
                {
                    dateTime = value;
                    PropertyChangedEventHandler handler = PropertyChanged;
                    if(handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("DateTime");
                    }
                }
            }
        }

        public MyDateViewModel()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
        }

        private bool OnTimerTick()
        {
            DateTime = DateTime.Now;
            return true;
        }
    }
}
