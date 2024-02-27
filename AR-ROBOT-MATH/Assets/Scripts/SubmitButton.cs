using UnityEngine;
using TMPro;

public class SubmitButton : MonoBehaviour
{

    public TMP_Text leftText;
    public TMP_Text rightText;
    private int leftNumber;
    private int rightNumber;
    private int randomNumber;
    public BackendApi backendApiEndpoint;
    public GameObject backendApiObject;
    public GameObject BGColor;
    public int stemNumber = 0;
    public string currentSong = "Beat 1 - Peaches & Eggplants";
    public AudioSource audioSource;

    private void Start()
    {
        ChangeStem();
    }

    public void ButtonPressed()
    {
        backendApiEndpoint = backendApiObject.GetComponent<BackendApi>();
        leftNumber = int.Parse(leftText.ToString());
        rightNumber = int.Parse(rightText.ToString());
        backendApiEndpoint.validateAnswer(leftNumber, rightNumber);
        ChangeTextDisplay();
        RandomGenerator();
        ChangeStem();
    }

    private void ChangeTextDisplay()
    {

        string nextDifficultyLevel = backendApiEndpoint.DetermineNextDifficultyLevel();
        backendApiEndpoint.RequestNewQuestion(nextDifficultyLevel);
        BGColor.GetComponent<TextMeshProUGUI>().text = backendApiEndpoint.mathQuestion["question"];
    }


    private void RandomGenerator()
    {
        randomNumber = Random.Range(2, 13);
        leftText.text = randomNumber.ToString();
        rightText.text = randomNumber.ToString();
    }

    private void ChangeStem()
    {
        stemNumber++;
        string audioFilePath = "../Beats/Beat Stems/Beat Stems (Mashed)/" + currentSong + "/Part " + stemNumber;
        // Load the audio clip from the specified file path
        AudioClip audioClip = Resources.Load<AudioClip>(audioFilePath);

        if (audioClip != null)
        {
            // Change the audio clip to the loaded one
            audioSource.clip = audioClip;

            // Play the audio
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Failed to load audio clip: " + audioFilePath);
            stemNumber = 0;
            currentSong = "Beat 2 - Turks & Caicos";
        }
    }
}
