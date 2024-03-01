using UnityEngine;
using TMPro;

public class RotateDiskRight : MonoBehaviour
{
    private float oldTouchPosition;
    public TMP_Text rightText;
    private int randomNumber;
    private bool isDragging = false;

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
                Touch touch = Input.GetTouch(0);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    // Touch occurred on this object
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            oldTouchPosition = touch.position.x;
                            isDragging = true;
                            break;

                        case TouchPhase.Moved:
                            if (isDragging)
                            {
                                Vector2 swipeValue = new Vector2(touch.position.x - oldTouchPosition, 0f);
                                float rotationSpeed = 0.5f;
                                bool aboveHalfway = touch.position.y > 700f;
                                float rotationAmount = swipeValue.x * rotationSpeed * (aboveHalfway ? -1f : 1f);
                                transform.Rotate(Vector3.forward, rotationAmount, Space.World);

                                // Update the random number based on rotation direction
                                if (rotationAmount < -10)
                                    randomNumber++;
                                else if (rotationAmount > 10)
                                    randomNumber--;

                                // Ensure the number stays within range
                                if (randomNumber > 12)
                                    randomNumber = 2;
                                else if (randomNumber < 2)
                                    randomNumber = 12;

                                rightText.text = randomNumber.ToString();
                                oldTouchPosition = touch.position.x;
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
