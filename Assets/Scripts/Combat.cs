using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;
    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))  
       {
            Attack();
       }
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
