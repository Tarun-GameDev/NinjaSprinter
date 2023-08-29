using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreak : MonoBehaviour
{
    bool triggered = false;
    [SerializeField] GameObject glassModel;
    [SerializeField] ParticleSystem glassBreakParticle;

    

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if(other.CompareTag("Player"))
        {
            triggered = true;
            
            if (glassModel != null)
                glassModel.SetActive(false);
            if (glassBreakParticle != null)
                Instantiate(glassBreakParticle, transform.position, Quaternion.identity);
            AudioManager.instance.Play("GlassSmash");
            CinemachineShake.instance.CameraShake(2f, .4f);
        }
    }
}
