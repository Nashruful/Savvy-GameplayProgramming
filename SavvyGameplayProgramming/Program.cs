
CodeGenerator codeGenerator = new();
string secretCode = codeGenerator.GenerateSecretCode();
int maxAttempts = 10;

Game game = new(secretCode, maxAttempts);
game.Start();

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

            // Handle Ctrl+D (EOF)
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
            Console.WriteLine($"Well placed pieces: {feedback.Item1}");
            Console.WriteLine($"Misplaced pieces: {feedback.Item2}");
        }

        Console.WriteLine("Game over! You ran out of attempts.");
        Console.WriteLine($"The secret code was: {_secretCode}");
    }
}



public class Validator
{
    public bool IsValidGuess(string guess)
    {
        if (string.IsNullOrEmpty(guess) || guess.Length != 4)
            return false;

        return guess.All(c => "012345678".Contains(c)) && guess.Distinct().Count() == 4;
    }
}


public class CodeGenerator
{
    private Random _random = new();

    public string GenerateSecretCode()
    {
        char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8' };
        char[] code = new char[4];

        for (int i = 0; i < 4; i++)
        {
            int index = _random.Next(digits.Length);
            code[i] = digits[index];
            digits = digits.Where((c, idx) => idx != index).ToArray(); // Remove used digit
        }

        return new string(code);
    }
}

public class FeedbackCalculator
{
    public (int WellPlaced, int Misplaced) CalculateFeedback(string secret, string guess)
    {
        int wellPlaced = 0;
        int misplaced = 0;

        bool[] secretUsed = new bool[4];
        bool[] guessUsed = new bool[4];

    // count well placed
        for (int i = 0; i < 4; i++)
        {
            if (secret[i] == guess[i])
            {
                wellPlaced++;
                secretUsed[i] = true;
                guessUsed[i] = true;
            }
        }

        // count misplaced
        for (int i = 0; i < 4; i++)
        {
            if (!guessUsed[i])
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!secretUsed[j] && guess[i] == secret[j])
                    {
                        misplaced++;
                        secretUsed[j] = true;
                        break;
                    }
                }
            }
        }

        return (wellPlaced, misplaced);
    }
}

