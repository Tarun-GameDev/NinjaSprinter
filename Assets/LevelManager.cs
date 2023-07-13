using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Player player;
    public UIManager uiManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<Player>();
        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
    }
}
