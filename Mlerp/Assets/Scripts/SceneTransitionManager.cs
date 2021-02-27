using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; } = null;

    [SerializeField]
    private float fadeTime = 2f;

    [SerializeField]
    private float timeToChangeScene = 2f;

    //Cached References
    private Image fadeImage = null;

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

        fadeImage = GetComponent<Image>();                     //Due to called on Start()
    }


    public void FadeOut()
    {
        fadeImage.CrossFadeAlpha(0, fadeTime, false);
    }

    public void FadeIn()
    {
        fadeImage.CrossFadeAlpha(1, fadeTime, false);
        Invoke(nameof(ChangeToNextScene), 1f);
    }

    private void ChangeToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex 
                               + 1);
    }

    public void SetAlpha(float amount)
    {
        fadeImage.canvasRenderer.SetAlpha(amount);
    }

    public float GetTimeToChangeScene()
    {
        return timeToChangeScene;
    }

    public void ActivateFadeImage()
    {
        fadeImage.enabled = true;
    }

    public void ChangeToPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex 
                               - 1);
    }

    public void ReloadThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
