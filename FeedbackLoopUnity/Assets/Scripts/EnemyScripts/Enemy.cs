using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyStats _stats;
    [SerializeField]
    private GameObject _aimingPivot;
    [SerializeField]
    private bool _shouldChasePlayer, _shouldAttackPlayer;
    [SerializeField]
    private bool _isChasingPlayer, _isPatrolling, _isAttacking;

    private IEnemyMovement enemyMovement;

    public EnemyStats Stats { get => _stats; set => _stats = value; }
    public bool ShouldChasePlayer { get => _shouldChasePlayer; set => _shouldChasePlayer = value; }
    public bool ShouldAttackPlayer { get => _shouldAttackPlayer; set => _shouldAttackPlayer = value; }
    public bool IsChasingPlayer { get => _isChasingPlayer; set => _isChasingPlayer = value; }
    public bool IsPatrolling { get => _isPatrolling; set => _isPatrolling = value; }
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public GameObject AimingPivot { get => _aimingPivot; set => _aimingPivot = value; }

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = gameObject.GetComponent<IEnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldChasePlayer)
            ChasePlayer();
    }

    public void ChasePlayer()
    {
        enemyMovement.MoveToPlayer();
        if (Stats.CanMove)
        {
            IsChasingPlayer = true;
        }
    }
    public void StopChase()
    {
        if (IsChasingPlayer)
        {
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
