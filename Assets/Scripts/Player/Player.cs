using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 500f;
    [SerializeField] float jumpForce = 500f;
    [SerializeField] Vector3 crouchOffset;
    Rigidbody rb;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    bool isGrounded = true;
    [SerializeField] float raycastDistance = 1f;
    [SerializeField]
    CapsuleCollider normalCol,crouchCol;
    bool crouching = false;
    [SerializeField]
    ParticleSystem dustEffect;
    [HideInInspector]
    public bool gameOver = false;
    [SerializeField] bool levelCompleted = false;
    [SerializeField] Animator animator;
    [SerializeField] GameObject playerModel;
    [SerializeField] ParticleSystem playerDeadEffect;
    [SerializeField] Transform gunTarget;

    [Header("Tyre Obstucel")]
    [SerializeField] Transform tyreSpawnPos;
    [SerializeField] GameObject tyrePrefab;

    [Header("PowerUps")]
    [SerializeField] float shieldLifeTime = 15f;
    [SerializeField] bool inShield = false;
    [SerializeField] GameObject shieldObj;

    [Header("Audio")]
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource crouchAudio;
    [SerializeField] AudioSource levelWinAudio;
    [SerializeField] AudioSource levelFailedAudio;
    [SerializeField] PlayableDirector endCutSceneDir;
    int currentHealth = 100, maxHealth = 100;
    TimeManager timeManager;

    void Start()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();

        if (timeManager == null)
            timeManager = TimeManager.instance;
        gameOver = false;
        currentHealth = maxHealth;

        Invoke("startRigPos", 1f);
    }

    void startRigPos()
    {
        gunTarget.localPosition = new Vector3(1.633f, 1.153f, -.49f);
    }

    private void Update()
    {
        if (gameOver || levelCompleted)
            return;

         isGrounded = CheckGround();

        #region PcCOntrollers
        if ((Input.GetKeyDown(KeyCode.Space) && isGrounded))
        {
            Jump();
            DisableSlowMotion();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Crouch());
            DisableSlowMotion();
        }
        #endregion

        #region AndroidControllers
        if ((CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded))
        {
            Jump();
            DisableSlowMotion();
        }

        if (CrossPlatformInputManager.GetButtonDown("Crouch"))
        {
            StartCoroutine(Crouch());
            DisableSlowMotion();
        }
        #endregion
    }

    void DisableSlowMotion()
    {
        if (timeManager != null)
            if (timeManager.InSlowMotion)
                timeManager.StopSlowMotion();
    }

    void FixedUpdate()
    {
        if (gameOver || levelCompleted)
            return;

        rb.position += Vector3.right * moveSpeed * Time.fixedDeltaTime;
        animator.SetBool("move", true);
    }

    void Jump()
    {
        if (jumpAudio != null)
            jumpAudio.Play();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        if (animator != null)
            animator.SetTrigger("Jump");
    }

    IEnumerator Crouch()
    {
        if (crouchAudio != null)
            crouchAudio.Play();
        if (animator != null)
            animator.SetTrigger("Slide");
        yield return new WaitForSeconds(.15f);
        if (dustEffect != null && isGrounded)
            dustEffect.Play();
        crouchCol.enabled = true;
        normalCol.enabled = false;
        crouching = true;
        yield return new WaitForSeconds(.85f);
        normalCol.enabled = true;
        crouchCol.enabled = false;
        crouching = false;

    }
    private bool CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Gizmos.DrawRay(ray);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Obstucle"))
        {
            Vector3 collisonNormal = collision.contacts[0].normal;

            if(Mathf.Abs(collisonNormal.y) < 0.5f && Mathf.Abs(collisonNormal.x) > 0.5f || Mathf.Abs(collisonNormal.z) > 0.5f)
            {
                LevelFailed();
            }
        }

        if(!inShield && collision.collider.CompareTag("MovingObstucle"))
        {
            LevelFailed();
        }

        if(collision.collider.CompareTag("Enemy") )
        {
            collision.collider.GetComponent<EnemyHealth>().Dead();
        }
    }

    public void TakeDamage(int damage)
    {
        if (inShield)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
                LevelFailed();
    }

    void LevelFailed()
    {
        gameOver = true;
        rb.isKinematic = true;
        normalCol.isTrigger = true;
        crouchCol.isTrigger = true;
        playerModel.SetActive(false);
        if (playerDeadEffect != null)
            Instantiate(playerDeadEffect, transform.position, Quaternion.identity);
        if (levelFailedAudio != null)
            levelFailedAudio.Play();
        LevelManager.instance.uiManager.LevelFailed();
        CinemachineShake.instance.CameraShake(3f, .4f);
    }

    public void LevelCompleted()
    {
        levelCompleted = true;
        if (endCutSceneDir != null)
            endCutSceneDir.Play();
        if (levelWinAudio != null)
            levelWinAudio.Play();
        animator.SetBool("move", false);
        animator.SetTrigger("Finish");
    }

    public void Action(bool _jumpAction)
    {
        if (_jumpAction)
        {
            Jump();
        }
        else
            StartCoroutine(Crouch());
    }

    public void SpawnmoveObstucle(GameObject _obstucle,Vector3 _yPosOffset)
    {
        Instantiate(_obstucle, tyreSpawnPos.position + _yPosOffset, Quaternion.identity);
        CinemachineShake.instance.CameraShake(1f, 2f);
    }

    #region PowerUps

    public void ShieldActivate()
    {
        StartCoroutine(Shield());
    }

    IEnumerator Shield()
    {
        inShield = true;
        if (shieldObj != null)
            shieldObj.SetActive(true);
        yield return new WaitForSeconds(shieldLifeTime);
        inShield = false;
        if (shieldObj != null)
            shieldObj.SetActive(false);
    }


    #endregion
}
