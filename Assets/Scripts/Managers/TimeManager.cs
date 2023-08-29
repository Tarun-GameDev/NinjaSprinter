using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
	public static TimeManager instance;
    public float slowMotionTimeScale;
    private float startTimeScale;
    private float startFixedDeltaTime;
    public bool InSlowMotion = false;
    [SerializeField] GameObject slowMotionVolume;

    

    float currentslowMoResetTimer,maxSlowMoReseTimer = 3f;
    bool slowMoReset = false;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        startTimeScale = 1f;
        startFixedDeltaTime = 0.02f;

        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }

    private void Update()
    {
        if (currentslowMoResetTimer <= 0f && !slowMoReset)
        {
            StopSlowMotion();
            slowMoReset = true;
        }
        if (currentslowMoResetTimer >= 0f)
            currentslowMoResetTimer -= Time.unscaledDeltaTime;

    }

    public void StartSlowMotion()
	{
        InSlowMotion = true;
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;

        if (slowMotionVolume != null)
            slowMotionVolume.SetActive(true);

        currentslowMoResetTimer = maxSlowMoReseTimer;
        slowMoReset = false;
	}
    

	public void StopSlowMotion()
    {
        InSlowMotion = false;
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;

        if (slowMotionVolume != null)
            slowMotionVolume.SetActive(false);
    }

    public void StartSetSlowMotion(float _slowMotionTime)
    {
        InSlowMotion = true;
        Time.timeScale = _slowMotionTime;
        Time.fixedDeltaTime = startFixedDeltaTime * _slowMotionTime;
    }

}