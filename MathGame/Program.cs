﻿using MathGame;
using System.Diagnostics;

List<MathProblem> history = [];
int correctAnswers = 0;
int wrongAnswers = 0;

Console.WriteLine("+---------------------------+");
Console.WriteLine("| Welcome to the Math Game! |");
Console.WriteLine("+---------------------------+");

// Main menu loop.
while (true)
{
    displayOptions();

    Console.Write("Your choice: ");
    string? userInput = Console.ReadLine();

    // Quit game if user enters 'Q'.
    if (userInput is not null && userInput.ToLower() == "q")
    {
        Console.WriteLine("Thank you for playing!");
        showHistory();
        break;
    }

    // Play game based on user input.
    handleUserChoice(userInput);
};

void displayOptions()
{
    Console.WriteLine();
    Console.WriteLine("Please enter a number from the options below, or Q to exit.\n");
    Console.WriteLine("1. Addition Problem");
    Console.WriteLine("2. Subtraction Problem");
    Console.WriteLine("3. Multiplication Problem");
    Console.WriteLine("4. Division Problem");
    Console.WriteLine("5. Performance History");
    Console.WriteLine();
}

void handleUserChoice(string? userInput)
{
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
}

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

    // Receive the player's response and time taken.
    Stopwatch stopwatch = new();
    stopwatch.Start();
    var userInput = Console.ReadLine();
    stopwatch.Stop();
    problem.ResponseTime = stopwatch.Elapsed;

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

        Console.WriteLine($"Answered in {problem.ResponseTime.TotalSeconds:0.00} seconds.");

        // Log this round to the game history.
        history.Add(problem);
    }
    else
    {
        // Back out to main menu if the player mistypes.
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