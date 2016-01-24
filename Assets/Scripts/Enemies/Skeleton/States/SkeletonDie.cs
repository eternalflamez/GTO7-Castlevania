using UnityEngine;
using System.Collections;

public class SkeletonDie : FSMState
{
    private Animator animator;
    private GameObject player;

    public SkeletonDie(Animator animator, GameObject player)
    {
        this.stateID = StateID.Dead;
    }

    public override void Act(GameObject npc)
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("Jumping", false);
    }

    public override void Reason(GameObject npc)
    {
        // we dead m8
    }
}
