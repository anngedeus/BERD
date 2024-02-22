using UnityEngine;
using TMPro;

public class RotateDiskRight : MonoBehaviour
{
    private Touch touch;
    private Vector3 oldTouchPosition;
    private Vector3 NewTouchPosition;
    public TMP_Text rightText;
    private int randomNumber;
    [SerializeField]
    private float keepRotateSpeed = 10f;

    private void Start()
    {
        RandomGenerator();
    }

    private void RandomGenerator()
    {
        randomNumber = Random.Range(2, 13);
        rightText.text = randomNumber.ToString();
    }

    private void Update()
    {
        RotateThings();
    }
    private void RotateThings()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                oldTouchPosition = touch.position;
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                NewTouchPosition = touch.position;
            }

            Vector3 rotDirection = oldTouchPosition - NewTouchPosition;
            Debug.Log(rotDirection);
            if (rotDirection.z < 0)
            {
                RotateRight();
            }
            else if (rotDirection.z > 0)
            {
                RotateLeft();
            }
        }
    }

    void RotateLeft()
    {
        transform.rotation = Quaternion.Euler(0f, 1.5f * keepRotateSpeed, 0f) * transform.rotation;
        randomNumber--;
        rightText.text = randomNumber.ToString();
        Debug.Log("I'm decreasing");
    }

    void RotateRight()
    {
        transform.rotation = Quaternion.Euler(0f, -1.5f * keepRotateSpeed, 0f) * transform.rotation;
        randomNumber++;
        rightText.text = randomNumber.ToString();
        Debug.Log("I'm increasing");
    }

}
