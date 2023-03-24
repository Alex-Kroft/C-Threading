﻿using System;
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
            CompositionTarget.Rendering += OnCompositionTargetRendering;
            this.city = new City(5, true);
        }

        private void OnCompositionTargetRendering(object sender, object e)
        {
            draw(); //or change to the update() for stuff?
        }

        public void update()
        {
           
        }

        public void initialize()
        {

        }

        public void draw()
        {
            Canvas canvas = new Canvas();
            canvas.Width = frame.ActualWidth;
            canvas.Height = frame.ActualHeight;

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

                rect.Fill = new SolidColorBrush(Colors.Red);

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

            this.frame.Content = canvas;
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
