using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public bool enemyDead;
    [SerializeField]
    ParticleSystem deadEffect;

    Material mat;


    private void Start()
    {
        currentHealth = maxHealth;
        enemyDead = false;
    }
    
    public void TakeDamage(int damage)
    {
        
        currentHealth -= damage;
        //damage vfx
        //var blood = objectPoller.SpawnFromPool("BloodParticleEffect", new Vector3(transform.position.x,(transform.position.y + 2),transform.position.z), Quaternion.identity);


        //if enemy dead
        if (currentHealth <= 0)
        {
            Dead();
        }
    }


    public void Dead()
    {
        enemyDead = true;
        if (deadEffect != null)
            Instantiate(deadEffect, transform.position, Quaternion.identity);

        TimeManager.instance.StopSlowMotion();
        Destroy(gameObject);
    }
}
