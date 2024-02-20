using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour
{
    public Text displayText;
    //public string newText = "New Text String";
    // Start is called before the first frame update
    void Start()
    {
        //BackendApi backendApiEndpoint = new BackendApi();
        //Dictionary<string, string> mathQuestion = backendApiEndpoint.GetMathProblem();

    }
    
    void ChangeText()
    {
        //displayText.text = mathQuestion["Question"];
    }

    // Update is called once per frame
    void Update()
    {
        ChangeText();
    }
}
