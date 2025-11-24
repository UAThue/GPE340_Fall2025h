using UnityEngine;

public class TestDeath : MonoBehaviour
{
    public Health healthComponentToTest;
    public Weapon weaponToTest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            healthComponentToTest.TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weaponToTest.OnTriggerPull.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            weaponToTest.OnTriggerRelease.Invoke();
        }


    }
}
