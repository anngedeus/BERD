using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BackendApi : MonoBehaviour
{
    public string apiUrl = "https://localhost:7132/api/math-problems";

    void Start()
    {
        StartCoroutine(RetrieveMathProblem(OnMathProblemReceived));
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
        }
        else
        {
            Debug.LogError("Failed to retrieve math problems.");
        }
    }

    IEnumerator RetrieveMathProblem(System.Action<List<Dictionary<string, string>>> callback)
    {
        Debug.Log("Starting...");

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
