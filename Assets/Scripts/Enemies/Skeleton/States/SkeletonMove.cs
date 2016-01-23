using UnityEngine;
using System.Collections;

public class SkeletonMove : FSMState {
    private Animator animator;
    private GameObject player;
    private float jumpTimer;
    private float attackDistance;

    public SkeletonMove(Animator animator, GameObject player, float attackDistance)
    {
        this.attackDistance = attackDistance;
        this.animator = animator;
        this.player = player;
        this.stateID = StateID.Idle;
    }

    public override void Act(GameObject npc)
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("Jumping", false);
    }

    public override void Reason(GameObject npc)
    {
        if (Vector3.Distance(player.transform.position, npc.transform.position) < attackDistance)
        {
            (npc.GetComponent<Skeleton>()).SetTransition(Transition.DetectedPlayer);
            jumpTimer = 0;
        }

        jumpTimer += Time.fixedDeltaTime;
        if(jumpTimer > 1.5f)
        {
            (npc.GetComponent<Skeleton>()).SetTransition(Transition.Jumping);
            jumpTimer = 0;
        }
    }
}
