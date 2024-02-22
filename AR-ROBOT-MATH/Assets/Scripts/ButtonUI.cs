
using UnityEngine;
using TMPro;

public class ButtonUI : MonoBehaviour
{

    public TMP_Text leftText;
    public TMP_Text rightText;
    private int leftNumber;
    private int rightNumber;
    private int randomNumber;
    public BackendApi backendApiEndpoint;
    public GameObject backendApiObject;
    public GameObject BGColor;
    private bool isCorrect;


    public void ButtonPressed()
    {
        
        leftNumber = int.Parse(leftText.ToString());
        rightNumber = int.Parse(rightText.ToString());
        isCorrect = backendApiEndpoint.validateAnswer(leftNumber, rightNumber);
        //if easy and correct || hard and incorrect
            //request medium
        //if medium and correct || hard and correct
            //request hard
        //if medium and incorrect
            //request easy
      
        

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
