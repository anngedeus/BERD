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
    public AudioSource audioSource;
    private string[] songNames;
    private AudioClip[] audioClips;
    private int songNum = 0;
    private int currentAudioIndex = 0;

    private void Start()
    {
        songNames = new string[]
        {
            "Beat 1 - Peaches & Eggplants",
            "Beat 2 - Turks & Caicos",
            "Beat 3 - Hvn on Earth",
            "Beat 4 - Back to the Moon"
        };

        audioClips = Resources.LoadAll<AudioClip>("Beats/Beat Stems (Mashed)/" + songNames[songNum]);
        if (audioClips.Length > 0)
        {
            audioSource.clip = audioClips[currentAudioIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clips found in the specified path.");
        }
       
    }

    public void ButtonPressed()
    {
        backendApiEndpoint = backendApiObject.GetComponent<BackendApi>();
        int.TryParse(leftText.text, out leftNumber);
        int.TryParse(rightText.text, out rightNumber);
        Debug.Log("LeftText: " + leftNumber);
        Debug.Log("RightText: " + rightNumber);
        //leftNumber = int.Parse(leftText.ToString().Trim());  
        //rightNumber = int.Parse(rightText.ToString().Trim());
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
        currentAudioIndex = (currentAudioIndex + 1);
        if (audioClips != null && audioClips.Length > 0 && currentAudioIndex < audioClips.Length)
        {
            //currentAudioIndex = (currentAudioIndex + 1) % audioClips.Length;
            audioSource.clip = audioClips[currentAudioIndex];
            audioSource.Play();
        }
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
        }
    }
}
