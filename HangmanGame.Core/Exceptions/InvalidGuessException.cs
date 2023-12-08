namespace HangmanGame.Core.Exceptions;

public class InvalidGuessException : Exception
{
    public InvalidGuessException()
    {
    }

    public InvalidGuessException(string message) : base(message)
    {
    }


    public InvalidGuessException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
