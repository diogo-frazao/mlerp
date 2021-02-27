using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    [SerializeField]
    private GameObject pausePanel = null;

    [SerializeField]
    private GameObject losePanel = null;

    private bool isPaused = false;
    private bool isAlive = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;               
        }

        PauseBehaviour();
    }

    private void PauseBehaviour()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
    }

    public void LoseBehaviour()
    {
        isAlive = false;
        losePanel.SetActive(true);

        ScoreManager.Instance.SetLoseScoreText();
        FindObjectOfType<PaddleController>().SmoothVanish();            //The paddle disappears smoothly
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }
}
