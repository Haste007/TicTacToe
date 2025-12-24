using System.Runtime.ExceptionServices;

namespace TicTacToe;

public class GameManager
{
    public int BoardSize { get; set; } = 3;
    public  BoardBox[][] Board {get; set;}
    public void PrintBoard()
    {
        if (!Console.IsOutputRedirected)
        {
            Console.Clear();
        }
        System.Console.WriteLine("------------Tic Tac Toe---------------------");
        for(int i = 0; i< BoardSize; i++)
        {
            Console.Write("|");
            for(int j= 0; j<BoardSize; j ++)
            {
                var box = Board[i][j];
                switch (box)
                {
                    case BoardBox.None:
                        int boxNo = (i*BoardSize)+j;
                        string printed = boxNo < 10? $"0{boxNo}":$"{boxNo}";
                        System.Console.Write($"| {boxNo} |");
                        break;
                    case BoardBox.Player1:
                        System.Console.Write($"| P1 |");
                        break;
                    case BoardBox.Player2:
                        System.Console.Write($"| P2 |");
                        break;
                }
            }
            Console.Write("|\n");
        }
        System.Console.WriteLine("----------------------------------------");
    }
    public bool Player1Turn {get; set;}
    public bool Player2Turn => !Player1Turn;
    public BoardBox Winner;

    public bool GameEnded => Winner != BoardBox.None;

    public void TakeTurn(int choice)
    {
        TakeTurn((choice)%BoardSize , (choice)/3);
    }

    public void TakeTurn(int col,int row)
    {
        if(col >= BoardSize || row >= BoardSize) return;
        if(GameEnded) return;

        BoardBox selected = Board[row][col];
        if(selected != BoardBox.None) return;
        Board[row][col] = Player1Turn? BoardBox.Player1 : BoardBox.Player2;
        CheckWin(Board[row][col]);
        CheckDraw();

        if(Winner == BoardBox.None)
        Player1Turn = !Player1Turn;
    }

    private void CheckDraw()
    {
        bool BoardFilled = Board.All(row => row.All(x => x != BoardBox.None));
        if(BoardFilled && Winner == BoardBox.None)
            Winner = BoardBox.Draw;
    }

    private void SetBoard()
    {
        Board = [[BoardBox.None,BoardBox.None,BoardBox.None],
                [BoardBox.None,BoardBox.None,BoardBox.None],
                [BoardBox.None,BoardBox.None,BoardBox.None]];
        Player1Turn = true;
        Winner = BoardBox.None;
    }

    public GameManager()
    {
        Board = new BoardBox[3][];
        SetBoard();
    }

    public void ResetGame()
    {
        SetBoard();
    }

    public void CheckWin(BoardBox player)
    {
        if(player is BoardBox.None) return;
        //Check Win Conditions
        bool isHorizontal = false;
        bool isVertical = false;
        bool isDiagonal = false;

        //Check Horizontal
        for(int i=0; i<BoardSize; i++)
        {
            isHorizontal = true;
            for(int j=0; j< BoardSize; j++)
            {
                if(Board[i][j] == player) continue;
                isHorizontal = false;
                break;
            };
            if(isHorizontal) break;
        }

        //Check for vertical
        // check each column,
            // set found to true, if a box is bad, set found to false, if by the end the found is true, return found


        for(int i=0; i< BoardSize; i++)
        {
            isVertical = true;
            for(int j=0; j< BoardSize; j++)
            {
                if(Board[j][i] == player) continue;
                isVertical = false;
                break;
            };
            if(isVertical) break;
        }

        //Check for diagonals
        //Main Diagonal
        for(int i=0; i< BoardSize; i++)
        {
            isDiagonal = true;
            if(Board[i][i] == player) continue;
            isDiagonal = false;
            break;
        }

        //Anti Diagonal
        for(int i=0; i< BoardSize; i++)
        {
            isDiagonal = true;
            if(Board[i][BoardSize-i-1] == player) continue;
            isDiagonal = false;
            break;
        }

        if(isDiagonal || isHorizontal || isVertical) Winner = player;
        return;
    }
}

public enum BoardBox
{
    None,
    Player1,
    Player2,
    Draw
}