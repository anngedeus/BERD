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
    public GameObject leftDisk;
    public TMP_Text leftText;
    public GameObject diskLocked;
    //public RobotSocket robotSocketEndpoint; 

    void Start()
    {
        // Just made the object in the scene
        // Instantiate the BackendApi script dynamically

        // Creates obj that opens a connection to the robot
        //GameObject robotSocketObject = new GameObject("RobotSocketObject");
        //robotSocketEndpoint = robotSocketObject.AddComponent<RobotSocket>();
        backendApiEndpoint = backendApiObject.GetComponent<BackendApi>();
        displayText = GetComponent<TextMeshProUGUI>();
        
        Debug.Log(backendApiEndpoint);
        //Debug.Log(robotSocketEndpoint);

        // Start the coroutine to display the math problem
        StartCoroutine(DisplayMathProblem());
    }
    private void Update()
    {
        if (backendApiEndpoint.mathQuestion["difficulty"] != "Easy")
        {
            //locking the disk and displaying red circle
            leftDisk.GetComponent<RotateDiskLeft>().enabled = false;
            diskLocked.GetComponent<Image>().enabled = true;
            leftText.text = backendApiEndpoint.mathQuestion["question"].Substring(0,1);
        }
        else
        {
            leftDisk.GetComponent<RotateDiskLeft>().enabled = true;
            diskLocked.GetComponent<Image>().enabled = false;
        }
        ChangeText();
    }

    IEnumerator DisplayMathProblem()
    {
        // Wait for BackendApi to fetch the question
        yield return new WaitUntil(() => backendApiEndpoint.mathQuestion != null);

        if (backendApiEndpoint.mathQuestion != null) 
        {
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
   
}
