using HangmanGame.Core.Enums;
using HangmanGame.Core.Exceptions;
using HangmanGame.Core.Validators;

namespace HangmanGame.Core;

public class HangmanGameEngine
{
    private readonly string _secretWord;
    private const char _maskChar = '_';
    protected const int initialGuesses = 5;

    public HangmanGameEngine(string secretWord = "secret")
    {
        _secretWord = secretWord.ToUpperInvariant();
    }

    public bool IsOver => Result > GameResultEnum.InProgress;
    public string CurrentMaskedWord => new(_secretWord.Select(c => PreviousGuesses.Contains(c) ? c : _maskChar).ToArray());
    public List<char> PreviousGuesses { get; } = new List<char>();
    public int RemainingGuesses => initialGuesses - PreviousGuesses.Count(c => !CurrentMaskedWord.Contains(c));
    public GameResultEnum Result { get; private set; }

    public void Guess(char guessChar)
    {
        if (char.IsWhiteSpace(guessChar)) throw new InvalidGuessException("Guess cannot be blank.");
        if (!GuessCharValidator.Validate($"{guessChar}")) throw new InvalidGuessException("Guess must be a capital letter A through Z");
        if (IsOver) throw new InvalidGuessException("Can't make guesses after game is over.");

        if (PreviousGuesses.Any(g => g == guessChar)) throw new DuplicateGuessException();

        PreviousGuesses.Add(guessChar);

        if (_secretWord.Contains(guessChar))
        {
            if (!CurrentMaskedWord.Contains(_maskChar))
            {
                Result = GameResultEnum.Won;
            }
            return;
        }

        if (RemainingGuesses <= 0)
        {
            Result = GameResultEnum.Lost;
        }
    }
}
