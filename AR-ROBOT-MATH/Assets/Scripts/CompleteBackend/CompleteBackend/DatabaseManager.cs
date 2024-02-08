using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace CompleteBackend
{
    public class DatabaseManager
    {
        // conection string used for connecting to the db of math questions
        //private string connectionString = "Data Source=localhost;Initial Catalog=mathDB;User ID=sa;Password=seniorProject24;";
        private string connectionString = "Server=tcp:berd.database.windows.net,1433;Initial Catalog=seniorProject24;Persist Security Info=False;User ID=berd;Password=seniorProject24;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public DatabaseManager()
        {
        }
        
        // returns 1 quesiton SET (question, answer, difficulty)
        public Dictionary<string, string> RetreiveMathProblems()
        {
            Dictionary<string, string> questionSet = new Dictionary<string, string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // open the connection
                connection.Open();

                //string query = "SELECT * FROM MathProblems"; // get all the problems in the db
                string query = "SELECT TOP 1 Question, Answer, Difficulty\nFROM MathProblems\nORDER BY NEWID()"; // get 1 problem set (randomly picked)

                // Create a SqlCommand object
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute the query
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the result set (1 row)
                        while (reader.Read())
                        {
                            string question = reader["Question"].ToString();
                            string answer = reader["Answer"].ToString();
                            string difficulty = reader["Difficulty"].ToString();

                            //Console.WriteLine($"Question: {question}, Answer: {answer}, Difficulty: {difficulty}");

                            // put the vals in the dictionary
                            questionSet.Add("Question", question);
                            questionSet.Add("Answer", answer);
                            questionSet.Add("Difficulty", difficulty);
                        }
                    }
                }
            }
            return questionSet;
        }
    }
}