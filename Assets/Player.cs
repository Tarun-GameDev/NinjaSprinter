using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 500f;
    [SerializeField] float jumpForce = 500f;
    [SerializeField] Vector3 crouchOffset;
    Rigidbody rb;
    [SerializeField]
    CapsuleCollider normalCol,crouchCol;
    [SerializeField] bool gameOver = false;
    [SerializeField] Animator animator;


    void Start()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
        if (normalCol == null)
            normalCol = GetComponent<CapsuleCollider>();
        gameOver = false;
    }

    private void Update()
    {
        if (gameOver)
            return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (animator != null)
                animator.SetTrigger("Jump");
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Crouch());
        }

    }


    void FixedUpdate()
    {
        if (gameOver)
            return;

        rb.position += Vector3.right * moveSpeed * Time.fixedDeltaTime;
        
    }

    IEnumerator Crouch()
    {
        crouchCol.enabled = true;
        normalCol.enabled = false;
        if (animator != null)
            animator.SetTrigger("Slide");
        yield return new WaitForSeconds(1f);
        normalCol.enabled = true;
        crouchCol.enabled = false;

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
    }

    void LevelFailed()
    {
        gameOver = true;
        LevelManager.instance.uiManager.LevelFailed();
    }
}
