using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayScript : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public Dictionary<string, string> mathQuestion = new Dictionary<string, string>();
    public BackendApi backendApiEndpoint; // Reference to the BackendApi script
    public GameObject backendApiObject;
    public RobotSocket robotSocketEndpoint; 

    void Start()
    {
        //Just made the object in the scene
        // Instantiate the BackendApi script dynamically
        //GameObject backendApiObject = new GameObject("BackendApiObject");
        //backendApiEndpoint = backendApiObject.AddComponent<BackendApi>();

        // Creates obj that opens a connection to the robot
        GameObject robotSocketObject = new GameObject("RobotSocketObject");
        robotSocketEndpoint = robotSocketObject.AddComponent<RobotSocket>();
        backendApiEndpoint = backendApiObject.GetComponent<BackendApi>();
        displayText = GetComponent<TextMeshProUGUI>();
        
        Debug.Log(backendApiEndpoint);
        Debug.Log(robotSocketEndpoint);

        // Start the coroutine to display the math problem
        StartCoroutine(DisplayMathProblem());
    }

    IEnumerator DisplayMathProblem()
    {
        // Wait for BackendApi to fetch the question
        yield return new WaitUntil(() => backendApiEndpoint.mathQuestion != null);

        if (backendApiEndpoint.mathQuestion != null) 
        {
            ChangeText();
            string question = backendApiEndpoint.mathQuestion["question"];
            Debug.Log("Math problem from BackendApi: " + question);
        }
        else 
        {
            Debug.LogError("No math problem received from BackendApi.");
        }
    }
    void ChangeText()
    {
        displayText.text = backendApiEndpoint.mathQuestion["question"];
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
