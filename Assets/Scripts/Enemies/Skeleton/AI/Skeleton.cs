using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class Skeleton : MonoBehaviour {

    [SerializeField]
    private float attackDistance;
    private GameObject player;
    [SerializeField]
    private GameObject bone;
    private FSMSystem fsmSystem;
    private Animator animator;

    private bool visible;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlatformerCharacter2D>().gameObject;
        animator = GetComponent<Animator>();

        fsmSystem = new FSMSystem();

        SkeletonAttack sa = new SkeletonAttack(animator, player, attackDistance);
        sa.AddTransition(Transition.LostPlayer, StateID.Idle);
        sa.AddTransition(Transition.Dying, StateID.Dead);

        SkeletonJump sj = new SkeletonJump(animator);
        sj.AddTransition(Transition.StopJump, StateID.Idle);
        sj.AddTransition(Transition.Dying, StateID.Dead);
        sj.AddTransition(Transition.Jumping, StateID.Jumping);

        SkeletonMove sm = new SkeletonMove(animator, player, attackDistance);
        sm.AddTransition(Transition.Jumping, StateID.Jumping);
        sm.AddTransition(Transition.Dying, StateID.Dead);
        sm.AddTransition(Transition.DetectedPlayer, StateID.Attacking);

        fsmSystem.AddState(sm);
        fsmSystem.AddState(sj);
        fsmSystem.AddState(sa);

        visible = false;
	}

    void FixedUpdate()
    {
        if (visible)
        {
            fsmSystem.CurrentState.Reason(gameObject);
            fsmSystem.CurrentState.Act(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        if (visible)
        {
            Vector3 adjustedPos = player.transform.position;
            adjustedPos.y = this.transform.position.y;

            transform.LookAt(adjustedPos);
            Vector3 euler = transform.rotation.eulerAngles;
            euler.y += 90;

            Quaternion quaternion = Quaternion.identity;
            quaternion.eulerAngles = euler;

            transform.rotation = quaternion;
        }
	}

    public void OnBecameInvisible()
    {
        visible = false;
    }

    public void OnBecameVisible()
    {
        visible = true;
    }

    public void ThrowBone()
    {
        Bone bone = Instantiate(this.bone).GetComponent<Bone>();
        bone.Target = player.transform;
        bone.transform.position = this.transform.position;

        bone.Fly();

    }

    public void StopJump()
    {
        animator.SetBool("Jumping", false);
        SetTransition(Transition.StopJump);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void SetTransition(Transition t) { fsmSystem.PerformTransition(t); }
}
