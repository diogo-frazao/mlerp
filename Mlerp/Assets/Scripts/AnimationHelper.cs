using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    public void InactivateObject()             //Called on the last frame of animation
    {
        gameObject.SetActive(false);
    }
}
