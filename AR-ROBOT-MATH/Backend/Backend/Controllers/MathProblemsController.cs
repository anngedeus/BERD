using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/math-problems")]
    public class MathProblemsController : ControllerBase
    {
        private readonly string _connectionString;

        public MathProblemsController(IConfiguration configuration)
        {
            // Retrieve the connection string from appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetMathProblems()
        {
            // Create a list to store math problems
            List<MathProblem> mathProblems = new List<MathProblem>();

            // Establish a connection to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection
                connection.Open();

                // SQL query to retrieve math problems from the database
                string query = "SELECT TOP 1 Question, Answer, Difficulty FROM MathProblems ORDER BY NEWID()";

                // Create a command object
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute the query and retrieve the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there are any rows returned
                        if (reader.Read())
                        {
                            // Create a MathProblem object from the query results
                            MathProblem mathProblem = new MathProblem
                            {
                                Question = reader["Question"].ToString(),
                                Answer = reader["Answer"].ToString(),
                                Difficulty = reader["Difficulty"].ToString()
                            };

                            // Add the MathProblem object to the list
                            mathProblems.Add(mathProblem);
                        }
                    }
                }
            }

            // Create an object to wrap the list of math problems
            var response = new MathProblemsResponse
            {
                MathProblems = mathProblems
            };

            // Return the response object as JSON
            return Ok(response);
        }
    }

    public class MathProblem
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Difficulty { get; set; }
    }

    public class MathProblemsResponse
    {
        public List<MathProblem> MathProblems { get; set; }
    }
}
