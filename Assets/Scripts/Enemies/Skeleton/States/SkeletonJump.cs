using UnityEngine;
using System.Collections;

public class SkeletonJump : FSMState
{
    private Animator animator;

    public SkeletonJump(Animator animator)
    {
        this.animator = animator;
        this.stateID = StateID.Jumping;
    }

    public override void Act(GameObject npc)
    {
        animator.SetBool("Jumping", true);
    }

    public override void Reason(GameObject npc)
    {
        // Automatically end jump through animation event
    }
}
