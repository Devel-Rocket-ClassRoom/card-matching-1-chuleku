using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;

class Board
{
    private int[,] _BoardNumber = new int[4, 4];
    private int[][] _CardNumber = new int[16][];
    private readonly int[] _Card = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
    string[,] DisplayBoard = new string[4, 4];
    int[,] RealBoard = new int[4,4];
    public int _Count;
    public int _Turn { get; private set; }
    public int _MaxTurn { get; private set; }
    public int MaxMatch { get; private set; }
    public int turnMatch { get; private set; }
    public Board()
    {
        _Count = 0;
        _MaxTurn = 20;
        MaxMatch = 8;
        turnMatch = 0;
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

        Console.WriteLine("    1열 2열 3열 4열");
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
    }
    public void FlipCard(int row,int col)
    {
        int r = row;
        int c = col;
        if(r>=0 && r<4 && c>=0 && c<4)
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
            Console.WriteLine("1 2 3 중에 선택해주세요.");
            return;
        }
    }
    public void CheckCard(int row,int col,int row1,int col1)
    {
        _Turn++;
        if(DisplayBoard[row, col] == DisplayBoard[row1, col1])
        {
            Console.WriteLine("짝을 찾았습니다");
            turnMatch++;
            DrawBorad();
            Thread.Sleep(2000);
        }
        else
        {
            Console.WriteLine("짝이 맞지않습니다.");
            HideCard(row,col,row1,col1);
            DrawBorad();
        }
    }
    public void HideCard(int row,int col, int row1, int col1)
    {
        Thread.Sleep(2000);
        DisplayBoard[row, col] = "**";
        DisplayBoard[row1, col1] = "**";
    }
}