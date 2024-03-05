using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubmitButton : MonoBehaviour
{

    public TMP_Text leftText;
    public TMP_Text rightText;
    private int leftNumber;
    private int rightNumber;
    private int randomNumberRight;
    private int randomNumberLeft;
    public BackendApi backendApiEndpoint;
    public GameObject backendApiObject;
    public GameObject BGColor;
    public AudioSource audioSource;
    private string[] songNames;
    private AudioClip[] audioClips;
    private int songNum = 0;
    private int currentAudioIndex = 0;
    public GameObject[] progressButtons;
    private int progressNum = 0;

    private void Start()
    {
        ResetProgressButtons();
        //array of song names
        songNames = new string[]
        {
            "Beat 1 - Peaches & Eggplants",
            "Beat 2 - Turks & Caicos",
            "Beat 3 - Hvn on Earth",
            "Beat 4 - Back to the Moon"
        };

        audioClips = Resources.LoadAll<AudioClip>("Beats/Beat Stems (Mashed)/" + songNames[songNum]);
    }

    public void ButtonPressed()
    {
        backendApiEndpoint = backendApiObject.GetComponent<BackendApi>();
        int.TryParse(leftText.text, out leftNumber);
        int.TryParse(rightText.text, out rightNumber);
        Debug.Log("LeftText: " + leftNumber);
        Debug.Log("RightText: " + rightNumber);
        backendApiEndpoint.validateAnswer(leftNumber, rightNumber);
        ChangeTextDisplay();
        RandomGenerator();
    }

    private void ChangeTextDisplay()
    {
        //getting the next difficulty based on what the player answered
        string nextDifficultyLevel = backendApiEndpoint.DetermineNextDifficultyLevel();
        //next beat will play when the player gets an answer correct
        if ((backendApiEndpoint.mathQuestion["difficulty"] == "Easy" && nextDifficultyLevel == "Medium") || nextDifficultyLevel == "Hard")
        {
            ChangeStem();
            EnableProgressButton();
        }
        backendApiEndpoint.RequestNewQuestion(nextDifficultyLevel);
        BGColor.GetComponent<TextMeshProUGUI>().text = backendApiEndpoint.mathQuestion["question"];
    }


    private void RandomGenerator()
    {
        randomNumberLeft = Random.Range(2, 13);
        randomNumberRight = Random.Range(2, 13);
        if (backendApiEndpoint.mathQuestion["difficulty"] == "Easy")
        {
            leftText.text = randomNumberLeft.ToString();
        }
        rightText.text = randomNumberRight.ToString();
    }

    private void ChangeStem()
    {
        currentAudioIndex = (currentAudioIndex + 1);
        //playing the next beat stem
        if (audioClips != null && audioClips.Length > 0 && currentAudioIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[currentAudioIndex];
            audioSource.Play();
        }
        //full song has played, start playing the first stem of the next song
        else if (currentAudioIndex >= audioClips.Length)
        {
            songNum = songNum + 1;
            if (songNum >= songNames.Length)
            {
                songNum = 0;
            }

            audioClips = Resources.LoadAll<AudioClip>("Beats/Beat Stems (Mashed)/" + songNames[songNum]);
            currentAudioIndex = -1;
            ChangeStem();
            ResetProgressButtons();
        }
    }

    private void ResetProgressButtons()
    {
        progressNum = 0;
        foreach (GameObject progressButton in progressButtons)
        {
            Button button = progressButton.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = false;
                progressButton.GetComponent<Image>().color = Color.gray;
            }
        }
    }

    private void EnableProgressButton()
    {
        progressButtons[progressNum].GetComponent<Button>().interactable = true;
        progressButtons[progressNum].GetComponent<Image>().color = new Color(Random.value, 0.5f, 0.8f);
        progressNum++;
    }
}