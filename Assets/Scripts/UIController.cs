using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject TimerContainer;
    public Text TimeElapsedLabel;
    public GameObject TapToPlayOverlay;
    public GameObject TapToPlayLabel;
    public float TapToPlayLabelToggleTime;
    public CameraController Camera;
    public GamePlayController GameplayController;

    private void Awake()
    {
        GameplayController.OnSessionStart += GameplayController_OnSessionStart;
        GameplayController.OnCompleted += GameplayController_OnCompleted;
        GameplayController.OnGameOver += GameplayController_OnGameOver;
        StartTogglingTapToPlay();
	}

    private void StartTogglingTapToPlay()
    {
        TapToPlayLabel.SetActive(!TapToPlayLabel.activeSelf);
        Invoke("StartTogglingTapToPlay", TapToPlayLabelToggleTime);
    }

    private void GameplayController_OnGameOver()
    {
        Camera.Orbit();
        TapToPlayOverlay.SetActive(true);
        TimerContainer.SetActive(false);
    }

    private void GameplayController_OnCompleted()
    {
        Debug.Log("Some UI to show here");
    }

    private void GameplayController_OnSessionStart()
    {
        Debug.Log("Whatever...");
    }

    public void OnPlayTapped()
    {
        Camera.SetViewForGamePlay();
        TapToPlayOverlay.SetActive(false);
        TimerContainer.SetActive(true);

        GameplayController.StartPlaying();
    }
}
