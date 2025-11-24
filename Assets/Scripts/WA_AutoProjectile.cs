using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Weapon))]
public class WA_AutoProjectile : MonoBehaviour
{
    [Header("Data")]
    public GameObject pf_Bullet;
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
        //TODO: Actually Shoot
        // Temp: Say "bang"
        Debug.Log("Bang!");

        // Subtract a bullet
        weapon.ammoCount--;
    }


}
