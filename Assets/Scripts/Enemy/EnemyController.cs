using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField]
    float attackRange;
    [SerializeField] Gun enemyGun;
    [SerializeField]
    float chaseAttackRange;

    [Header("No Need")]
    public bool canAttack = false;
    [SerializeField]
    float playerDistance = 10f;

    Rigidbody rb;
    GameObject player;
    Player playerController;
    [SerializeField]Animator animator;

    bool playerDead = false,slowMo = false;
    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (animator == null)
            animator = GetComponent<Animator>();
        if (enemyGun != null)
            enemyGun.enabled = false;

        StartCoroutine(DetectPlayer());
    }

    IEnumerator DetectPlayer()
    {
        yield return new WaitForSeconds(.1f);
        playerController = LevelManager.instance.player;
        player = LevelManager.instance.player.gameObject;
    }

    private void Update()
    {
        if (player == null && !playerDead)
            return;

        if (playerController != null)
        {
            if(playerDead == playerController.gameOver)
            {
                enemyGun.enabled = false;
                playerDead = true;
            }
        }
  
            

        playerDistance = (transform.position - player.transform.position).magnitude;


        if (playerDistance < attackRange && !canAttack)
        {
            canAttack = true;
            animator.SetTrigger("ShootPose");
            Invoke("EnableGUn", .5f);
        }

        if (playerDistance < attackRange + 2f && !slowMo)
        {
            TimeManager.instance.StartSlowMotion();
            slowMo = true;
        }
    }

    void EnableGUn()
    {
        if (enemyGun != null)
            enemyGun.enabled = true;
    }

    /*
    private void OnAnimatorIK()
    {
        if (canAttack && player != null)
        {
            //weapon aim at target
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, player.transform.position);

            //look at aim
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(player.transform.position);

        }

    }*/
}
