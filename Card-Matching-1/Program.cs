using System;
using System.Xml;
Console.InputEncoding = System.Text.Encoding.UTF8;
bool gameflag = true;
while (gameflag)
{

    Console.WriteLine("=== 카드 맞추기 게임 ===");
    Console.WriteLine("쉬움: (2x4)");
    Console.WriteLine("보통: (4x4)");
    Console.WriteLine("어려움: (4x6)");
    Console.WriteLine();
    int boardLevel;
    while (true)
    {
        Console.Write("난이도를 선택해주세요 (1~3): ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out boardLevel) && boardLevel >= 1 && boardLevel <= 3)
        {
            break;
        }
        Console.WriteLine("잘못된 입력입니다. 1, 2, 3 중에서 선택해주세요.");
    }

    Board board = new Board(boardLevel);
    board.suffleCard();
    board.DrawBorad();
    bool endflag = true;

    while (endflag)
    {

        board._Count = 0;
        int[] firstinput = board.GetValidInput("첫 번째 카드를 선택하세요 (행/열) ");
        int row = firstinput[0];
        int col = firstinput[1];
        board.FlipCard(row, col);
        if (board._Count == 1)
        {
            bool card = true;
            while (card)
            {
                int[] secondinput = board.GetValidInput("두 번째 카드를 선택하세요 (행/열) ");
                int row1 = secondinput[0];
                int col1 = secondinput[1];
                board.FlipCard(row1, col1);
                if (board._Count == 2)
                {
                    board.CheckCard(row, col, row1, col1);
                    card = false;
                    if(board.turnMatch>=board.MaxMatch||board._Turn>board._MaxTurn)
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



