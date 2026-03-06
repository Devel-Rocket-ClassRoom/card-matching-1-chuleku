using System;
using System.Xml;
Console.InputEncoding = System.Text.Encoding.UTF8;
bool gameflag = true;
while (gameflag)
{
    Board board = new Board();
    board.suffleCard();
    board.DrawBorad();
    bool endflag = true;
    while (endflag)
    {

        board._Count = 0;
        Console.Write("첫 번째 카드를 선택하세요 (행/열)");
        string input = Console.ReadLine();
        string[] output = input.Split(' ');
        int row = int.Parse(output[0]) - 1;
        int col = int.Parse(output[1]) - 1;
        board.FlipCard(row, col);
        if (board._Count == 1)
        {
            bool card = true;
            while (card)
            {
                Console.Write("두 번째 카드를 선택하세요 (행/열): ");
                string input1 = Console.ReadLine();
                string[] output1 = input1.Split(' ');
                int row1 = int.Parse(output1[0]) - 1;
                int col1 = int.Parse(output1[1]) - 1;
                board.FlipCard(row1, col1);
                if (board._Count == 2)
                {
                    board.CheckCard(row, col, row1, col1);
                    card = false;
                    if(board.turnMatch>=board.MaxMatch||board._Turn>=board._MaxTurn)
                    {
                        endflag = false;
                    }
                }
                else
                {
                    Console.WriteLine("다시 입력해주세요.");
                }
            }
        }
        else
        {
            Console.WriteLine("다시 입력해주세요.");
        }
    }
    Console.Write("새 게임을 하시겠습니까? (Y/N): ");
    string input2 = Console.ReadLine();
    Console.WriteLine();
    if (input2.ToLower() == "y")
    {
        endflag = true;
    }
    else if (input2.ToLower() == "n")
    {
        break;
    }
}



