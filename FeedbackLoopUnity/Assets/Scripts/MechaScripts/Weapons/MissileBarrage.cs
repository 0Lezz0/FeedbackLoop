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
        throw new System.NotImplementedException();
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
