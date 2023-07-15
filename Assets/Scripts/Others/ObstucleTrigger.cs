using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstucleTrigger : MonoBehaviour
{
    [SerializeField] bool jumpBool = true;
    [SerializeField] GameObject emmisiveLine;
    [SerializeField] ParticleSystem lineBrokeEffect;
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if(other.CompareTag("Player"))
        {
            LevelManager.instance.uiManager.ActivateActionButton(jumpBool);
            if (emmisiveLine != null)
                emmisiveLine.SetActive(false);
            if (lineBrokeEffect != null)
                Instantiate(lineBrokeEffect, transform.position, Quaternion.identity);
        }
    }
}
