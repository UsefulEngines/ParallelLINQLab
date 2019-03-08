using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HOL
{
    public class StatusItem : INotifyPropertyChanged
    {
        public StatusItem(string name, int qty, long time)
        {
            this.Name = name;
            this.QtyFound = qty;
            this.ElapsedTime = time;
        }

        private long _elapsedTime;
        private int _qtyFound;

        public String Name { get; set; }

        public long ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                if (value != _elapsedTime)
                {
                    _elapsedTime = value;
                    OnPropertyChanged("ElapsedTime");
                }
            }
        }

        public int QtyFound
        {
            get { return _qtyFound; }
            set
            {
                if (value != _qtyFound)
                {
                    _qtyFound = value;
                    OnPropertyChanged("QtyFound");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }

}
