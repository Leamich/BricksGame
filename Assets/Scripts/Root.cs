using System;
using TMPro;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] Ball _ball;
    [SerializeField] Platform _platform;
    [SerializeField] GameObject _endGameWindow;
    [SerializeField] int _winScore;
    [SerializeField] int _maxScore;

    public event Action OnBrickDestroy;
    public event Action OnGameStart;
    public event Action OnGameEnd;

    public bool IsPlaying { get; private set; }
    public int Score { get; private set; } = 0;

    public void OnEnable()
    {
        OnBrickDestroy += () => { Score += 1; };
    }

    public void InvokeBrickDestroy()
    {
        OnBrickDestroy?.Invoke();
        if (Score == _maxScore) EndGame();
    }
    
    public void StartGame()
    {
        IsPlaying = true;
        OnGameStart?.Invoke();
    }

    public void EndGame()
    {
        IsPlaying = false;

        OnGameEnd?.Invoke();

        _endGameWindow.SetActive(true);
        _endGameWindow.GetComponentInChildren<TextMeshProUGUI>().SetText(
            (Score >= _winScore) ?
            $"Win with score {Score}" :
            $"Lose with score {Score}"
        );
    }
}