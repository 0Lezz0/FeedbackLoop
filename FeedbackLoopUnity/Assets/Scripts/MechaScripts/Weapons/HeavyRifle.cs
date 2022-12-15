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
        Ray ray = new Ray(gunBarrel.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Camera.main.farClipPlane))
        {
            aimingPivot.transform.LookAt(hit.point);
        }
    }

    public void FireWeapon()
    {
        RaycastHit hit;
        Ray ray = new Ray(gunBarrel.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, stats.EffectiveRange))
        {
            if (hit.collider.CompareTag(GameController.ENEMY))
            {
                Enemy enemy;
                if (hit.collider.gameObject.TryGetComponent(out enemy))
                {
                    enemy.TakeDamage(stats.BaseDamage);
                    Debug.DrawRay(gunBarrel.transform.position, ray.direction * hit.distance, Color.cyan);
                }
                else
                {
                    enemy = hit.collider.gameObject.GetComponentInParent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(stats.BaseDamage);
                        Debug.DrawRay(gunBarrel.transform.position, ray.direction * hit.distance, Color.white);
                    }
                }
            }
        }
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
