using UnityEngine;


[CreateAssetMenu(fileName = "EnemyInstance", menuName = "EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public string Name;
    public string Description;
    public int BaseHealth;
    public int BaseDamage;
    public float BaseMovementSpeed;
    public float AttackPerSecond, ProjectileSpeed, ProjectileNockBackForce, ProjectileScale;
    public int Range;
    public EnemyTypes Type;
    public bool CanMove;
}
