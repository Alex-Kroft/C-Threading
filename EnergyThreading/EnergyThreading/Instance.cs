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

namespace EnergyThreading
{
    public class Instance
    {
        private Stopwatch stopwatch;
        private City city;
        public float totalDemand;
        private Frame frame;
        Object locker = new object();


        public Instance(Frame frame)
        {
            this.frame = frame;
            CompositionTarget.Rendering += OnCompositionTargetRendering;
            this.city = new City(100, true);
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
            totalDemand = 0;
            city.calculateTotalDemand();
            totalDemand = city.total;
        }

        public void draw()
        {
            Canvas canvas = new Canvas();
            canvas.Width = frame.ActualWidth;
            canvas.Height = frame.ActualHeight;

            int housesPerRow = 30;
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

                    /*if (city.getHouses().Count <= 50 )
                    {
                        rect.Width = columnWidth - 10;
                        
                    } else if (city.getHouses().Count > 50 && city.getHouses().Count <= 100)
                    {
                        rect.Width = (columnWidth - 10) / 2;
                    } else
                    {
                        rect.Width = (columnWidth - 10) / 3;
                    }*/

                    /**
                    ImageBrush brush = new ImageBrush();
                    
                    if (house.isSatisfied())
                    {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/house_green.png"));
                        rect.Fill = brush;
                    } else {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/house_red.png"));
                       rect.Fill = brush;
                    } **/

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
