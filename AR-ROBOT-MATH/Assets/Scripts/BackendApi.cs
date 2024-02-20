using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BackendApi : MonoBehaviour
{
    // URL of the backend API endpoint to retrieve math problems
    public string apiUrl = "https://localhost:7131/api/math-problems";

    // Method to retrieve a math problem from the backend API
    public IEnumerator RetrieveMathProblem(System.Action<Dictionary<string, string>> callback)
    {
        // Send a GET request to the backend API
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            // Check if the request was successful
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to retrieve math problem: " + request.error);
                callback(null); // Notify the caller with a null result
            }
            else
            {
                // Parse the JSON response
                string jsonResponse = request.downloadHandler.text;
                Dictionary<string, string> mathProblem = JsonUtility.FromJson<Dictionary<string, string>>(jsonResponse);

                // Invoke the callback with the retrieved math problem
                callback(mathProblem);
            }
        }
    }
    void Start()
    {
        StartCoroutine(RetrieveMathProblem(OnMathProblemReceived));
    }

    void OnMathProblemReceived(Dictionary<string, string> mathProblem)
    {
        if (mathProblem != null)
        {
            // Math problem received, do something with it
            Debug.Log("Math problem received: " + mathProblem["Question"]);
        }
        else
        {
            // Error occurred while retrieving math problem
            Debug.LogError("Failed to retrieve math problem.");
        }
    }
}
