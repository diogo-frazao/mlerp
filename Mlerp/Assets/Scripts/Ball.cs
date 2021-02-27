using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float timeToStartFalling = 3f;

    [SerializeField]
    private float ballTorque = 5f;

    [SerializeField]
    private float minYValue = -5.53f;                       //If yPos lesser than this number, loses

    [SerializeField]
    private Transform[] spawnPositions = new Transform[3];

    //Cached References                                                     
    private Rigidbody2D myRigidBody = null;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        SelectRandomSpawnPosition();

        Invoke(nameof(StartFalling), timeToStartFalling);
    }

    private void SelectRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Length);

        transform.position = spawnPositions[randomIndex].position;
    }

    private void StartFalling()
    {
        myRigidBody.isKinematic = false;
        myRigidBody.AddTorque(ballTorque);
    }

    private void Update()
    {
        CheckIfIsAlive();
    }

    private void CheckIfIsAlive()
    {
        if (transform.position.y <= minYValue &&
            GameManager.Instance.GetIsAlive())               //Called only once
        {
            GameManager.Instance.LoseBehaviour();
        }
    }
}
