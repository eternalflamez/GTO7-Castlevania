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

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
