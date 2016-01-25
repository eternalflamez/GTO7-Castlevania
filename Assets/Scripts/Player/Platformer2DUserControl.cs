using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    private PlatformerCharacter2D m_Character;
    private bool m_Jump;
    private bool m_Crouch;
    private float m_H;

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }

    private void Update()
    {
#if !UNITY_ANDROID
        // For pc only, crossplatforminputmanager my butthole.
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetAxis("Vertical") > 0;
        }

        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            m_Character.Attack();
        }
        else
        {
            m_Character.NoAttack();
        }

        // Read the inputs.
        m_Crouch = CrossPlatformInputManager.GetAxis("Vertical") < 0;
        m_H = CrossPlatformInputManager.GetAxis("Horizontal");
#endif
    }

    private void FixedUpdate()
    {
#if !UNITY_ANDROID
        // Pass all parameters to the character control script.
        m_Character.Move(m_H, m_Crouch, m_Jump);
        m_Jump = false;
#endif
    }
}