using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyStats _stats;
    private Vector3 patrollingStartPoint;
    private EnemyMovement enemyMovement;
    [SerializeField]
    private bool _shouldChasePlayer, _shouldAttackPlayer;
    [SerializeField]
    private bool _isChasingPlayer, _isPatrolling, _isAttacking; 


    public EnemyStats Stats { get => _stats; set => _stats = value; }
    public bool ShouldChasePlayer { get => _shouldChasePlayer; set => _shouldChasePlayer = value; }
    public bool ShouldAttackPlayer { get => _shouldAttackPlayer; set => _shouldAttackPlayer = value; }
    public bool IsChasingPlayer { get => _isChasingPlayer; set => _isChasingPlayer = value; }
    public bool IsPatrolling { get => _isPatrolling; set => _isPatrolling = value; }
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (Stats.CanMove)
        {
            enemyMovement = gameObject.GetComponent<EnemyMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldChasePlayer)
            ChasePlayer();
    }

    public void ChasePlayer()
    {
        if (Stats.CanMove)
        {
            enemyMovement.MoveToPlayer();
            IsChasingPlayer = true;
        }
    }
    public void StopChase()
    {
        if (IsChasingPlayer && Stats.CanMove)
        {
            Debug.Log("will get him one day");
            IsChasingPlayer = false;
        }
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
