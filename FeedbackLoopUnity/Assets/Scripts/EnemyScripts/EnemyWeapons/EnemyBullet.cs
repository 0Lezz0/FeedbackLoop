using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _impactForce;
    [SerializeField]
    private EnemyTypes _enemyType;

    public int Damage { get => _damage; set => _damage = value; }
    public float ImpactForce { get => _impactForce; set => _impactForce = value; }
    public EnemyTypes EnemyType { get => _enemyType; set => _enemyType = value; }


    private void OnCollisionExit(Collision collision)
    {
        InitializeSelf();
    }

    private void OnCollisionStay(Collision collision)
    {
        InitializeSelf();
    }

    public void InitializeSelf()
    {
        Damage = 0;
        ImpactForce = 0;
        EnemyType = EnemyTypes.FlyinChaser;
        BulletPool.GetInstance().ReturnBulletToPool(gameObject);
    }

}
