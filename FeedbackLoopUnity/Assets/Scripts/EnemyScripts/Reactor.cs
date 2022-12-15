using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour, IEnemy
{
    [SerializeField]
    private EnemyStats _stats;

    private HealthSystem healthSystem;

    public EnemyStats Stats { get => _stats; set => _stats = value; }

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = gameObject.GetComponent<HealthSystem>();
        healthSystem.InitializeHealth(Stats.BaseHealth);
        healthSystem.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.IsDead())
            OnDeath();
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
    }

    public void OnDeath()
    {
        Debug.Log("The reactor is destroyed. You are a monster");
        GameController.NextLoop();
    }
}
