using HangmanGame.Core;
using HangmanGame.Core.Enums;
using HangmanGame.Core.Exceptions;

var game = new HangmanGameOriginator();
var gameHistory = new Stack<HangmanGameMemento>();
gameHistory.Push(game.CreateSetPoint());

while (!game.IsOver)
{
    Console.Clear();
    Console.SetCursorPosition(0, 0);
    Console.WriteLine("Welcome to Hangman");

    Console.WriteLine(game.CurrentMaskedWord);
    Console.WriteLine($"Previous guesses: {string.Join(',', game.PreviousGuesses.ToArray())}");
    Console.WriteLine($"Guesses left: {game.RemainingGuesses}");

    Console.Write("Guess (a-z or '-' to undo last guess): ");

    var entry = char.ToUpperInvariant(Console.ReadKey().KeyChar);

    if (entry == '-' && gameHistory.Count > 1)
    {
        gameHistory.Pop();
        game.ResumeFrom(gameHistory.Peek());
        Console.WriteLine();
        continue;
    }

    try
    {
        game.Guess(entry);
        gameHistory.Push(game.CreateSetPoint());
        Console.WriteLine();
    }
    catch (DuplicateGuessException)
    {
        Console.WriteLine("You already guessed that.");
        continue;
    }
    catch (InvalidGuessException)
    {
        Console.WriteLine("Invalid guess.");
        continue;
    }

    if (game.Result == GameResultEnum.Won) Console.WriteLine("You won!");
    if (game.Result == GameResultEnum.Lost) Console.WriteLine("You lost, try again!");
}
