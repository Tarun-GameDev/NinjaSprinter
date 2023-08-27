using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonUI : MonoBehaviour
{
    [SerializeField] GameObject button;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            button.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
