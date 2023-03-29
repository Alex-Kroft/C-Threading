using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace EnergyThreading
{
    public class Instance
    {
        private Stopwatch stopwatch;
        private City city;
        private Generator generator;
        private Frame frame;
       

        public Instance(Frame frame)
        {
            this.frame = frame;
        }

        private void update()
        {

        }

        private void initialize()
        {

        }
        public City getCity { get { return city; } }
        private void draw(Frame frame)
        {
            Canvas canvas = new Canvas();

            int housesPerRow = 5;
            int houseCount = city.getHouses().Count;
            int rowCount = (int)Math.Ceiling((double)houseCount / housesPerRow);

            double rowHeight = canvas.Height / rowCount;
            double columnWidth = canvas.Width / housesPerRow;

            int currentRow = 0;
            int currentColumn = 0;

            foreach (House house in city.getHouses())
            {
                Rectangle rect = new Rectangle();
                rect.Width = columnWidth - 10;
                rect.Height = rowHeight - 10;

                ImageBrush brush = new ImageBrush();
                if (house.isSatisfied())
                {
                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/house_green.svg"));
                    rect.Fill = brush;
                }
                else
                {
                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/house_red.svg"));
                    rect.Fill = brush;
                }

                Canvas.SetLeft(rect, currentColumn * columnWidth);
                Canvas.SetTop(rect, currentRow * rowHeight);
                canvas.Children.Add(rect);

                currentColumn++;
                if (currentColumn == housesPerRow)
                {
                    currentRow++;
                    currentColumn = 0;
                }
            }

            frame.Content = canvas; 
        }

       private void loadContent()
       {

       }

       public void timeCounter()
        {

        }

        public void getRunPerformance()
        {

        }
    }
}
