using HangmanGame.Core;
using HangmanGame.Core.Enums;
using HangmanGame.Core.Exceptions;

namespace HangmanGame.UnitTests;

public class HangmanGameGuess
{
    private const string testSecretWord = "TEST";
    private readonly HangmanGameEngine _game = new(testSecretWord);

    [Theory]
    [InlineData(' ')]
    [InlineData('-')]
    [InlineData('1')]
    public void ShouldThrowInvalidGuess(char invalidGuess)
    {
        Assert.Throws<InvalidGuessException>(() => _game.Guess(invalidGuess));
    }

    [Fact]
    public void ShouldThrowValidGuessWhenGameIsOver()
    {
        _game.Guess('T');
        _game.Guess('E');
        // game over, won!
        _game.Guess('S');
        Assert.Throws<InvalidGuessException>(() => _game.Guess('A'));
    }

    [Fact]
    public void ShouldDecrementRemainingGuessesGivenInvalidGuess()
    {
        int initialGuesses = _game.RemainingGuesses;
        char wrongGuess = 'Z';
        _game.Guess(wrongGuess);
        Assert.Equal(initialGuesses - 1, _game.RemainingGuesses);
    }

    [Theory]
    [InlineData('E')]
    public void ShouldNotDecrementRemainingGuessesGivenValidGuess(char correctGuess)
    {
        int initialGuesses = _game.RemainingGuesses;
        _game.Guess(correctGuess);
        Assert.Equal(initialGuesses, _game.RemainingGuesses);
    }

    [Fact]
    public void MaskedWordShouldIncludeGuessLetterIfCorrect()
    {
        char correctGuess = 'E';
        _game.Guess(correctGuess);
        Assert.True(_game.CurrentMaskedWord.Contains(Convert.ToChar(correctGuess)));
    }

    [Fact]
    public void ShouldSetGameIsOverTrueAndResultLostWhenGuessesLeftReaches0()
    {
        _game.Guess('A');
        _game.Guess('B');
        _game.Guess('C');
        _game.Guess('D');
        // correct
        _game.Guess('E');
        _game.Guess('F');
        Assert.True(_game.IsOver);
        Assert.Equal(GameResultEnum.Lost, _game.Result);
    }

    [Fact]
    public void ShouldSetGameIsOverAndResultWonWhenEntireWordIsGuessed()
    {
        _game.Guess('T');
        _game.Guess('E');
        _game.Guess('S');
        Assert.True(_game.IsOver);
        Assert.Equal(GameResultEnum.Won, _game.Result);
    }
}
