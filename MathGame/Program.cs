/* 
 * 1. Greeting
 * 2. Menu
 * 3. Play Game
 * 4. Store history
 */

using MathGame;

List<MathProblem> history = [];
int correctAnswers = 0;
int wrongAnswers = 0;

Console.WriteLine("+---------------------------+");
Console.WriteLine("| Welcome to the Math Game! |");
Console.WriteLine("+---------------------------+");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Please enter a number from the options below, or Q to exit.\n");
    Console.WriteLine("1. Addition Problem");
    Console.WriteLine("2. Subtraction Problem");
    Console.WriteLine("3. Multiplication Problem");
    Console.WriteLine("4. Division Problem");
    Console.WriteLine("5. Performance History");
    Console.WriteLine();

    Console.Write("Your choice: ");
    string? userInput = Console.ReadLine();

    if (userInput is not null && userInput.ToLower() == "q")
    {
        break;
    }

    bool validChoice = int.TryParse(userInput, out int userChoice) && userChoice is > 0 and <= 5;

    if (validChoice)
    {
        Console.WriteLine($"You chose {userChoice}.\n");
        optionSelect(userChoice);
    }
    else
    {
        Console.WriteLine("Choice must be a number from the list above. Please try again.");
    }
    Console.WriteLine();
};

Console.WriteLine("Thank you for playing!");

void optionSelect(int userChoice)
{
    switch (userChoice)
    {
        case 1:
            playProblem(new AdditionProblem());
            break;
        case 2: 
            playProblem(new SubtractionProblem());
            break;
        case 3:
            playProblem(new MultiplicationProblem());
            break;
        case 4:
            playProblem(new DivisionProblem());
            break;
        case 5:
            showHistory();
            break;
    }
}

void playProblem(MathProblem problem)
{
    Console.Write(problem.ToStringPrompt());
    var userInput = Console.ReadLine();

    if (int.TryParse(userInput, out int userAnswer))
    {
        problem.MakeAttempt(userAnswer);
        if (problem.WasCorrect)
        {
            correctAnswers++;
            Console.WriteLine("Correct!");
        }
        else
        {
            wrongAnswers++;
            Console.WriteLine($"Wrong. The correct answer is {problem.Result}.");
        }
        history.Add(problem);
    }
    else
    {
        Console.WriteLine("Invalid input. Try another round.");
    }
}

void showHistory()
{
    Console.WriteLine();
    Console.WriteLine("<<< RESULTS >>>");
    Console.WriteLine($"Correct answers: {correctAnswers}");
    Console.WriteLine($"Incorrect answer: {wrongAnswers}");
    Console.WriteLine();
    foreach (var problem in history)
    {
        Console.WriteLine(problem.ToStringFull());
        Console.WriteLine(problem.WasCorrect ? "    Correct." : "    Incorrect.");
    }
}