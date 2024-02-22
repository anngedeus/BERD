
using UnityEngine;
using TMPro;

public class ButtonUI : MonoBehaviour
{

    public TMP_Text leftText;
    public TMP_Text rightText;
    private int randomNumber;
    public BackendApi backendApiEndpoint;
    public GameObject backendApiObject;


    public void ButtonPressed()
    {
        //use leftText here for backend
        Debug.Log(leftText);
        //use rightText here for backend
        Debug.Log(rightText);
        RandomGenerator();
        Debug.Log("I'm button pressed");
    }
    private void RandomGenerator()
    {
        randomNumber = Random.Range(2, 13);
        leftText.text = randomNumber.ToString();
        rightText.text = randomNumber.ToString();
    }
}
