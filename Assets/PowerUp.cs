using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ShieldActivate();
            triggered = true;
            Destroy(gameObject);
        }
    }
}
