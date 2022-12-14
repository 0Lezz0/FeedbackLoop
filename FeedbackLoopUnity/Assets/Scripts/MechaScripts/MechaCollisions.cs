using UnityEngine;

public class MechaCollisions : MonoBehaviour
{
    private Rigidbody mechaRigidBody;
    private MechaStatus mechaStatus;
    // Start is called before the first frame update
    void Start()
    {
        mechaStatus = gameObject.GetComponent<MechaStatus>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameController.ENVIORMENT_TAG))
        {
            if (mechaStatus.IsFlying || mechaStatus.IsFalling)
            {
                mechaStatus.IsFlying = false;
                mechaStatus.IsFalling = false;
            }
            return;
        }

        EnemyBullet bullet;
        if(collision.gameObject.TryGetComponent(out bullet)){
            //Take damage using the properties on the bullet
            Debug.Log(string.Format("You have been hit for {0} by {1}", bullet.Damage, bullet.EnemyType));
            return; 
        }

    }
}
