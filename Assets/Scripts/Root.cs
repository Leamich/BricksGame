using System;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] Ball _ball;
    [SerializeField] Platform _platform;

    public event Action OnBrickDestroy;
    public event Action OnGameStart;

    public int Score { get; private set; }

    public void OnEnable()
    {
        OnBrickDestroy += () => { Score += 1; };
        OnGameStart?.Invoke();
    }

    public void InvokeBrickDestroy()
    {
        OnBrickDestroy?.Invoke();
    }

    public void EndGame()
    {

    }
}
