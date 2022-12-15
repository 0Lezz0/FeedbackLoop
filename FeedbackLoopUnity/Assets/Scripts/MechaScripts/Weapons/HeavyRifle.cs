using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyRifle : MonoBehaviour, IMechaWeapon
{
    [SerializeField]
    private GameObject gunBarrel, aimingPivot;
    [SerializeField]
    private MechaWeaponStats stats;

    public void AimWeapon()
    {
        RaycastHit hit;
        Ray ray = new Ray(gunBarrel.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.DrawRay(gunBarrel.transform.position, ray.direction * hit.distance, Color.red);
            aimingPivot.transform.LookAt(hit.point);
        }

        /*
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.Ra

        float hitDist = 0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }
        */
        
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

    private void FixedUpdate()
    {
        AimWeapon();
    }
}
