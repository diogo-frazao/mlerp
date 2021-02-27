using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBall : MonoBehaviour
{
    [SerializeField]
    private float torqueAmount = 30f;

    private bool hasCollided = false;

    //Cached References
    private Rigidbody2D myRigidBody = null;


    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        GetComponent<Animator>().SetTrigger("stop");

        myRigidBody.isKinematic = false;
        myRigidBody.AddTorque(torqueAmount);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hasCollided)
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(FadeAndChangeToGameScene());
            hasCollided = true;
        }
    }

    private IEnumerator FadeAndChangeToGameScene()
    {
        yield return new WaitForSeconds(SceneTransitionManager.Instance.
                                        GetTimeToChangeScene());

        SceneTransitionManager.Instance.ActivateFadeImage();
        SceneTransitionManager.Instance.SetAlpha(0f);
        SceneTransitionManager.Instance.FadeIn();
    }
}
