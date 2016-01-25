using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour {

    public abstract void TakeDamage(int amount);

    public abstract float GetYOffset();

    protected abstract void Die();
}
