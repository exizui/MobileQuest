using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public static Fader instance;

    public Animator animator;

    private const string FadeInTrigger = "FadeIn";
    private const string FadeOutTrigger = "FadeOut";

    [Header("Fade Settings")]
    [SerializeField] private float fadeInSpeed = 2f;   
    [SerializeField] private float fadeOutSpeed = 2f;  

    private float fadeSpeed;
    private float defaultSpeed;

  
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator FadeIn()
    {
        animator.SetTrigger(FadeInTrigger);
        yield return new WaitForSeconds(GetAnimationLength(FadeInTrigger) / fadeInSpeed);
        //ResetSpeed();
    }

    

    public IEnumerator FadeOut()
    {
        animator.SetTrigger(FadeOutTrigger);
        yield return new WaitForSeconds(GetAnimationLength(FadeOutTrigger) / fadeOutSpeed);
        //ResetSpeed();
    }

    private float GetAnimationLength(string clipName)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                return clip.length;
        }
        return 1f;
    }

    public void SetSpeed(float speed)
    {
        fadeSpeed = speed;
        animator.speed = fadeSpeed;
    }
    private void ResetSpeed()
    {
        fadeSpeed = defaultSpeed;
        animator.speed = defaultSpeed;
    }
}
