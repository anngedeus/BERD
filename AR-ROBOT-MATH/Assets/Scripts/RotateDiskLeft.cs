using UnityEngine;
using TMPro;

public class RotateDiskLeft : MonoBehaviour
{
    //private Touch touch;
    private Vector2 oldTouchPosition;
    public TMP_Text leftText;
    private int randomNumber;
    private bool isDragging = false;

    private void Start()
    {
        RandomGenerator();
    }

    private void RandomGenerator()
    {
        randomNumber = Random.Range(2, 13);
        leftText.text = randomNumber.ToString();
    }

    private void Update()
    {
        RotateThings();
    }

    private void RotateThings()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // Touch occurred on this object
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        oldTouchPosition = touch.position;
                        isDragging = true;
                        break;

                    case TouchPhase.Moved:
                        if (isDragging)
                        {
                            Vector2 swipeValue = touch.position - oldTouchPosition;
                            float rotationSpeed = 0.5f;
                            Debug.Log(touch.position);
                            //keeping the disk rotating in same direction when it crosses y axis
                            bool aboveHalfway = touch.position.y > 700f;

                            //transform.Rotate(Vector3.forward, rotationAmount, Space.World);
                            float rotationAmountX = swipeValue.x * rotationSpeed * (aboveHalfway ? -1f : 1f);

                            // Rotate the object around x axis
                            transform.Rotate(Vector3.forward, rotationAmountX, Space.World);


                            // Update the random number based on rotation direction
                            if (rotationAmountX < -10)
                                randomNumber++;
                            else if (rotationAmountX > 10)
                                randomNumber--;

                            // Ensure the number stays within range
                            if (randomNumber > 12)
                                randomNumber = 2;
                            else if (randomNumber < 2)
                                randomNumber = 12;

                            leftText.text = randomNumber.ToString();
                            oldTouchPosition = touch.position;
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        isDragging = false;
                        break;
                }
            }
        
        }
    }
}
