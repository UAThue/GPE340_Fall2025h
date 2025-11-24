using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Weapon))]
public class WA_AutoProjectile : MonoBehaviour
{
    [Header("Data")]
    public ProjectileBullet pf_Bullet;
    public bool isFiring = false;
    private float nextShootTime;
    private Weapon weapon;

    public void Start() 
    {
        // Set our next shoot time to right now!
        nextShootTime = Time.time;

        // Get our weapon component
        weapon = GetComponent<Weapon>();
    }

    public void Update()
    {
        // If we are firing, try to shoot
        if (isFiring)
        {
            TryShoot();
        }
    }
    public void StartShooting()
    {
        isFiring = true;
    }

    public void StopShooting()
    {
        isFiring = false;
    }

    public void TryShoot()
    {
        // If it is time to shoot
        if (Time.time > nextShootTime)
        {
            // Check for ammo
            if (weapon.ammoCount > 0)
            {
                // Shoot 
                Shoot();
            }
            else
            {
                // Or run the Out of Ammo Event
                weapon.OnOutOfAmmo.Invoke();
            }

            // Set the time we can try to shoot again
            nextShootTime = Time.time + +(1 / weapon.fireRate);
        }
    }

    public void Reload()
    {
        weapon.ammoCount = weapon.maxAmmo;
    }

    public void Shoot()
    {
        //Actually Shoot
        ProjectileBullet bullet = Instantiate<ProjectileBullet>(pf_Bullet, weapon.firePoint.position, weapon.firePoint.rotation) as ProjectileBullet;
        if (bullet != null)
        { 
            bullet.damageDone = weapon.damageDone;
        }

        // Rotate the bullet based on our accuracy (this way it isn't perfectly aimed)
        bullet.transform.Rotate(0, weapon.GetAccuracyModifiedRotationDegrees(),0);

        // Subtract a bullet
        weapon.ammoCount--;

        // Do our weapon shoot event
        weapon.OnShoot.Invoke();
    }


}
