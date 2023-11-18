using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEnding : MonoBehaviour
{
    public float fadeDuration;
    public bool isPlayerAtExit;
    private bool isPlayerCaught;
    
    public CanvasGroup exitCG;
    public CanvasGroup caugthGroup;
    public AudioSource win;
    public AudioSource lose;
    private bool hasAudioPlayed;
    private PlayerController player;
    public Animator needKey;
    private float timer;
    private float displayImageDuration = 5;


    private void Start()
    {
        needKey = FindObjectOfType<NeedKey>().GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitCG, false, win);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caugthGroup, true, lose);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            if (player.isGotKey)
            {
                isPlayerAtExit = true;
            } else
            {
                needKey.SetBool("IsFinished", true);
            }
            
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            if (player.isGotKey)
            {
                isPlayerAtExit = true;
            }
            else
            {
                needKey.SetBool("IsFinished", false);
            }

        }
    }

    private void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audio)
    {
        if (!hasAudioPlayed)
        {
            audio.Play();
            hasAudioPlayed = true;
        }
        player.footstep.Stop();
        player.isActivePlayer = false;
        timer = timer + Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;
        
        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            } else
            {
                Application.Quit();
            }
        }
    }
}
