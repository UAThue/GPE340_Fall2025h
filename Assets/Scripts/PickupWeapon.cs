using UnityEngine;

public class PickupWeapon : Pickup
{
    public Weapon weaponToPickUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        PawnRootMotion otherPawn = other.GetComponent<PawnRootMotion>();
        if (otherPawn != null)
        {
            otherPawn.EquipWeapon(weaponToPickUp);
            Destroy(this.gameObject);
        }

    }
}
