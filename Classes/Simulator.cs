using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_Simulator.Classes
{
    public class Simulator : INotifyPropertyChanged
    {
        private Zoo zoo;
        private bool simulationStarted = false, isSimRunning = true;
        private int hoursElapsed = 0;

        int cycleTime = 20;
        DateTime lastTime, simTime;
        double elapsedTime;

        public Zoo Zoo
        {
            get { return zoo; }
            set
            {
                zoo = value;
                OnPropertyChanged(nameof(Zoo));
            }
        }

        public bool SimulationStarted
        {
            get { return simulationStarted; }
            set
            {
                simulationStarted = value;
                OnPropertyChanged(nameof(SimulationStarted));
            }
        }

        public bool IsSimRunning
        {
            get { return isSimRunning; }
            set
            {
                isSimRunning = value;
                OnPropertyChanged(nameof(IsSimRunning));
            }
        }

        public string HoursElapsedString
        {
            get { return simTime.ToString(); }
        }

        public int HoursElapsed
        {
            get { return hoursElapsed; }
            set
            {
                hoursElapsed = value;
                OnPropertyChanged(nameof(HoursElapsed));
                OnPropertyChanged(nameof(HoursElapsedString));
            }
        }

        public Simulator()
        {
            Zoo = new Zoo();
        }

        public void StartSimulation()
        {
            lastTime = DateTime.Now;
            simTime = DateTime.Now;
            simTime = simTime.AddMinutes(-simTime.Minute);
            simTime = simTime.AddSeconds(-simTime.Second);
            OnPropertyChanged(nameof(HoursElapsedString));

            while (IsSimRunning)
            {
                elapsedTime = DateTime.Now.Subtract(lastTime).TotalSeconds;

                if (elapsedTime > cycleTime)
                {
                    HoursElapsed++;
                    simTime = simTime.AddHours(1);

                    UpdateZoo();
                    CheckSimFinished();

                    lastTime = DateTime.Now;
                }
            }
        }

        public void UpdateZoo()
        {
            Zoo.UpdateAnimals();
        }

        public void CheckSimFinished()
        {
            if (Zoo.AllAnimalsDead)
            {
                IsSimRunning = false;
                OnPropertyChanged(nameof(Zoo.AllAnimalsDead));
            }
        }

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
