using UnityEngine;


[CreateAssetMenu(fileName = "EnemyInstance", menuName = "EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public string Name;
    public string Description;
    public int BaseHealth;
    public int BaseDamage;
    public int AttackPerBurst, AttackCoolDownInSeconds;
    public float BaseMovementSpeed;
    public float ProjectileSpeed, ProjectileNockBackForce, ProjectileScale, ProjectileDelay;
    public int Range;
    public EnemyTypes Type;
    public bool CanMove;
}
