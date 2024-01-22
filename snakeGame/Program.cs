namespace snakeGame;

internal static class Program
{
    public static void Main()
    {
        var snake = new Snake();
        ConsoleKeyInfo readLine = default;
        var lastKeyPressTime = DateTime.Now;
        var repeatInterval = TimeSpan.FromSeconds(1);

        do
        {
            if (Console.KeyAvailable)
            {
                readLine = Console.ReadKey();
                lastKeyPressTime = DateTime.Now;
            }
            else if (DateTime.Now - lastKeyPressTime > repeatInterval)
            {
            }

            snake.MoveSnake(readLine.Key);
            snake.Print();

            Thread.Sleep(1000); // Add a 1-second delay

            Console.Clear();
        } while (readLine.Key != ConsoleKey.E);
    }
}