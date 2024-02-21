using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour
{
    public Text displayText;
    public Dictionary<string, string> mathQuestion = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        // BackendApi backendApiEndpoint = new BackendApi();
        // mathQuestion = backendApiEndpoint.getMathQuestion(); // Assign the value returned by getMathQuestion to mathQuestion
        // Debug.Log(mathQuestion);
    }

    
    void ChangeText()
    {
        // if (mathQuestion != null && mathQuestion.ContainsKey("question"))
        // {
        //     displayText.text = mathQuestion["question"];
        // }
        // else
        // {
        //     displayText.text = "No question available";
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // ChangeText();
    }
}
