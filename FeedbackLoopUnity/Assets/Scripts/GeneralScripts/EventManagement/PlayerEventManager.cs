using UnityEngine;

public static class PlayerEventManager
{
    public delegate void _OnPlayerDamage(int damageTaken);
    public static event _OnPlayerDamage OnPlayerDamage;

    public delegate void _OnPlayerHeal(int healtHealed);
    public static event _OnPlayerHeal OnPlayerHeal;

    public delegate void _OnPlayerDeath();
    public static event _OnPlayerDeath OnPlayerDeath;

    public static void PlayerTakesDamage(int damageTaken) => OnPlayerDamage?.Invoke(damageTaken);
    public static void PlayerHeals(int healtHealed) => OnPlayerHeal?.Invoke(healtHealed);
    public static void PlayerDies() => OnPlayerDeath?.Invoke();
}
