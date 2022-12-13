using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyStats _stats;
    private Vector3 patrollingStartPoint;
    [SerializeField]
    private EnemyMovement enemyMovement;


    public EnemyStats Stats { get => _stats; set => _stats = value; }

    // Start is called before the first frame update
    void Start()
    {
        if(Stats.CanMove)
            enemyMovement = gameObject.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChasePlayer()
    {

    }
    public void AttackPlayer()
    {

    }

    public void TakeDamage()
    {

    }

    public void OnDeath()
    {

    }

    public void Patrol()
    {

    }
}
