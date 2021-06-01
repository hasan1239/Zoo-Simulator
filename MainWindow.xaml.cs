using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Zoo_Simulator.Classes;

namespace Zoo_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Simulator sim;
        public Thread simThread;

        public Simulator Simulator
        {
            get { return sim; }
            set { sim = value; }
        }

        public MainWindow()
        {
            Simulator = new Simulator();

            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Simulator.SimulationStarted = true;
            //Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);
            simThread = new Thread(delegate () { Simulator.StartSimulation(); });
            simThread.Start();
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

        private void btnFeed_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Zoo.FeedAnimals();
        }

        private void parentWindow_Closing(object sender, CancelEventArgs e)
        {
            if (simThread != null && simThread.IsAlive)
            {
                simThread.Abort();
            }
        }
    }

    #region Converters

    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool || value is bool?)
            {
                if ((bool)value)
                {
                    return Visibility.Visible;
                }
                else return Visibility.Collapsed;
            }
            else return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return true;
        }
    }

    public class ReverseVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if (value is bool)
                {
                    if ((bool)value)
                    {
                        return Visibility.Collapsed;
                    }
                    else return Visibility.Visible;
                }
                else if (value is Visibility)
                {
                    if ((Visibility)value == Visibility.Visible)
                    {
                        return Visibility.Collapsed;
                    }
                    else return Visibility.Visible;
                }

                return value;
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return true;
        }
    }

    #endregion
}
