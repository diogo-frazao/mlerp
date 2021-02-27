using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } = null;

    [SerializeField]
    private Image scoreImage = null; 

    [SerializeField]
    private Text scoreText = null;              // Its a child of scoreImage, so moving the scoreImage will move the text

    [SerializeField]
    private Text loseScoreText = null;

    [SerializeField]
    private RectTransform[] scorePositions = new RectTransform[7];

    private int score = 0;
    private int currentScorePosition = -1;

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

    private void Start()
    {
        scoreImage.gameObject.SetActive(false);
    }

    public void AddAndShowScore()
    {
        score++;
        scoreText.text = score.ToString();

        MoveToNextPos();
    }

    private void MoveToNextPos()
    {
        scoreImage.gameObject.SetActive(true);

        if (currentScorePosition < scorePositions.Length - 1)
        {
            currentScorePosition++;
        }
        else
        {
            currentScorePosition = 0;
        }

        scoreImage.rectTransform.position = scorePositions
                                            [currentScorePosition].position;
    }

    public int GetCurrentScore()
    {
        return score;
    }

    public void SetLoseScoreText()
    {
        loseScoreText.text = score.ToString();
    }
}
