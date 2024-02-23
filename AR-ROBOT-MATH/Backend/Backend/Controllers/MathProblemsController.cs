using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections;
/*
 *  this entire class is responsible for handling HTTP requests related to math problems
 */
namespace Backend.Controllers
{
    [ApiController]
    [Route("api/math-problems")]
    public class MathProblemsController : ControllerBase
    {
        private readonly string _connectionString;

        public MathProblemsController(IConfiguration configuration)
        {
            // get the connection string from appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // responsible for etrieving information from the sql server
        [HttpGet]
        public IActionResult GetMathProblems(string difficulty)
        {
            List<MathProblem> mathProblems = new List<MathProblem>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                if (difficulty == null)
                {
                    difficulty = "Easy";
                }

                // SQL query to randomly retrieve math problems from the database
                string query = $"SELECT TOP 1 Question, Answer, Difficulty FROM MathProblems WHERE Difficulty = @Difficulty ORDER BY NEWID()";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Difficulty", difficulty);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // create a MathProblem object from the query results
                            MathProblem mathProblem = new MathProblem
                            {
                                Question = reader["Question"].ToString(),
                                Answer = reader["Answer"].ToString(),
                                Difficulty = reader["Difficulty"].ToString()
                            };

                            mathProblems.Add(mathProblem);
                        }
                    }
                }
            }

            // create an object to wrap the list of math problems
            var response = new MathProblemsResponse
            {
                MathProblems = mathProblems
            };

            // return the response object as JSON
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
