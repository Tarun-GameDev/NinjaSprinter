//using SupersonicWisdomSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniManager : MonoBehaviour
{
    public static UniManager instance;
    public bool isSupersonicReady;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // Subscribe
        //SupersonicWisdom.Api.AddOnReadyListener(OnSupersonicWisdomReady);
        // Then initialize
        //SupersonicWisdom.Api.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSupersonicWisdomReady()
    {
        // Start your game from this point
        isSupersonicReady = true;
    }
}
