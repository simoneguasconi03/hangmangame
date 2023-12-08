using System.Text.RegularExpressions;

namespace HangmanGame.Core.Validators;

public static partial class GuessCharValidator
{
    [GeneratedRegex("^[A-Z]$")]
    private static partial Regex GuessCharRegex();

    public static bool Validate(string guessChar) => GuessCharRegex().IsMatch(guessChar);
}
