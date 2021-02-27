using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [Header("Clamped Values")]
    [SerializeField]
    private float minX = 0.4f;

    [SerializeField]
    private float maxX = 4.18f;

    [SerializeField]
    private float minY = -3.84f;

    [SerializeField]
    private float maxY = 2.4f;

    [Space]
    [SerializeField]
    private float moveToMouseSpeed = 0.50f;

    [Header("Rotation Related")]
    [SerializeField]
    private float halfScreen = 1.87f;

    [SerializeField]
    private float rotationAmount = 4f;

    [SerializeField]
    private float noRotationRange = 0.2f;                   //from the half of the screen + this and - this rotation = 0

    [SerializeField]
    private float rotationSpeed = 2f;

    [Header("Sound Related")]
    [SerializeField]
    private AudioSource hitSound = null;

    [SerializeField]
    private AudioSource hitFeedbackSound = null;

    private int rotationDirection = 0;                      // -1 when > half screen, 1 < half screen

    //Cached References
    private Camera mainCamera = null;

    private void Start()
    {
        mainCamera = Camera.main;

        SceneTransitionManager.Instance.ActivateFadeImage();
        SceneTransitionManager.Instance.SetAlpha(1);
        SceneTransitionManager.Instance.FadeOut();
    }

    private void Update()
    {
        FollowMousePosition();
        RotateAccordingToPosition();
        ClampPosition();
    }

    private void FollowMousePosition()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        transform.position = Vector2.Lerp(transform.position, desiredPosition, 
                                          moveToMouseSpeed * Time.deltaTime);

    }

    private void RotateAccordingToPosition()
    {
        SetRotationDirection();

        Vector3 rotateVector = new Vector3(0f, 0f, (transform.position.x + rotationAmount) 
                                                    * rotationDirection);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                             Quaternion.Euler(rotateVector), rotationSpeed * Time.deltaTime); 
    }

    private void ClampPosition()
    {
        float xClamped = Mathf.Clamp(transform.position.x, minX, maxX);
        float yClamped = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector2(xClamped, yClamped);
    }

    private void SetRotationDirection()
    {
        if (transform.position.x >= halfScreen)                                 //Right
        {
            rotationDirection = 1;
        }
        else if (transform.position.x <= halfScreen + noRotationRange &&        
                 transform.position.x >= halfScreen - noRotationRange)          // half - x <= xPos <= half + x
        {
            rotationDirection = 0;
        }
        else if (transform.position.x < halfScreen - noRotationRange &&
                 transform.position.x >= halfScreen - noRotationRange - 1)      //Close Left
        {
            rotationDirection = -1;
        }
        else                                                                    //Far Left
        {
            rotationDirection = -3;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            hitSound.Play();
            hitFeedbackSound.Play();

            ScoreManager.Instance.AddAndShowScore();
        }
    }

    public void SmoothVanish()
    {
        GetComponent<Animator>().SetTrigger("vanish");
    }
}
