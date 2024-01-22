using System;
using System.Collections.Generic;

namespace snakeGame;

internal class Snake
{
    private readonly int _tableSize = 10;
    private (int, int) _apple;
    private readonly List<(int, int)> _player;
    private readonly string[,] _table;

    public Snake()
    {
        _table = new String[_tableSize, _tableSize];
        for (int i = 0; i < _tableSize; i++)
        {
            for (int j = 0; j < _tableSize; j++)
            {
                _table[i, j] = " ";
            }
        }

        _apple = (3, 5);
        _player = new List<(int, int)> { (_tableSize / 2, _tableSize / 2) };
        _table[_apple.Item1, _apple.Item2] = 2.ToString();
        foreach (var (x, y) in _player)
        {
            _table[x, y] = 1.ToString();
        }
    }

    private void MoveSnake(int x, int y)
    {
        var newHead = (_player[0].Item1 + x, _player[0].Item2 + y);
        if (newHead.Item1 < 0 || newHead.Item1 >= _tableSize || newHead.Item2 < 0 || newHead.Item2 >= _tableSize ||
            _player.Contains(newHead))
        {
            // if snake hit wall and self
            Console.WriteLine("Game Over!");
            Environment.Exit(0);
        }
        else
        {
            _player.Insert(0, newHead);
            if (newHead != _apple)
            {
                var tail = _player[_player.Count - 1];
                _player.RemoveAt(_player.Count - 1);
                _table[tail.Item1, tail.Item2] = " ";
            }
            else
            {
                // If the snake eats the apple
                do _apple = (_tableSize / 2, new Random().Next(0, _tableSize));
                while (_player.Contains(_apple));

                _table[_apple.Item1, _apple.Item2] = 2.ToString();
            }

            foreach (var (i, j) in _player)
            {
                _table[i, j] = 1.ToString();
            }
        }
    }

    public void MoveSnake(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.W:
                MoveSnake(-1, 0);
                break;
            case ConsoleKey.A:
                MoveSnake(0, -1);
                break;
            case ConsoleKey.S:
                MoveSnake(1, 0);
                break;
            case ConsoleKey.D:
                MoveSnake(0, 1);
                break;
        }
    }

    public void Print()
    {
        for (int i = 0; i < _tableSize; i++) Console.Write("-");

        Console.WriteLine();
        for (int i = 0; i < _tableSize; i++)
        {
            Console.Write("|");
            for (int j = 0; j < _tableSize; j++)
            {
                Console.Write(_table[i, j]);
            }

            Console.WriteLine("|");
        }

        for (int i = 0; i < _tableSize; i++) Console.Write("-");

        Console.WriteLine();
    }
}