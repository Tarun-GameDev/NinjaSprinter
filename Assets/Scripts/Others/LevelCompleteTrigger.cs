using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            LevelManager.instance.player.LevelCompleted();
            LevelManager.instance.uiManager.LevelCompleted();
        }


    }
}
