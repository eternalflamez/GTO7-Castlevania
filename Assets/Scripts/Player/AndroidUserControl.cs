using UnityEngine;
using System.Collections;

public class AndroidUserControl : MonoBehaviour
{
    [SerializeField]
    private GameObject androidControls;
    private bool leftButtonHeld;
    private bool rightButtonHeld;
    private PlatformerCharacter2D character;

    private bool crouching;
    private bool jumping;
    private bool attacking;

    // Use this for initialization
    void Start()
    {
        character = GetComponent<PlatformerCharacter2D>();

#if UNITY_ANDROID
        androidControls.SetActive(true);
#endif
    }

    private void FixedUpdate()
    {
#if UNITY_ANDROID
        if (attacking && !jumping)
        {
            character.Attack();
        }
        else
        {
            character.NoAttack();
        }

        float horizontal = 0;

        if(leftButtonHeld)
        {
            horizontal = -1;
        }
        else if(rightButtonHeld)
        {
            horizontal = 1;
        }

        character.Move(horizontal, crouching, jumping);
#endif
    }

    public void LeftButtonPressed()
    {
        leftButtonHeld = true;
    }

    public void LeftButtonReleased()
    {
        leftButtonHeld = false;
    }

    public void RightButtonPressed()
    {
        rightButtonHeld = true;
    }

    public void RightButtonReleased()
    {
        rightButtonHeld = false;
    }

    public void JumpButtonPressed()
    {
        jumping = true;
    }

    public void JumpButtonReleased()
    {
        jumping = false;
    }

    public void CrouchButtonPressed()
    {
        crouching = true;
    }

    public void CrouchButtonReleased()
    {
        crouching = false;
    }

    public void AttackButtonPressed()
    {
        attacking = true;
    }

    public void AttackButtonReleased()
    {
        attacking = false;
    }
}
