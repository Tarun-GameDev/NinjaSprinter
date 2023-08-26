using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstucleTriggeer : MonoBehaviour
{
    bool triggered = false;
    [SerializeField] GameObject obstucle;
    [SerializeField] Vector3 spawnPosOffset;    //"Use yPos -2.2 for shuriken"

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if(other.CompareTag("MoveObstucleCol"))
        {
            triggered = true;
            if(obstucle != null)           
            LevelManager.instance.player.SpawnmoveObstucle(obstucle,spawnPosOffset);
            Invoke("slowMo", .15f);
        }
    }

    void slowMo()
    {
        TimeManager.instance.StartSlowMotion();
    }
}
