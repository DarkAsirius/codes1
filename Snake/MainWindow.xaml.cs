using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Rectangle[,] rect = new Rectangle[10, 10];
        private int[,] math = new int[10, 10];
        private int x, y = 0;
        public static Random rnd = new Random();
        private int HeadX = 1;
        private int HeadY = 1;
        private int hor = 0;
        private int vert = 0;
        private int ex = rnd.Next(10);
        private int ey = rnd.Next(10);
        private int TailX = 1;
        private int TailY = 0;
        private string[,] dir = new string[10, 10];

        private void start_Click(object sender, RoutedEventArgs e)
        {
            rest.Visibility = Visibility.Visible;
            stop.Visibility = Visibility.Visible;

            start.Visibility = Visibility.Hidden;

            //Поле
            for (int i = 0; i < 10;  i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    rect[i, j] = new Rectangle();
                    rect[i, j].Width = 40;
                    rect[i, j].Height = 40;
                    rect[i, j].Fill = Brushes.Green;
                    rect[i, j].VerticalAlignment = VerticalAlignment.Top;
                    rect[i, j].HorizontalAlignment = HorizontalAlignment.Left;
                    rect[i, j].Margin = new Thickness(x, y, 0, 0);
                    GridMap.Children.Add(rect[i, j]);
                    x += 50;
                    dir[HeadY, HeadX] = "";
                    dir[TailY, TailX] = "Down";
                }
                y += 50;
                x = 0;
            }
            //Таймер
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(250);
            timer.Start();
            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Tick += Timer2_Tick;
            timer2.Interval = TimeSpan.FromMilliseconds(10);
            timer2.Start();
        }



        private void Timer2_Tick(object sender, EventArgs e)
        {
            
            //Математика
            math[HeadY, HeadX] = 1;
            math[ey, ex] = 2;
            math[TailY, TailX] = 1;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (math[i, j] == 1)
                    {
                        rect[i, j].Fill = Brushes.Pink;
                    }
                    else if (math[i, j] == 2)
                    {
                        rect[i, j].Fill = Brushes.Red;
                    }
                    else if (math[i, j] == 0)
                    {
                        rect[i, j].Fill = Brushes.Green;
                    }
                }
            }
        }

        private void rest_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D  || e.Key == Key.Right)
            {
                hor = 1;
                vert = 0;
            }
            else if (e.Key == Key.A || e.Key == Key.Left)
            {
                hor = -1;
                vert = 0;
            }
            else if (e.Key == Key.S || e.Key == Key.Down)
            {
                hor = 0;
                vert = 1;
            }
            else if (e.Key == Key.W || e.Key == Key.Up)
            {
                hor = 0;
                vert = -1;
            }
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (hor == 1)
            {
                dir[HeadY, HeadX] = "Right";
                HeadX++;
            }
            if (hor == -1)
            {
                dir[HeadY, HeadX] = "Left";
                HeadX--;
            }
            if (vert == -1)
            {
                dir[HeadY, HeadX] = "Up";
                HeadY--;
            }
            if (vert == 1)
            {
                dir[HeadY, HeadX] = "Down";
                HeadY++;
            }



            if (dir[TailY, TailX] == "Right")
            {
                if (math[HeadY,HeadX] == math[ey,ex])
                {
                    ey = rnd.Next(10);
                    ex = rnd.Next(10);
                }
                else
                {
                    math[TailY, TailX] = 0;
                    dir[TailY, TailX] = "";
                    TailX++;
                }
            }
            else if (dir[TailY,TailX] == "Left")
            {
                if (math[HeadY,HeadX] == math[ey,ex])
                {
                    ey = rnd.Next(10);
                    ex = rnd.Next(10);
                }
                else
                {
                    math[TailY, TailX] = 0;
                    dir[TailY, TailX] = "";
                    TailX--;
                }
            }
            else if (dir[TailY,TailX] == "Down")
            {
                if (math[HeadY, HeadX] == math[ey, ex])
                {
                    ey = rnd.Next(10);
                    ex = rnd.Next(10);
                }
                else
                {
                    math[TailY, TailX] = 0;
                    dir[TailY, TailX] = "";
                    TailY++;
                }
            }
            else if (dir[TailY,TailX] == "Up")
            {
                if (math[HeadY, HeadX] == math[ey, ex])
                {
                    ey = rnd.Next(10);
                    ex = rnd.Next(10);
                }
                else
                {
                    math[TailY, TailX] = 0;
                    dir[TailY, TailX] = "";
                    TailY--;
                }
            }
        }
    }
}
