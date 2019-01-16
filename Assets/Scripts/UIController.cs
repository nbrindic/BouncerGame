using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject TimerContainer;
    public Text TimeElapsedLabel;
    public Text TimeElapsedGameOverLabel;
    public Text TimeElapsedCompletedLabel;
    public GameObject GameOverOverlay;
    public GameObject TapToPlayOverlay;
    public GameObject CompletedOverlay;
    public GameObject TapToPlayLabel;
    public GameObject TapToPlayAgainLabel;
    public GameObject TapToPlayAgain2Label;
    public float TapToPlayLabelToggleTime;
    public CameraController Camera;
    public GamePlayController GameplayController;

    private void Awake()
    {
        GameplayController.OnCompleted += GameplayController_OnCompleted;
        GameplayController.OnGameOver += GameplayController_OnGameOver;
        StartTogglingTapToPlay();
	}

    private void StartTogglingTapToPlay()
    {
        TapToPlayLabel.SetActive(!TapToPlayLabel.activeSelf);
        TapToPlayAgainLabel.SetActive(!TapToPlayAgainLabel.activeSelf);
        TapToPlayAgain2Label.SetActive(!TapToPlayAgain2Label.activeSelf);
        Invoke("StartTogglingTapToPlay", TapToPlayLabelToggleTime);
    }

    private void GameplayController_OnGameOver()
    {
        Camera.Orbit();
        TimerContainer.SetActive(false);
        TimeElapsedGameOverLabel.text = "Your Time: " + GameplayController.SessionTimeElapsed.ToString("N2");
        GameOverOverlay.SetActive(true);
    }

    private void GameplayController_OnCompleted()
    {
        Camera.Orbit();
        TimerContainer.SetActive(false);
        TimeElapsedCompletedLabel.text = "Your Time: " + GameplayController.SessionTimeElapsed.ToString("N2");
        CompletedOverlay.SetActive(true);
    }

    public void OnPlayTapped()
    {
        TapToPlayOverlay.SetActive(false);
        GameOverOverlay.SetActive(false);
        CompletedOverlay.SetActive(false);
        TimerContainer.SetActive(true);

        GameplayController.StartPlaying();
    }
}
