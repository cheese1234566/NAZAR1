using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
   private enum State
    {
        WaitingToStart,
        CountdownToStart,
        Gameplay,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1;
    private float countdownToStartTimer = 3;
    private float gameplayTimerMax = 30;
    private float gameplayTimer;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0)
                {
                    state = State.Gameplay;
                    gameplayTimer = gameplayTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Gameplay:
                gameplayTimer -= Time.deltaTime;
                if (gameplayTimer <= 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.Gameplay;
    }

    public bool IsCountdownToStart()
    {
        return state == State.CountdownToStart;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetPlayingTimerNormalized()
    {
        float leftTime = gameplayTimer / gameplayTimerMax;
        return leftTime;
    }
}
