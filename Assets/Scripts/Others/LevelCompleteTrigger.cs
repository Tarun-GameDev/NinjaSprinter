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
            PlayerPrefs.SetInt("LevelUnlocked", SceneManager.GetActiveScene().buildIndex);
            LevelManager.instance.player.LevelCompleted();
            LevelManager.instance.uiManager.LevelCompleted();
        }


    }
}
