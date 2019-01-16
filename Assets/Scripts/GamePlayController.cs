using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public Text TimeElapsedLabel;
    public GameObject BallPrefab;
    public Transform BallSpawnPoint;
    public FinishController FinishController;

    public event Action OnSessionStart;
    public event Action OnCompleted;
    public event Action OnGameOver;

    private BallController _ballInstance;
    private bool _isPlaying;
    private float _sessionTimeElapsed;

    private void Awake()
    {
        FinishController.OnFinishReached += FinishController_OnFinishReached;
    }

    public void StartPlaying()
    {
        _ballInstance = Instantiate(BallPrefab, BallSpawnPoint.position, Quaternion.identity, transform).GetComponent<BallController>();
        _ballInstance.OnHitDeadWall += _ballInstance_OnHitDeadWall;
        _sessionTimeElapsed = 0f;
        TimeElapsedLabel.text = _sessionTimeElapsed.ToString("N2");
        _isPlaying = true;

        OnSessionStart();
    }

    private void _ballInstance_OnHitDeadWall()
    {
        _ballInstance.OnHitDeadWall -= _ballInstance_OnHitDeadWall;
        OnGameOver();

        Debug.Log("Ball should be destroyed here...");
    }

    private void FinishController_OnFinishReached()
    {
        Debug.Log("Finish reached");
        OnCompleted();
    }

    private void Update()
    {
        if (_isPlaying)
        {
            _sessionTimeElapsed += Time.deltaTime;
            TimeElapsedLabel.text = _sessionTimeElapsed.ToString("N2");
        }
    }
}
