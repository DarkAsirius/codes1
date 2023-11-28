using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Sh=System.Windows.Shapes;
using S=System.Drawing;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Ink;
using System.Diagnostics;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;

namespace Lessons
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
        static S.Bitmap bmp;
        System.Windows.Media.Brush brush;
        System.Windows.Media.Color px2;
        private System.Windows.Media.Color color1;
        S.Color color;
        S.Color colorsr2, colorO, colorP;
        public double xl, yl, w, h, xr, yr, xlr, xrl, xll;
        int count;
        double a, b, c;
        Ellipse ell;
        
        private void Getimage_Click(object sender, RoutedEventArgs e)
        {
            GetImage();
        }


        private void start_Click(object sender, RoutedEventArgs e)
        {
            
            CanvasColor();
            GetLeftPixel();
            GetRightPixel();
            Scan();
        }


        private void GetImage()
        {
            count = 0;
            Im1.Width = CanvasMap.Width;
            Im1.Height = CanvasMap.Height;
            OpenFileDialog OpenF = new OpenFileDialog();
            if (OpenF.ShowDialog() == true)
            {
                Im1.Stretch = Stretch.None;
                Im1.Source = new BitmapImage(new Uri(OpenF.FileName));
                bmp = new S.Bitmap(OpenF.FileName);
                T.Text = "";
                T1.Text = "";
                CanvasMap.Children.Remove(ell);
            }
        }

        public void GetLeftPixel()
        {
            for (int i = 0 ; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {   
                    count++;
                    color = bmp.GetPixel(j, i);
                    color1 = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
                    if(color1 != px2)
                    {
                        
                        xl = j;
                        yl = i;
                        colorsr2 = color;
                        return;
                    }
                }
            }
        }

        private void GetRightPixel()
        {
            for (int i = bmp.Height-1; i > 0; i--)
            {
                for(int j = bmp.Width-1; j > 0; j--)
                {
                    color = bmp.GetPixel(j, i);
                    color1 = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
                    count++;
                    if(color1 != px2)
                    {
                        xr = j;
                        yr = i;
                        colorsr2 = color;
                        return;
                    }
                }
            }
        }

        public void CanvasColor()
        {
            brush = CanvasMap.Background;
            px2 = ((System.Windows.Media.SolidColorBrush)(brush)).Color;
        }

        private void Scan()
        {
            a = xr;
            for (int i = 0; i < xr; i++)
            {
                count++;
                color = bmp.GetPixel((int)a, (int)yr);
                if(color != colorsr2)
                {
                    xrl = a;
                    break;
                }
                a--;
            }

            colorO = bmp.GetPixel((int)(((xr - xl) / 2) + xl), (int)((yr - yl) / 2 + yl));
            colorP = bmp.GetPixel((int)((xr - xl) / 2 + xl), (int)yr);
            if ((colorO != colorsr2) && (colorP == colorsr2))
            {
                string count1 = count.ToString();
                T.Visibility = Visibility.Visible;
                T1.Visibility = Visibility.Visible;
                T.Text = count1;
                T1.Text = "Буква О";
                return;
            }

            b = xrl;
            for (int i = 0; i < xrl; i++)
            { 
                count++;
                color = bmp.GetPixel((int)b, (int)yr);
                if (color == colorsr2)
                {
                    xlr = b;
                    break;
                }
                b--;
            }

            c = xlr;
            for(int i = 0; i < xlr; i++)
            {
                count++;
                color = bmp.GetPixel((int)c, (int)yr);
                if (color != colorsr2)
                {
                    xll = c;
                    break;
                }
                c--;
            }

            if (xr - xll > xr - xl)
            {
                string count1 = count.ToString();
                T.Visibility = Visibility.Visible;
                T1.Visibility = Visibility.Visible;
                T.Text = count1;
                T1.Text = "Буква Л";
                return;
            }

            if ((xr - xll == xr - xl))
            {
                string count1 = count.ToString();
                T.Visibility = Visibility.Visible;
                T1.Visibility = Visibility.Visible;
                T.Text = count1;
                T1.Text = "Буква П";
                return;
            }
            
        }

        //private void CreateRectangle()
        //{
        //    string count1 = count.ToString();
        //    double scalex = Im1.Width / Im1.Source.Width;
        //    double scaley = Im1.Height / Im1.Source.Height;

        //    w = (xr - xl) * scalex;
        //    h = (yr - yl) * scaley;

        //    rect = new System.Windows.Shapes.Rectangle();
        //    rect.Width = w;
        //    rect.Height = h;
        //    rect.HorizontalAlignment = HorizontalAlignment;
        //    rect.VerticalAlignment = VerticalAlignment;
        //    rect.Fill = System.Windows.Media.Brushes.Aqua;
        //    Canvas.SetLeft(rect, xl * scalex);
        //    Canvas.SetTop(rect, yl * scaley);
        //    CanvasMap.Children.Add(rect);
        //    T.Visibility = Visibility.Visible;
        //    T.Text = count1;
        //}

        /*
                double scalex = Im1.Width / Im1.Source.Width;
                double scaley = Im1.Height / Im1.Source.Height;
                ell = new Ellipse();
                ell.Height = 10;
                ell.Width = 10;
                ell.VerticalAlignment = VerticalAlignment;
                ell.HorizontalAlignment = HorizontalAlignment;
                ell.Fill = System.Windows.Media.Brushes.Aqua;
                Canvas.SetLeft(ell, (double)j * scalex);
                Canvas.SetTop(ell, (double)i * scaley);
                CanvasMap.Children.Add(ell);


                T.Visibility = Visibility.Visible;
                T.Text = text;
        */
    }
}
