using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaCollisions : MonoBehaviour
{

    private const string ENVIORMENT_TAG = "Enviorment";

    private Rigidbody mechaRigidBody;
    private MechaStatus mechaStatus;
    // Start is called before the first frame update
    void Start()
    {

        mechaStatus = gameObject.GetComponent<MechaStatus>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ENVIORMENT_TAG))
        {
            if (mechaStatus.IsFlying || mechaStatus.IsFalling)
            {
                mechaStatus.IsFlying = false;
                mechaStatus.IsFalling = false;
            }
        }
    }
}
