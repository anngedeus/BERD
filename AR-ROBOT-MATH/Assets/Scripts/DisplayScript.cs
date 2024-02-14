using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour
{
    public Text displayText;
    public string newText = "New Text String";
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //function that will be called by backend
    void ChangeText()
    {
        displayText.text = newText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
