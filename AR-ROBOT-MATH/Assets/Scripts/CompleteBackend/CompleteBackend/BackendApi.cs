using System;
using System.Collections.Generic;
using CompleteBackend;

public class BackendApi
{
    private DatabaseManager databaseManager;
    private Dictionary<string, string> currMathProblem;

    // ensure that the BackendApi instance is shared across all instances of the class
    private static BackendApi instance;
    public static BackendApi Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendApi();
            }
            return instance;
        }
    }

    public BackendApi()
    {
        databaseManager = new DatabaseManager();
        currMathProblem = new Dictionary<string, string>();
    }

    // Method to retrieve a math problem from the database
    public Dictionary<string, string> GetMathProblem()
    {
        currMathProblem = databaseManager.RetrieveMathProblem();
        return currMathProblem;
    }

    // Method to validate user answer that is received 
    public bool ValidateAnswer(int userAnswer)
    {
        int trueCorrectAnswer = int.Parse(currMathProblem["Answer"]);
        return userAnswer == trueCorrectAnswer;
    }

    // Ensure DatabaseManager resources are properly disposed when the application quits
    public void Dispose()
    {
        if (databaseManager != null)
        {
            databaseManager.Dispose();
        }
    }
}
