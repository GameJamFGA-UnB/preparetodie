using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movSpeed;
    [SerializeField] float dashSpeed;
    float currentSpeed;
    
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 mov;
    [SerializeField] Animator anim;

    [SerializeField] float dashCooldown;
    [SerializeField] float attackCooldown;

    bool inDash;

    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    { 
        currentSpeed = movSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpeed == movSpeed)
        {
            mov.x = Input.GetAxisRaw("Horizontal");
            mov.y = Input.GetAxisRaw("Vertical");
        }

        anim.SetFloat("Horizontal", mov.x);
        anim.SetFloat("Vertical", mov.y);
        anim.SetFloat("Speed", mov.sqrMagnitude); 

        mov.Normalize();

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift))&& mov != Vector2.zero && inDash == false) 
        {
            inDash = true;
            currentSpeed = dashSpeed;
            Invoke("PosDash", 0.1f);
        } 

        if ((Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))&& isAttacking == false)  
       {
            isAttacking = true;
            anim.SetTrigger("Attack");
            Invoke("PosAttack", 0.1f);
       }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + mov * currentSpeed * Time.fixedDeltaTime);
    }

    void PosDash() 
    {
        currentSpeed = movSpeed;
        Invoke("Dashed", dashCooldown);
    }
    void PosAttack() 
    {
        currentSpeed = movSpeed;
        Invoke("Attacked", attackCooldown);
    }

    void Dashed() 
    {
        inDash = false;
    }
    void Attacked()
    {
        isAttacking = false;
    }
}
