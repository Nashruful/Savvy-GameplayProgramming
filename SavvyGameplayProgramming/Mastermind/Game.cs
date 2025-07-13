public class Game
{
    private string _secretCode;
    private int _maxAttempts;
    private Validator _validator = new();
    private FeedbackCalculator _feedbackCalculator = new();

    public Game(string secretCode, int maxAttempts)
    {
        _secretCode = secretCode;
        _maxAttempts = maxAttempts;
    }

    public void Start()
    {
        Console.WriteLine("Can you break the code? Enter a valid guess.");

        for (int attempt = 0; attempt < _maxAttempts; attempt++)
        {
            Console.WriteLine($"---\nRound {attempt}\n>");

            string? guess = Console.ReadLine()?.Trim();

            // Handle Ctrl+C (EOF)
            if (string.IsNullOrEmpty(guess))
            {
                Console.WriteLine("Game interrupted. Goodbye!");
                return;
            }

            if (!_validator.IsValidGuess(guess))
            {
                Console.WriteLine("Wrong input!");
                attempt--; // Retry same round
                continue;
            }

            if (guess == _secretCode)
            {
                Console.WriteLine("Congratz! You did it!");
                return;
            }

            var feedback = _feedbackCalculator.CalculateFeedback(_secretCode, guess);
            Console.WriteLine($"Well-placed pieces: {feedback.Item1}");
            Console.WriteLine($"Misplaced pieces: {feedback.Item2}");
        }

        Console.WriteLine("Game over! You ran out of attempts.");
        Console.WriteLine($"The secret code was: {_secretCode}");
    }
}