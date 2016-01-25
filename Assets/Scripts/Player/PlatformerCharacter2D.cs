using System;
using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField]
    private float m_MaxSpeed = 10f;
    [SerializeField]
    private float m_JumpForce = 400f;
    [SerializeField]
    private bool m_AirControl = false;
    [SerializeField]
    private LayerMask m_WhatIsGround;

    private Transform m_GroundCheck;
    const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    private Transform m_CeilingCheck;
    const float k_CeilingRadius = .01f;
    [SerializeField]
    private Animator m_Anim;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;

    private float standStillTimer;
    private bool healing = false;

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        this.transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"));
    }

    private void Update()
    {
        if(Mathf.Approximately(m_Anim.GetFloat("Speed"), 0))
        {
            standStillTimer += Time.deltaTime;

            if(healing && standStillTimer > 1)
            {
                DamageUIManager.instance.CreateDamageNumber(1, transform.position + new Vector3(0, .5f), true, true);
                standStillTimer = 0;
            }

            if(standStillTimer > 3)
            {
                healing = true;
                standStillTimer = 0;
            }
        }
        else
        {
            standStillTimer = 0;
            healing = false;
        }
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim.SetBool("Ground", m_Grounded);
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        m_Anim.SetBool("Damaged", false);
    }

    public void Attack()
    {
        m_Anim.SetBool("Attacking", true);
    }

    public void NoAttack()
    {
        m_Anim.SetBool("Attacking", false);
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (!crouch && m_Anim.GetBool("Crouch"))
        {
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        m_Anim.SetBool("Crouch", crouch);

        if (m_Grounded || m_AirControl)
        {
            move = (crouch ? 0 : move);

            m_Anim.SetFloat("Speed", Mathf.Abs(move));

            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }

        if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        {
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    public void TakeDamage()
    {
        m_Anim.SetBool("Damaged", true);
        healing = false;
        standStillTimer = 0;
    }

    public void HitEnemies()
    {
        int damage = 10;
        int range = 3;
        Vector2 direction = Vector2.right;

        if(!m_FacingRight)
        {
            direction = -direction;
        }

        LayerMask mask = LayerMask.GetMask("Enemy");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, mask);
        if(hit.collider != null)
        {
            hit.transform.GetComponent<Enemy>().TakeDamage(damage);
            DamageUIManager.instance.CreateDamageNumber(damage, hit.point);
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
