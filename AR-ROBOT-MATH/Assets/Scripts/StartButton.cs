using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartButton : MonoBehaviour
{
    public TMP_InputField userInput;
    public static string userName;


    void Start()
    {

    }

    public void ButtonPressed()
    {
        //getting the user name
        userName = userInput.text;
        Debug.Log("Entered name: " + userName);

        //changing to main scene
        SceneManager.LoadScene("XR-Testing-Unity.unity");
    }
}
