using Config;
using UnityEngine;

public class MechaCollisions : MonoBehaviour
{
    private MechaStatus mechaStatus;

    // Start is called before the first frame update
    void Start()
    {
        mechaStatus = gameObject.GetComponent<MechaStatus>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ConstantsAndFixedValues.ENVIORMENT_TAG))
        {
            if (mechaStatus.IsFlying || mechaStatus.IsFalling)
            {
                mechaStatus.IsFlying = false;
                mechaStatus.IsFalling = false;
            }
        }

        if(collision.gameObject.TryGetComponent(out EnemyBullet bullet)){
            mechaStatus.TakeDamage(bullet.Damage);
        }

    }
}
