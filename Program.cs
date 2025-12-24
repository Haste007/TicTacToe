using TicTacToe;
GameManager game = new();

while(game.Winner == BoardBox.None)
{

    game.PrintBoard();
    string turnOwner = game.Player1Turn? "Player 1" : "Player 2";
    System.Console.WriteLine($"Current Turn: {turnOwner}");
    Console.Write("Take Turn:");
    var c = Console.ReadLine();
    if(int.TryParse(c,out int result))
    {
        game.TakeTurn(result);

        if(game.Winner != BoardBox.None)
        {
            game.PrintBoard();
            System.Console.WriteLine($"Game Won by {turnOwner}\n");

            Console.WriteLine($"Press R to reset, Press E to end");
            char[] validInputs = ['r','R','e','E'];
            char order = '\0';
            while(!validInputs.Contains(order))
            {
            var key = Console.ReadKey();
            order = key.KeyChar;
            }
            switch (order)
            {
                case 'r':
                case 'R':
                    game.ResetGame();
                    break;
                case 'e':
                case 'E':
                    break;
            }

        }
    }
}