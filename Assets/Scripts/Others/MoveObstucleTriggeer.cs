using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstucleTriggeer : MonoBehaviour
{
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if(other.CompareTag("MoveObstucleCol"))
        {
            triggered = true;
            LevelManager.instance.player.SpawnTyre();
            Invoke("slowMo", .3f);
        }
    }

    void slowMo()
    {
        TimeManager.instance.StartSlowMotion();
    }
}
