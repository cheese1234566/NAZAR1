using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        player.pauseText.SetActive(true);
        player.isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        player.pauseText.SetActive(false);
        player.isPaused = false;
    }
}
