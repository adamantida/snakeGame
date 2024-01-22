namespace snakeGame;

/// <summary>
///     Represents the snake in the game. It contains methods to initialize the game board, place the apple, initialize the
///     player, display elements on the table, move the snake, check for collisions, update the tail, respawn the apple,
///     and update the game table.
/// </summary>
internal class Snake
{
    private readonly int _tableSize = 10;

    private (int, int) _apple;
    private List<(int, int)> _player;
    private string[,] _table;

    // Constructor for the Snake class. Initializes the table, places the apple at
    // coordinates (3, 5), initializes the player, and displays elements on the table.
    public Snake()
    {
        InitializeTable();
        PlaceApple(3, 5);
        InitializePlayer();
        DisplayElementsOnTable();
    }



    /// <summary>
    ///     Move the snake to a new position based on the given x and y coordinates.
    /// </summary>
    /// <param name="x">The movement in the x direction.</param>
    /// <param name="y">The movement in the y direction.</param>
    private void MoveSnake(int x, int y)
    {
        // Get the old head position of the snake
        var (oldHeadX, oldHeadY) = _player[0];

        // Calculate the new head position based on the given movement
        var newHeadX = oldHeadX + x;
        var newHeadY = oldHeadY + y;

        // Check for collision and out of bounds
        if (IsCollision((newHeadX, newHeadY)) || IsOutOfBounds((newHeadX, newHeadY)))
        {
            // End the game if collision or out of bounds
            Console.WriteLine("Game Over!");
            Environment.Exit(0);
            return;
        }

        // Insert the new head position to the snake
        _player.Insert(0, (newHeadX, newHeadY));

        // Check if the snake has eaten the apple
        if ((newHeadX, newHeadY) == _apple)
            RespawnApple(); // Respawn the apple if eaten
        else
            UpdateTail(); // Update the tail of the snake

        // Update the game table
        UpdateTable();
    }



    /// <summary>
    ///     Moves the snake based on the provided console key.
    /// </summary>
    /// <param name="key">The console key representing the direction of movement.</param>
    public void MoveSnake(ConsoleKey key)
    {
        // Move the snake based on the provided key
        switch (key)
        {
            case ConsoleKey.W: // Move up
                MoveSnake(-1, 0);
                break;
            case ConsoleKey.A: // Move left
                MoveSnake(0, -1);
                break;
            case ConsoleKey.S: // Move down
                MoveSnake(1, 0);
                break;
            case ConsoleKey.D: // Move right
                MoveSnake(0, 1);
                break;
        }
    }

    /// <summary>
    ///     Prints a table of size _tableSize to the console.
    /// </summary>
    public void Print()
    {
        // Print top border
        Console.WriteLine(new string('-', _tableSize));

        // Print table rows
        for (var i = 0; i < _tableSize; i++)
        {
            Console.Write("|");

            // Print table cells in current row
            for (var j = 0; j < _tableSize; j++) Console.Write(_table[i, j]);

            Console.WriteLine("|");
        }

        // Print bottom border
        Console.WriteLine(new string('-', _tableSize));
    }
    // Initializes the table by creating a new string array with the specified size
    // and populating it with empty strings.
    private void InitializeTable()
    {
        _table = new string[_tableSize, _tableSize];
        for (var i = 0; i < _tableSize; i++)
        for (var j = 0; j < _tableSize; j++)
            _table[i, j] = " ";
    }

    /// <summary>
    ///     Places an apple at the specified coordinates on the table.
    /// </summary>
    /// <param name="x">The x-coordinate of the apple</param>
    /// <param name="y">The y-coordinate of the apple</param>
    /// <returns>void</returns>
    private void PlaceApple(int x, int y)
    {
        _table[x, y] = "2";
    }

    /// <summary>
    ///     Initializes the player by creating a new list with a single tuple containing the initial position of the player.
    /// </summary>
    private void InitializePlayer()
    {
        // Set the initial position of the player at the center of the table
        _player = new List<(int, int)> { (_tableSize / 2, _tableSize / 2) };
    }

    /// <summary>
    ///     Updates the table with elements from the player collection.
    /// </summary>
    private void DisplayElementsOnTable()
    {
        // Iterate through the player collection and update the table with the corresponding elements
        foreach (var (x, y) in _player) _table[x, y] = 1.ToString();
    }
    private bool IsOutOfBounds((int, int) position)
    {
        return position.Item1 < 0 || position.Item1 >= _tableSize || position.Item2 < 0 || position.Item2 >= _tableSize;
    }

    private bool IsCollision((int, int) newHead)
    {
        return newHead.Item1 < 0 || newHead.Item1 >= _tableSize || newHead.Item2 < 0 || newHead.Item2 >= _tableSize ||
               _player.Contains(newHead);
    }

    private void UpdateTail()
    {
        var tail = _player[^1];
        _player.RemoveAt(_player.Count - 1);
        _table[tail.Item1, tail.Item2] = " ";
    }

    /// <summary>
    ///     Respawns the apple on the table.
    /// </summary>
    private void RespawnApple()
    {
        var random = new Random();
        do
        {
            _apple = (_tableSize / 2, random.Next(0, _tableSize));
        } while (_player.Contains(_apple) || (_apple.Item1 == _player[0].Item1 && _apple.Item2 == _player[0].Item2));

        _table[_apple.Item1, _apple.Item2] = "2";
    }

    private void UpdateTable()
    {
        for (var i = 0; i < _tableSize; i++)
        for (var j = 0; j < _tableSize; j++)
            if (_player.Contains((i, j)))
                _table[i, j] = "1";
            else if ((i, j) == _apple)
                _table[i, j] = "2";
            else
                _table[i, j] = " ";
    }
}