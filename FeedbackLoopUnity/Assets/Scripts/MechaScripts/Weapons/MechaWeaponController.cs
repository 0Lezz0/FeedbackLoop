using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaWeaponController : MonoBehaviour
{
    private MechaControls controlSystem;
    private MechaControls.MechaActionsActions mechaActions;

    private MechaStatus mechaStatus;

    [SerializeField]
    private GameObject _mainWeapon, _specialWeapon;

    [SerializeField]
    private bool _shouldAttackMain, _shouldAttackSpecial;

    public GameObject MainWeapon { get => _mainWeapon; set => _mainWeapon = value; }
    public GameObject SpecialWeapon { get => _specialWeapon; set => _specialWeapon = value; }
    public bool ShouldAttackMain { get => _shouldAttackMain; set => _shouldAttackMain = value; }
    public bool ShouldAttackSpecial { get => _shouldAttackSpecial; set => _shouldAttackSpecial = value; }

    // Start is called before the first frame update
    void Start()
    {
        controlSystem = new MechaControls();
        mechaActions = controlSystem.MechaActions;
        mechaActions.Enable();

        mechaStatus = gameObject.GetComponent<MechaStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mechaActions.Shootmain.WasPerformedThisFrame())
        {
            ShouldAttackMain = true;
        }

        if (mechaActions.Shootspecial.WasPerformedThisFrame())
        {
            ShouldAttackSpecial = true;
        }
    }

    private void FixedUpdate()
    {
        if (ShouldAttackMain)
        {
            ShouldAttackMain = false;
            MainWeapon.GetComponent<IMechaWeapon>().FireWeapon();
        }
        
        if (ShouldAttackSpecial)
        {
            ShouldAttackSpecial = false;
            SpecialWeapon.GetComponent<IMechaWeapon>().FireWeapon();
        }
    }
}
