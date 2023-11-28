using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeGame
{
    public partial class MainWindow : Window
    {
        private const int SnakeSquareSize = 20;
        private const int InitialSnakeLength = 3;
        private const int SnakeSpeed = 200; // Milliseconds
        private readonly DispatcherTimer gameTimer;
        private readonly List<Rectangle> snake;
        private readonly Random random;
        private Rectangle food;
        private int directionX;
        private int directionY;
        private int snakeLength;

        public MainWindow()
        {
            InitializeComponent();

            gameTimer = new DispatcherTimer();
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromMilliseconds(SnakeSpeed);

            snake = new List<Rectangle>();
            random = new Random();

            StartGame();
        }

        private void StartGame()
        {
            GameCanvas.Children.Clear();
            snake.Clear();

            directionX = 1;
            directionY = 0;
            snakeLength = InitialSnakeLength;

            CreateSnake();
            CreateFood();

            gameTimer.Start();
        }

        private void CreateSnake()
        {
            for (int i = 0; i < snakeLength; i++)
            {
                Rectangle snakePart = new Rectangle
                {
                    Width = SnakeSquareSize,
                    Height = SnakeSquareSize,
                    Fill = Brushes.Green
                };

                Canvas.SetLeft(snakePart, i * SnakeSquareSize);
                Canvas.SetTop(snakePart, 0);

                GameCanvas.Children.Add(snakePart);
                snake.Add(snakePart);
            }
        }

        private void CreateFood()
        {
            int maxX = (int)GameCanvas.ActualWidth / SnakeSquareSize;
            int maxY = (int)GameCanvas.ActualHeight / SnakeSquareSize;

            int foodX = random.Next(0, maxX) * SnakeSquareSize;
            int foodY = random.Next(0, maxY) * SnakeSquareSize;

            food = new Rectangle
            {
                Width = SnakeSquareSize,
                Height = SnakeSquareSize,
                Fill = Brushes.Red
            };

            Canvas.SetLeft(food, foodX);
            Canvas.SetTop(food, foodY);

            GameCanvas.Children.Add(food);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            CheckCollision();
        }

        private void MoveSnake()
        {
            double headLeft = Canvas.GetLeft(snake[0]);
            double headTop = Canvas.GetTop(snake[0]);

            double newLeft = headLeft + (directionX * SnakeSquareSize);
            double newTop = headTop + (directionY * SnakeSquareSize);

            // Create a new head
            Rectangle newHead = new Rectangle
            {
                Width = SnakeSquareSize,
                Height = SnakeSquareSize,
                Fill = Brushes.Green
            };

            Canvas.SetLeft(newHead, newLeft);
            Canvas.SetTop(newHead, newTop);

            GameCanvas.Children.Insert(0, newHead);
            snake.Insert(0, newHead);

            // Remove the tail if the snake didn't eat food
            if (snake.Count > snakeLength)
            {
                GameCanvas.Children.Remove(snake[snake.Count - 1]);
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void CheckCollision()
        {
            double headLeft = Canvas.GetLeft(snake[0]);
            double headTop = Canvas.GetTop(snake[0]);

            if (headLeft < 0 || headLeft >= GameCanvas.ActualWidth ||
                headTop < 0 || headTop >= GameCanvas.ActualHeight)
            {
                EndGame();
                return;
            }

            for (int i = 1; i < snakeLength; i++)
            {
                if (Canvas.GetLeft(snake[i]) == headLeft && Canvas.GetTop(snake[i]) == headTop)
                {
                    EndGame();
                    return;
                }
            }

            if (headLeft == Canvas.GetLeft(food) && headTop == Canvas.GetTop(food))
            {
                snakeLength++;
                GameCanvas.Children.Remove(food);
                CreateFood();
            }
        }

        private void EndGame()
        {
            gameTimer.Stop();
            MessageBox.Show($"Game Over! Your score: {snakeLength - InitialSnakeLength}");
            StartGame();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Up:
                    if (directionY != 1)
                    {
                        directionX = 0;
                        directionY = -1;
                    }
                    break;
                case System.Windows.Input.Key.Down:
                    if (directionY != -1)
                    {
                        directionX = 0;
                        directionY = 1;
                    } 
                    break;
                case System.Windows.Input.Key.Left:
                    if (directionX != 1)
                    {
                        directionX = -1;
                        directionY = 0;
                    }
                    break;
                case System.Windows.Input.Key.Right:
                    if (directionX != -1)
                    {
                        directionX = 1;
                        directionY = 0;
                    }
                    break;
            }
        }
    }
}
