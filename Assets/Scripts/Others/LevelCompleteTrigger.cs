using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteTrigger : MonoBehaviour
{
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().buildIndex != 10)
                PlayerPrefs.SetInt("LevelUnlocked", SceneManager.GetActiveScene().buildIndex);
            else
                PlayerPrefs.SetInt("LevelUnlocked", 0);
            LevelManager.instance.player.LevelCompleted();
            LevelManager.instance.uiManager.LevelCompleted();
        }
    }
}
