using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playingLevel;
    [SerializeField] GameObject levelFailed;
    [SerializeField] RectTransform actionButton;
    [SerializeField] Vector2 ButtonRandomLimits;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            RandomButtonPlace();
    }

    public void LevelFailed()
    {
        playingLevel.SetActive(false);
        levelFailed.SetActive(true);
    }

    public void RandomButtonPlace()
    {
        actionButton.localPosition = new Vector3(Random.Range(-ButtonRandomLimits.x, ButtonRandomLimits.x), Random.Range(-ButtonRandomLimits.y, ButtonRandomLimits.y), 0f);
    }
}
