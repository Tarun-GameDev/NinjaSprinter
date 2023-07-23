using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityStandardAssets.CrossPlatformInput;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform attackPoint;

    [SerializeField]
    GameObject BulletPrefab;
    [Range(0f, 50f)]
    public float velocity = 20f;
    [Range(0f, 100f)]
    public int damage;
    [SerializeField] float timeBetweenShooting, timeBetweenShots;
    public int bulletsPerTap;
    [SerializeField] bool allowButtonToHold;

    [Header("Bug Fixing")]
    [SerializeField] bool allowInvoke = true;

    [Header("Manager")]
    public bool playerEquipped;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Rig playerIk;
    public bool enemyEquipped;
    [SerializeField] Animator enemyAnimator;

    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField]
    bool shooting, readyToShoot;


    //GameManager gameManager;
    [SerializeField]
    bool pcControllersOn = true;

    float resetWeightIn = 2f;
    bool resetedWeight = false;

    TimeManager timeManager;

    private void Start()
    {
        //gameManager = GameManager.instance;
        //pcControllersOn = gameManager.PCControllersOn;
        timeManager = TimeManager.instance;

        Invoke("Delay", .15f);
        if(playerIk != null)
            playerIk.weight = 0f;
    }

    private void Awake()
    {
        shooting = false;
        readyToShoot = true;
    }

    private void Update()
    {
        if(playerEquipped || !enemyEquipped)
        {
            MyInput();  
            playerEquipped = true;
        }
        else if(enemyEquipped)
        {
            EnemyInput();
        }


        if (resetWeightIn <= 0f && !resetedWeight)
        {
            playerIk.weight = 0f;
            resetedWeight = true;
        }
        if(resetWeightIn >= 0f)
            resetWeightIn -= Time.deltaTime;

    }

    void MyInput()
    {
        if (pcControllersOn)
        {
            if (allowButtonToHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        }


        if (allowButtonToHold) shooting = CrossPlatformInputManager.GetButton("Shoot");
        else shooting = CrossPlatformInputManager.GetButtonDown("Shoot");

        if (playerEquipped && readyToShoot && shooting)
        {
            StartCoroutine(playerGunShoot());
        }
        else if (shooting && readyToShoot)
        {
            Shoot();
        }
    }

    IEnumerator playerGunShoot()
    {

        if (resetedWeight == false)
        {
            yield return new WaitForSeconds(.1f);
            Shoot();
        }
        else
            Shoot();

        playerIk.weight = 1f;
        resetWeightIn = 2f;
        resetedWeight = false;

        if (timeManager != null)
            if (timeManager.InSlowMotion)
                timeManager.StopSlowMotion();
    }


    public void ShootPlayerGun()
    {
        if(readyToShoot)
            Shoot();
    }

    void EnemyInput()
    {
        //atomaticaly shoot
        if (readyToShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        readyToShoot = false;

        if (enemyEquipped)
        {
            if(enemyAnimator != null)
                enemyAnimator.SetTrigger("Shoot");

        }   
        /*
        else if(playerEquipped)
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetLayerWeight(1, 1f);
                playerAnimator.SetTrigger("Shoot");
            }    
        }*/

        if (muzzleFlash != null)
            muzzleFlash.Play();

        #region BulletSpawning

        var go = Instantiate(BulletPrefab, attackPoint.position, attackPoint.rotation);
        var bullet = go.GetComponent<Bullet>();
        bullet.Fire(velocity,damage);


        #endregion
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        //allow shootin and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

}
