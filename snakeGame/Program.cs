using System;

namespace snakeGame;

internal class Program
{
    public static void Main()
    {
        Snake snake = new Snake();
        ConsoleKeyInfo readLine = new ConsoleKeyInfo();
        while (readLine.Key != ConsoleKey.E)
        {
            snake.MoveSnake(readLine.Key);
            snake.Print();
            readLine = Console.ReadKey();
            Console.Clear();
        }
    }
}