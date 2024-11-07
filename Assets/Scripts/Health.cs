using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    
    private int MAX_HEALTH = 100;
    // Update is called once per frame
    void Update()
    {
        /* if(Input.GetKeyDown(KeyCode.J))
        {
            Damage(10);
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        } */
    }

    public void Damage(int amount)
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }
        this.health -= amount;

        if(health <= 0) Die();
    }

    public void Heal(int amount)
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative heal");
        }

        if(health + amount > MAX_HEALTH)
        {
            this.health = MAX_HEALTH;
        }
        else this.health += amount;

    }

    private void Die()
    {
        Debug.Log("Im dead");
        Destroy(gameObject);
    }
}
