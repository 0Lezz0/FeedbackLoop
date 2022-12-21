using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth, _currentHealth;
    [SerializeField]
    private bool _isInvulnerable;
    [SerializeField]
    private float _invulnerabilityTime;

    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public bool IsInvulnerable { get => _isInvulnerable; set => _isInvulnerable = value; }
    public float InvulnerabilityTime { get => _invulnerabilityTime; set => _invulnerabilityTime = value; }

    // Start is called before the first frame update
    void Start()
    {
        Revive();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool TakeDamage(int damage)
    {
        if(damage > 0 && !IsDead() && !IsInvulnerable)
        {
            CurrentHealth -= damage;
            if(InvulnerabilityTime > 0)
            {
                StartCoroutine(StartIframes());
            }
            return true;
        }
        return false;
            
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    private IEnumerator StartIframes()
    {
        IsInvulnerable = true;
        yield return new WaitForSeconds(InvulnerabilityTime);
        IsInvulnerable = false;
    }

    public void Revive()
    {
        CurrentHealth = MaxHealth;
        IsInvulnerable = false;
    }

    public void InitializeHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
        Revive();
    }
}

public class HealthUpdateReport
{
    public int HealthOffset { get; set; }
    public int HealthUpdatedValue { get; set; }
}
