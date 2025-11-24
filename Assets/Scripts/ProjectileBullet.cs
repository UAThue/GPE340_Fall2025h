using UnityEngine;

public class ProjectileBullet : Projectile
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        Debug.Log("!");
        Move();
        base.Update();
    }

    public override void Move()
    {
        // Move Forward
        transform.position += transform.forward * projecileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            //Pawn otherPawn = other.GetComponent<Pawn>();
            Health otherHealth = other.GetComponent<Health>();
            if (otherHealth != null) 
            {
                otherHealth.TakeDamage(damageDone);
            }
        }

        Destroy(gameObject);
    }

}
