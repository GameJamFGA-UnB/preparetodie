using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject AttackArea = default;
    [SerializeField] float movSpeed;
    [SerializeField] float dashSpeed;
    float currentSpeed;
    
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 mov;
    [SerializeField] Animator anim;

    [SerializeField] float dashCooldown;
    private float attackCooldown = 0.25f;
    private float timer = 0f;

    bool inDash;

    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    { 
        AttackArea = transform.GetChild(0).gameObject;
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

        if((Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))&& isAttacking == false)  
        {
            Attack();
        }
        if(isAttacking) 
        {
            timer += Time.deltaTime;

            if(timer>=attackCooldown)
            {
                timer = 0;
                isAttacking = false;
                AttackArea.SetActive(isAttacking);

            }
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

    void Dashed() 
    {
        inDash = false;
    }

    void Attack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        AttackArea.SetActive(isAttacking);
    }

}
