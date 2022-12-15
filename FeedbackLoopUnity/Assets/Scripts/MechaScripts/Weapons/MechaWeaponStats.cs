using UnityEngine;


[CreateAssetMenu(fileName = "MechaWeaponInstance", menuName = "MechaWeaponStats")]
public class MechaWeaponStats : ScriptableObject
{
    public string Name;
    public string FlavorText;
    public int BaseDamage;
    public int EffectiveRange;
    public float ProjectileSpeed, RateOfFire, TimeBetweenShots;
    public int ProjectilesPerVoley;
    public float Cooldown;
}
