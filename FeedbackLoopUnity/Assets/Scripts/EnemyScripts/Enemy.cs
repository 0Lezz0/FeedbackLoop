using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField]
    private EnemyStats _stats;
    [SerializeField]
    private GameObject _aimingPivot;
    [SerializeField]
    private GameObject _bulletSpawn;
    [SerializeField]
    private bool _shouldChasePlayer, _shouldAttackPlayer;
    [SerializeField]
    private bool _isChasingPlayer, _isPatrolling, _isAttacking;
    [SerializeField]
    private bool _returnToPoolOnDeath;

    private IEnemyMovement enemyMovement;
    private HealthSystem healthSystem;

    public EnemyStats Stats { get => _stats; set => _stats = value; }
    public bool ShouldChasePlayer { get => _shouldChasePlayer; set => _shouldChasePlayer = value; }
    public bool ShouldAttackPlayer { get => _shouldAttackPlayer; set => _shouldAttackPlayer = value; }
    public bool IsChasingPlayer { get => _isChasingPlayer; set => _isChasingPlayer = value; }
    public bool IsPatrolling { get => _isPatrolling; set => _isPatrolling = value; }
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public GameObject AimingPivot { get => _aimingPivot; set => _aimingPivot = value; }
    public GameObject BulletSpawn { get => _bulletSpawn; set => _bulletSpawn = value; }
    public bool ReturnToPoolOnDeath { get => _returnToPoolOnDeath; set => _returnToPoolOnDeath = value; }

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = gameObject.GetComponent<IEnemyMovement>();
        healthSystem = gameObject.GetComponent<HealthSystem>();
        healthSystem.InitializeHealth(Stats.BaseHealth);
        healthSystem.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldChasePlayer)
            ChasePlayer();

        if (IsPlayerInRange())
            enemyMovement.LookAtPlayer();

        if (healthSystem.IsDead())
            OnDeath();
    }

    private void FixedUpdate()
    {
        if (IsPlayerInRangeOfAttack() && ShouldAttackPlayer)
        {
            StartAttackPlayer();
        }
    }

    public void ChasePlayer()
    {
        if (Stats.CanMove)
        {
            enemyMovement.MoveToPlayer();
            IsChasingPlayer = true;
        }
    }
    
    private void StartAttackPlayer()
    {
        if(!IsAttacking)
            StartCoroutine(AttackPlayer());
    }
    public IEnumerator AttackPlayer()
    {
        IsAttacking = true;
        for (int i = 0; i < Stats.AttackPerBurst; i++)
        {
            GameObject bullet = BulletPool.GetInstance().GetBullet();
            if (bullet != null)
            {
                EnemyBullet currentBullet = bullet.GetComponent<EnemyBullet>();
                bullet.transform.position = BulletSpawn.transform.position;
                bullet.transform.localScale = bullet.transform.localScale * Stats.ProjectileScale;
                bullet.SetActive(true);
                currentBullet.Damage = Stats.BaseDamage;
                currentBullet.EnemyType = Stats.Type;
                currentBullet.ImpactForce = Stats.ProjectileNockBackForce;
                bullet.GetComponent<Rigidbody>().AddForce(AimingPivot.transform.forward.normalized * Stats.ProjectileSpeed, ForceMode.VelocityChange);

                yield return new WaitForSeconds(Stats.ProjectileDelay);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
        yield return new WaitForSeconds(Stats.AttackCoolDownInSeconds);
        IsAttacking = false;
    }
    
    public bool IsPlayerInRange()
    {
        Transform playerPosition = GameController.GetPlayerPosition();
        if (playerPosition != null)
        {
            Vector3 movementDirection = playerPosition.position - gameObject.transform.position;
            float distance = movementDirection.magnitude;
            return distance <= Stats.TargetingRange;
        }
        return false;
    }
    public bool IsPlayerInRangeOfAttack()
    {
        Transform playerPosition = GameController.GetPlayerPosition();
        if (playerPosition != null)
        {
            Vector3 movementDirection = playerPosition.position - gameObject.transform.position;
            float distance = movementDirection.magnitude;
            return distance <= Stats.AttackRange;
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
    }

    public void OnDeath()
    {
        if (ReturnToPoolOnDeath)
        {
            gameObject.SetActive(false);
            healthSystem.Revive();
            //Returns the enemy to its pool
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
