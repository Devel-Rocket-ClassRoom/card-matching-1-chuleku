using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;

class Board
{
    public  int[,] _BoardNumber {  get; private set; }
    private readonly int[][] _CardNumber;
    private readonly int[] _Card;
    string[,] DisplayBoard;
    int[,] RealBoard;
    public int _Count;
    public int _Turn { get; private set; }
    public int _MaxTurn { get; private set; }
    public int MaxMatch { get; private set; }
    public int turnMatch { get; private set; }
    public Board(int level)
    {
        _Count = 0;
        _Turn = 0;
        turnMatch = 0;
        int rows = 0, cols = 0;
        if (level == 1) 
        { 
            rows = 2; cols = 4; _MaxTurn = 10; MaxMatch = 4;
        }
        else if (level == 2)
        { 
            rows = 4; cols = 4; _MaxTurn = 20; MaxMatch = 8;
        }
        else if (level == 3) 
        { 
            rows = 4; cols = 6; _MaxTurn = 30; MaxMatch = 12;
        }

        _BoardNumber = new int[rows, cols];
        _CardNumber = new int[rows * cols][];
        _Card = new int[rows * cols];
        DisplayBoard = new string[rows, cols];
        RealBoard = new int[rows, cols];

        int cardcount = 0;
        for (int i = 0; i < MaxMatch; i++)
        {
            _Card[cardcount++] = i + 1;
            _Card[cardcount++] = i + 1;
        }
    }
    public void suffleCard()
    {
        Console.Clear();
        Console.WriteLine("카드를 섞는중....");
        Thread.Sleep(2000);
        Random random = new Random();
        int count = 0;
        foreach (int i in _Card)
        {
            _CardNumber[count] = new int[2];
            _CardNumber[count][0] = i;
            _CardNumber[count][1] = random.Next(1, 100000);
            count++;
        }
        Array.Sort(_CardNumber, (a, b) => a[1].CompareTo(b[1]));
        int count1 = 0;
        for (int i = 0; i < _BoardNumber.GetLength(0); i++)
        {
            for (int j = 0; j < _BoardNumber.GetLength(1); j++)
            {
                DisplayBoard[i, j] = "**";
                RealBoard[i, j] = _CardNumber[count1][0];
                count1++;
            }
        }
    }
    public void DrawBorad()
    {
       
        Console.Clear();
        Console.WriteLine("=== 카드 짝 맞추기 게임 ===");
        Console.WriteLine();
        Console.Write("    ");
        for (int z = 0; z < _BoardNumber.GetLength(1); z++)
        {
            Console.Write($"{z + 1}열 ");
        }
        Console.WriteLine();
        for (int i = 0; i < _BoardNumber.GetLength(0); i++)
        {
            Console.Write($"{i + 1}행 ");
            for (int j = 0; j < _BoardNumber.GetLength(1); j++)
            { 
                Console.Write($"{DisplayBoard[i, j]}  ");
            }
            
            Console.WriteLine();
            
        }
        Console.WriteLine($"시도 횟수: {_Turn}/{_MaxTurn} | 찾은 쌍: {turnMatch}/{MaxMatch}");
        Console.WriteLine();
    }
    public int[] GetValidInput(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            string[] output = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (output.Length == 2 && int.TryParse(output[0], out int r) && int.TryParse(output[1], out int c))
            {

                if (r >= 1 && r <= _BoardNumber.GetLength(0) && c >= 1 && c <= _BoardNumber.GetLength(1))
                {
                    return new int[] { r - 1, c - 1 };
                }
                else
                {
                    Console.WriteLine($"행은 1~{_BoardNumber.GetLength(0)},열은 1~{_BoardNumber.GetLength(1)} 범위로  입력하세요.");
                    continue;
                }

            }
            else if (output.Length != 2)
            {
                Console.WriteLine("행과 열을 공백으로 구분하여 두 개의 숫자를 입력하세요. (예: 1 3)");
                continue;
            }

        }
    }
    public void FlipCard(int row,int col)
    {
        int r = row;
        int c = col;
        if(r>=0 && r< _BoardNumber.GetLength(0) && c>=0 && c< _BoardNumber.GetLength(1))
        {
            if(DisplayBoard[r,c]=="**")
            {
                DisplayBoard[r,c] = RealBoard[r,c].ToString("D2");
                _Count++;
                DrawBorad();
            }
        }
        else
        {
            Console.WriteLine($"행은 1~{_BoardNumber.GetLength(0)},열은 1~{_BoardNumber.GetLength(1)} 범위로  입력하세요.");
            return;
        }
    }
    public void CheckCard(int row,int col,int row1,int col1)
    {
        _Turn++;
        if(DisplayBoard[row, col] == DisplayBoard[row1, col1])
        {
            Console.WriteLine("짝을 찾았습니다");
            Thread.Sleep(2000);
            turnMatch++;
            DrawBorad();
            
            
        }
        else
        {
            Console.WriteLine("짝이 맞지않습니다.");
            Thread.Sleep(2000);
            HideCard(row,col,row1,col1);
            DrawBorad();
            
            
        }
    }
    public void HideCard(int row,int col, int row1, int col1)
    {
       
        DisplayBoard[row, col] = "**";
        DisplayBoard[row1, col1] = "**";
    }

}