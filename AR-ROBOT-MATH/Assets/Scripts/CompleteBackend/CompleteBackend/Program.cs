using System;
using System.Diagnostics;
using System.Media;
//using Xamarin.Forms;
//using Xamarians.MediaPlayer;

namespace CompleteBackend
{
    public class Program
    {
        static void Main()
        {
            DatabaseManager databaseManager = new DatabaseManager();
            Dictionary<string, string> mathQuestion = databaseManager.RetreiveMathProblems();

            // convert the answer from string to an int
            string correctAnswer = mathQuestion["Answer"];
            int numCorrectAnswer = int.Parse(correctAnswer);

            Console.WriteLine($"Your question is: {mathQuestion["Question"]}");

            bool correctChoice = false;

            while (!correctChoice)
            {
                string userInput = Console.ReadLine();

                int userAnswer;

                if (!int.TryParse(userInput, out userAnswer)) // convert user answer to an int
                {
                    Console.WriteLine("Please enter a valid integer as your answer.");
                    continue;
                }

                correctChoice = ValidateAnswer(userAnswer, numCorrectAnswer);

                if (correctChoice)
                {
                    Console.WriteLine("You have entered the correct answer, good job!");
                    PlayBeat(); // play the first beat repeatedly
                    break;
                }
                Console.WriteLine("Wrong answer, try again.");
            }

            Console.ReadKey();
        }

        static bool ValidateAnswer(int userAnswer, int correctAnswer)
        {
            return userAnswer == correctAnswer;
        }

        static void PlayBeat()
        {
            string audioFilePath = "/Users/paigeharvey/Desktop/Spring 2024/Senior Project/Beats/Beat Stems (Mashed)/Beat 2 - Turks & Caicos/Part 1.mp3";
            Process process = new Process();

            process.StartInfo.FileName = "afplay"; // Using afplay command-line tool for audio playback on macOS
            process.StartInfo.Arguments = $"\"{audioFilePath}\""; ; // Surround file path with double quotes
            process.Start();
            //process.WaitForExit();
        }
    }
}