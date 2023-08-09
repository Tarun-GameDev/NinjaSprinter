using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using SupersonicWisdomSDK;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playingMenu;
    [SerializeField] GameObject levelFailedMenu;
    [SerializeField] GameObject levelCompletedMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject actionButton;
    [SerializeField] Vector2 ButtonRandomLimits;
    bool jumpAction = true;
    TimeManager timeManager;
    bool actionDisabled = false;
    public static bool gamePaused = false;

    private void Start()
    {
        actionButton.SetActive(false);
        RandomButtonPlace();
        timeManager = TimeManager.instance;

        //SupersonicWisdom.Api.NotifyLevelStarted(SceneManager.GetActiveScene().buildIndex, null);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playingMenu.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        playingMenu.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void LevelFailed()
    {
        playingMenu.SetActive(false);
        levelFailedMenu.SetActive(true);

        //SupersonicWisdom.Api.NotifyLevelFailed(SceneManager.GetActiveScene().buildIndex, null);
    }

    void RandomButtonPlace()
    {
        actionButton.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-ButtonRandomLimits.x, ButtonRandomLimits.x), Random.Range(-ButtonRandomLimits.y, ButtonRandomLimits.y), 0f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActivateActionButton(bool _jumpAction)
    {
        jumpAction = _jumpAction;
        StartCoroutine(StartAction());
    }
    IEnumerator StartAction()
    {
        actionButton.SetActive(true);
        timeManager.StartSlowMotion();
        actionDisabled = false;
        yield return new WaitForSecondsRealtime(1.5f);
        StopAction();
    }

    void StopAction()
    {
        if (!actionDisabled)
        {
            actionButton.SetActive(false);
            timeManager.StopSlowMotion();
            RandomButtonPlace();
            actionDisabled = true;
        }
    }

    public void ActionButton()
    {
        LevelManager.instance.player.Action(jumpAction);
        StopAction();
    }

    public void LevelCompleted()
    {
        StartCoroutine(LevelCompleteActive());

        //SupersonicWisdom.Api.NotifyLevelCompleted(SceneManager.GetActiveScene().buildIndex, null);
    }

    IEnumerator LevelCompleteActive()
    {
        yield return new WaitForSeconds(2f);
        levelCompletedMenu.SetActive(true);
        playingMenu.SetActive(false);
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
