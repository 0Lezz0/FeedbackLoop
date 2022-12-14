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

    public EnemyBullet(int damage, float impactForce, EnemyTypes enemyType)
    {
        Damage = damage;
        ImpactForce = impactForce;
        EnemyType = enemyType;
    }

}
