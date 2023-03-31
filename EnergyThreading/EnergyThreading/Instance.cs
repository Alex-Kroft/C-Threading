using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.System;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading;
using Windows.Globalization;

namespace EnergyThreading
{
    public class Instance
    {
        private Stopwatch stopwatch;
        private City city;
        public float totalDemand;
        private Frame frame;
        Object locker = new object();


        public Instance(Frame frame, int amountOfHouses, float availableSupply)
        {
            this.frame = frame;
            CompositionTarget.Rendering += OnCompositionTargetRendering;
            this.city = new City(amountOfHouses, true, availableSupply);
            this.totalDemand = city.totalDemand;

        }
        public City getCity { get { return city; } }
        private void OnCompositionTargetRendering(object sender, object e)
        {
            draw();
            city.calculateTotalDemand();
        }

        public void update()
        {
            city.calculateTotalDemand();
            city.distributeEnergyToHouses();
            totalDemand = city.totalDemand;

        }

        public void initialize()
        {
            city.calculateTotalDemand();
            totalDemand = city.totalDemand;
        }

        public void draw()
        {
            Canvas canvas = new Canvas();
            canvas.Width = frame.ActualWidth;
            canvas.Height = frame.ActualHeight;

            int housesPerRow = 25;
            int houseCount = city.getHouses().Count;

            if (houseCount > 10000) { housesPerRow = 400; }
            else if (houseCount > 7500) { housesPerRow = 300; }
            else if (houseCount > 5000) { housesPerRow = 200; }
            else if (houseCount > 2500) { housesPerRow = 100; }

            int rowCount = (int)Math.Ceiling((double)houseCount / housesPerRow);

            double rowHeight = canvas.Height / rowCount;
            double columnWidth = canvas.Width / housesPerRow;

            int currentRow = 0;
            int currentColumn = 0;

            foreach (House house in city.getHouses())
            {
                lock (this.locker)
                {
                    Rectangle rect = new Rectangle();
                    rect.Height = rowHeight - 1;
                    rect.Width = columnWidth - 1;

                    if (house.isSatisfied())
                    {
                        rect.Fill = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        rect.Fill = new SolidColorBrush(Colors.Red);
                    }

                    Canvas.SetLeft(rect, currentColumn * columnWidth);
                    Canvas.SetTop(rect, currentRow * rowHeight);
                    canvas.Children.Add(rect);

                    currentColumn++;
                }
                if (currentColumn == housesPerRow)
                {
                    currentRow++;
                    currentColumn = 0;
                }
            }

            this.frame.Content = canvas;
        }

       public void timeCounter()
        {

        }

        public void getRunPerformance()
        {

        }
    }
}
