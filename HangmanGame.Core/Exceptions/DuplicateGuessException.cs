namespace HangmanGame.Core.Exceptions;

public class DuplicateGuessException : Exception
{
    public DuplicateGuessException()
    {
    }

    public DuplicateGuessException(string? message) : base(message)
    {
    }

    public DuplicateGuessException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
