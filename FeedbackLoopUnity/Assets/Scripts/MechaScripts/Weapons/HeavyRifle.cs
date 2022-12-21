using Config;
using System.Collections;
using UnityEngine;

public class HeavyRifle : MonoBehaviour, IMechaWeapon
{
    [SerializeField]
    private GameObject gunBarrel, aimingPivot;
    [SerializeField]
    private MechaWeaponStats stats;
    [SerializeField]
    private ParticleSystem muzzleFlash;

    private bool isFiring;

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
        if (!isFiring)
            StartCoroutine(FiringWeapon());
    }

    private IEnumerator FiringWeapon()
    {
        isFiring = true;
        RaycastHit hit;
        Ray ray = new Ray(gunBarrel.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, stats.EffectiveRange))
        {
            //AcitvateEffectOnHit(hit.point);
            if (hit.collider.CompareTag(ConstantsAndFixedValues.ENEMY))
            {
                if (hit.collider.gameObject.TryGetComponent(out IEnemy enemy))
                {
                    enemy.TakeDamage(stats.BaseDamage);
                }
                else
                {
                    enemy = hit.collider.gameObject.GetComponentInParent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(stats.BaseDamage);
                    }
                }
            }

        }
        yield return new WaitForSeconds(stats.TimeBetweenShots);
        isFiring = false;
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

    public ParticleSystem GetFiringEffect()
    {
        return muzzleFlash;
    }

    private void AcitvateEffectOnHit(Vector3 hitPoint)
    {
       
        GameObject particleHit = ParticleEffectPool.GetInstance().GetParticle();
        if (particleHit != null )
        {
            particleHit.SetActive(true);
            if (particleHit.TryGetComponent(out ParticleSystem heavyRifleHit))
            {
                Debug.Log("This should be showing something");
                heavyRifleHit.transform.position = hitPoint;
                heavyRifleHit.Play();
            }
        }
        ParticleEffectPool.GetInstance().ReturnParticleToPool(particleHit);
    }
}
