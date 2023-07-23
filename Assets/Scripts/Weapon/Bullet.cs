using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    [Header("These Values Can override in gun")]
    [SerializeField]
    [Range(0f, 50f)]
    float velocity = 20f;

    [SerializeField]
    [Range(0f, 100f)]
    int damage = 100;


    [SerializeField]
    LayerMask collideWithBulletMask;

    float lifeTimer;
    [SerializeField]
    float life = 3f;

    Rigidbody rb;
    
    public void StartingForceToBombs()
    {
        if(rb==null)
            rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * velocity * 5, ForceMode.Force);
    }

    private void Update()
    {

        transform.Translate(Vector3.forward * velocity * Time.deltaTime);

        //this makes destroy bullet atomatically
        if (Time.time > lifeTimer + life)
        {
            Dead();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Dead();

        Player playerHealth = collision.collider.GetComponent<Player>();
        EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); ;
        }
        Dead();

    }

    
    public void Fire(float bulletVelocity,int damageAmount)
    {

        lifeTimer = Time.time;

        velocity = bulletVelocity;
        damage = damageAmount;

    }
    
    void Dead()
    {
        Destroy(gameObject);
    }
}
