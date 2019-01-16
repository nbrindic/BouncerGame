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
    }

    private void _ballInstance_OnHitDeadWall()
    {
        _ballInstance.OnHitDeadWall -= _ballInstance_OnHitDeadWall;
        Destroy(_ballInstance.gameObject);
        _isPlaying = false;
        OnGameOver();        
    }

    private void FinishController_OnFinishReached()
    {
        _isPlaying = false;
        Destroy(_ballInstance.gameObject, 3f);
        OnCompleted();
    }

    private void Update()
    {
        if (_isPlaying)
        {
            _sessionTimeElapsed += Time.deltaTime;
            TimeElapsedLabel.text = _sessionTimeElapsed.ToString("N2");

            if (Input.GetKeyUp(KeyCode.Q))
                FinishController_OnFinishReached();
            if (Input.GetKeyUp(KeyCode.W))
                _ballInstance_OnHitDeadWall();
        }
    }
}
