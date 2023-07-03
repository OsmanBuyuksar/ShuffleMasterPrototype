using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static Action OnGameStart;
    public static Action OnGameLose;
    public static Action OnGameWin;
    public void StartGame()
    {
        OnGameStart?.Invoke();
        Debug.Log("Start");
    }
    public void GameOver()
    {
        OnGameLose?.Invoke();
        Debug.Log("Game Over");
    }
    public void GameWin()
    {
        OnGameWin?.Invoke();
        Debug.Log("Win");
    }
}
