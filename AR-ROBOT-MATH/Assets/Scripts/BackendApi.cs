using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BackendApi : MonoBehaviour
{
    public string apiUrl = "https://berdbackend.azurewebsites.net/api/math-problems";
    public Dictionary<string, string> mathQuestion;
    List<Dictionary<string, string>> mathProblems = new List<Dictionary<string, string>>();
    public bool isCorrect = false;

    void Start()
    {
        string difficulty = DetermineNextDifficultyLevel();
        StartCoroutine(RetrieveMathProblem(difficulty, OnMathProblemReceived));
    }

    void OnMathProblemReceived(List<Dictionary<string, string>> mathProblems)
    {
        if (mathProblems != null)
        {
            foreach (var mathProblem in mathProblems)
            {
                string question = mathProblem["question"];
                string answer = mathProblem["answer"];
                string difficulty = mathProblem["difficulty"];

                Debug.Log($"Math problem received: Question: {question}, Answer: {answer}, Difficulty: {difficulty}");
            }
            this.mathQuestion = mathProblems[0];
        }
        else
        {
            Debug.LogError("Failed to retrieve math problems.");
        }
    }

    // makes a GET request to the backend API to retrieve math problems.
    IEnumerator RetrieveMathProblem(string requestedDifficulty, System.Action<List<Dictionary<string, string>>> callback)
    {
        Debug.Log("Starting...");
        apiUrl += "?difficulty=" + UnityWebRequest.EscapeURL(requestedDifficulty);

        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to retrieve math problems: " + request.error);
                callback(null);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                // Debug.Log("Received JSON response: " + jsonResponse);

                try
                {
                    List<Dictionary<string, string>> mathProblems = ParseMathProblems(jsonResponse);
                    callback(mathProblems);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Failed to parse JSON response: " + e.Message);
                    callback(null);
                }
            }
        }
    }

    public string DetermineNextDifficultyLevel()
    {
        string difficultyLevel = "Easy";
        if (mathQuestion != null)
        {            
            if ((mathQuestion["difficulty"] == "Easy" && isCorrect) ||
                (mathQuestion["difficulty"] == "Hard" && isCorrect))
            {
                difficultyLevel = "Medium";
            }
            else if ((mathQuestion["difficulty"] == "Medium" && isCorrect) ||
                    (mathQuestion["difficulty"] == "Hard" && isCorrect))
            {
                difficultyLevel = "Hard";
            }
            else if (mathQuestion["difficulty"] == "Medium" && !isCorrect)
            {
                difficultyLevel = "Easy";
            }
        }
        return difficultyLevel;
    }

    List<Dictionary<string, string>> ParseMathProblems(string jsonResponse)
    {
        var mathProblems = new List<Dictionary<string, string>>();

        // manual parsing
        var jsonObject = JsonUtility.FromJson<JsonObject>(jsonResponse);
        foreach (var mathProblemObject in jsonObject.mathProblems)
        {
            var mathProblem = new Dictionary<string, string>();
            mathProblem["question"] = mathProblemObject.question;
            mathProblem["answer"] = mathProblemObject.answer;
            mathProblem["difficulty"] = mathProblemObject.difficulty;

            mathProblems.Add(mathProblem);

        }
        return mathProblems;
    }

    public void RequestNewQuestion(string newDifficulty)
    {
        // Call RetrieveMathProblem with the new difficulty
        StartCoroutine(RetrieveMathProblem(newDifficulty, OnMathProblemReceived));
    }

    public Dictionary<string, string> getMathQuestion()
    {
        return mathQuestion;
    }

    public void validateAnswer(int? multiplicantOne = null, int? multiplicantTwo = null, int? userAnswer = null)
    {
        // logic to determine if the user's answer is correct
        isCorrect = true;
    }

    [System.Serializable]
    public class JsonObject
    {
        public List<MathProblemObject> mathProblems;
    }

    [System.Serializable]
    public class MathProblemObject
    {
        public string question;
        public string answer;
        public string difficulty;
    }
}
