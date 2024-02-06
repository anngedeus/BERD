using System;
using System.Data.SqlClient;

namespace CompleteBackend
{
    public class DatabaseManager
    {
        // conection string used for connecting to the db of math questions
        private string connectionString = "Data Source=localhost;Initial Catalog=Master;User ID=sa;Password=seniorProject24;";

        public DatabaseManager()
        {
        }

        public void RetreiveMathProblems()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // open the connection
                connection.Open();

                string query = "SELECT * FROM MathProblems";

                // Create a SqlCommand object
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute the query
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the result set
                        while (reader.Read())
                        {
                            // Access the columns by index or column name
                            string question = reader["Question"].ToString();
                            int answer = (int)reader["Answer"];
                            string difficulty = reader["Difficulty"].ToString();

                            // Do something with the retrieved data
                            Console.WriteLine($"Question: {question}, Answer: {answer}, Difficulty: {difficulty}");
                        }
                    }
                }
            }
        }
    }
}
