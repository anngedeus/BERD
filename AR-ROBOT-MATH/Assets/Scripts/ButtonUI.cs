
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
        // isCorrect = backendApiEndpoint.validateAnswer(leftNumber, rightNumber);
        // ChangeTextDisplay(isCorrect);
        backendApiEndpoint.validateAnswer(leftNumber, rightNumber);
        ChangeTextDisplay();
        RandomGenerator();
      
    }
    // private void ChangeTextDisplay(bool isCorrect)
    private void ChangeTextDisplay()
    {
        // if ((backendApiEndpoint.mathQuestion["difficulty"] == "Easy" && isCorrect == true) || (backendApiEndpoint.mathQuestion["difficulty"] == "Hard" && isCorrect == true))
        // {
        //     //request medium            
        // }
        // if ((backendApiEndpoint.mathQuestion["difficulty"] == "Medium" && isCorrect == true) || (backendApiEndpoint.mathQuestion["difficulty"] == "Hard" && isCorrect == true))
        // {
        //     //request hard
        // }
        // if (backendApiEndpoint.mathQuestion["difficulty"] == "Medium" && isCorrect == false) {
        //     //request easy
        // }
        string nextDifficultyLevel = backendApiEndpoint.DetermineNextDifficultyLevel();
        backendApiEndpoint.RequestNewQuestion(nextDifficultyLevel);

        //For changing questions based on in BGColor.GetComponent<TextMeshProUGUI>().text;
    }


    private void RandomGenerator()
    {
        randomNumber = Random.Range(2, 13);
        leftText.text = randomNumber.ToString();
        rightText.text = randomNumber.ToString();
    }
}
