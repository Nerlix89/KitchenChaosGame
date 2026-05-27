using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    public static GameManager Instance { get; private set;}

    public Action OnStateChanged;
    public Action<bool> OnPauseStateChanged;

    private State currentState;
    [SerializeField] private float waitingToStartTimer = 1f;
    [SerializeField] private float countdownToStartTimer = 3f;
    [SerializeField] private float gamePlayingTimerMax = 10f;

    private float gamePlayingTimer;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        currentState = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0f)
                {
                    currentState = State.CountdownToStart;
                    if (OnStateChanged != null) OnStateChanged();
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    currentState = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    if (OnStateChanged != null) OnStateChanged();
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f)
                {
                    currentState = State.GameOver;
                    if (OnStateChanged != null) OnStateChanged();
                }
                break;
            case State.GameOver:
                break;
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public bool IsGamePlaying()
    {
        return currentState == State.GamePlaying;
    }

    public bool IsCountdownToStart()
    {
        return currentState == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return currentState == State.GameOver;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (OnPauseStateChanged != null) OnPauseStateChanged(isGamePaused);

        if (isGamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        
    }
}
