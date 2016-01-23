using UnityEngine;
using System.Collections;

public class SkeletonAttack : FSMState
{
    private Animator animator;
    private GameObject player;
    private float attackDistance;

    public SkeletonAttack(Animator animator, GameObject player, float attackDistance)
    {
        this.attackDistance = attackDistance;
        this.animator = animator;
        this.player = player;
        this.stateID = StateID.Attacking;
    }

    public override void Act(GameObject npc)
    {
        animator.SetBool("Attacking", true);
    }

    public override void Reason(GameObject npc)
    {
        if (Vector3.Distance(player.transform.position, npc.transform.position) > attackDistance)
        {
            (npc.GetComponent<Skeleton>()).SetTransition(Transition.LostPlayer);
        }
    }
}
