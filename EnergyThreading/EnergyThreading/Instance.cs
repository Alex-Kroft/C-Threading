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
            this.totalDemand = city.total;

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
            totalDemand = city.total;

        }

        public void initialize()
        {
            city.calculateTotalDemand();
            totalDemand = city.total;
        }

        public void draw()
        {
            Canvas canvas = new Canvas();
            canvas.Width = frame.ActualWidth;
            canvas.Height = frame.ActualHeight;

            int housesPerRow = 30;
            if (city.getHouses().Count > 500) { housesPerRow = 50; }

            int houseCount = city.getHouses().Count;
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
                    rect.Height = rowHeight - 10;
                    rect.Width = columnWidth - 10;

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
