using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EnergyThreading
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Instance instance;
        public TimeSpan _timeOfDay;

        public MainPage()
        {
            this.InitializeComponent();
            instance = new Instance(MyFrame);
            _timeOfDay = checkTime();
        }
        public void amountOfHouses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int houseAmount = (int)e.AddedItems[0];
        }

        public TimeSpan checkTime()
        {
            DateTime _startStopwatch;
            _startStopwatch = DateTime.Now;
            TimeSpan elapsed = DateTime.Now - _startStopwatch;
            if(timePicker.SelectedTime != null)
            {
                TimeSpan currentTimeOfDay = (TimeSpan)(elapsed + timePicker.SelectedTime);
                return currentTimeOfDay;
            }
            else
            {
                return TimeSpan.Zero;
            }
            
        }
        
        public async Task button_Click_singlethread()
        {
            if(instance.getCity.getSingleThread == false)
            {
                instance.getCity.setSingleThread(true);
            }
            while (instance.getCity.getSingleThread)
            {
                await Task.Delay(100);
            }
        }

        public async Task button_Click_multithread()
        {
            if (instance.getCity.getSingleThread == true)
            {
                instance.getCity.setSingleThread(false);
            }
            while (!instance.getCity.getSingleThread)
            {
                await Task.Delay(100);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
