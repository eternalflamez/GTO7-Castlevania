using UnityEngine;
using System.Collections;

public class Bone : MonoBehaviour {

	public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public int damage;

    private Vector3 lastVector;
    private bool done;

    void Update()
    {
        if(done)
        {
            // Continue the vector
            this.transform.position += lastVector;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public void Fly()
    {
        StartCoroutine(SimulateProjectile());
    }
 
    IEnumerator SimulateProjectile()
    {
        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, Target.position);
 
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
 
        // Extract the X  Y component of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
 
        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Change direction if it's going the wrong way.
        Vector3 relativePoint = transform.InverseTransformPoint(Target.position);
        if ((relativePoint.x < 0.0 && Vx > 0) || (relativePoint.x > 0.0 && Vx < 0))
        {
            Vx *= -1;
        }

        float elapse_time = 0;
 
        while (elapse_time < flightDuration)
        {
            // Save last moved position
            Vector3 old = transform.position;
            transform.Translate(Vx * Time.deltaTime, (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);
            Vector3 newv = transform.position;

            lastVector = newv - old;

            elapse_time += Time.deltaTime;
 
            yield return null;
        }

        done = true;
    }  

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlatformerCharacter2D>().TakeDamage();
            DamageUIManager.instance.CreateDamageNumber(damage, other.contacts[0].point, true);
            Destroy(this.gameObject);
        }
    }
}
