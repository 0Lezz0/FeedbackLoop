using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBarrage : MonoBehaviour, IMechaWeapon
{
    [SerializeField]
    private GameObject missileOrigin;
    [SerializeField]
    private MechaWeaponStats stats;
    public void AimWeapon()
    {
        throw new System.NotImplementedException();
    }

    public void FireWeapon()
    {
        Debug.Log("Imagine i'm discharging an alarming ammount of missiles.");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
