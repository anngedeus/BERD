using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartButton : MonoBehaviour
{
    public TMP_InputField userInput;
    private string userName;
    public string mainScene;

    void Start()
    {

    }

    public void ButtonPressed()
    {
        //getting the user name
        userName = userInput.text;
        //changing to main scene
        //SceneManager.LoadScene(mainScene);
    }
}
