using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_Simulator.Classes
{
    public abstract class Animal : INotifyPropertyChanged
    {
        private float health = 100;
        private bool isAlive = true;

        public float Health
        {
            get { return health; }
            set 
            { 
                health = value;
                OnPropertyChanged(nameof(Health));
            }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set 
            { 
                isAlive = value;
                OnPropertyChanged(nameof(IsAlive));
            }
        }

        public void ReduceHealth(int amount)
        {
            Health -= amount;
        }

        public void Feed(int amount)
        {
            Health += amount;

            if (Health > 100)
            {
                Health -= Health % 100;
            }

            OnPropertyChanged(nameof(Health));
        }

        public abstract void CheckAlive();

        #region INotifyPropertyChanged Members

        // INotifyPropertyChanged event for auto refreshing changes to UI
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}
