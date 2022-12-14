using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
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

    private IEnemyMovement enemyMovement;

    public EnemyStats Stats { get => _stats; set => _stats = value; }
    public bool ShouldChasePlayer { get => _shouldChasePlayer; set => _shouldChasePlayer = value; }
    public bool ShouldAttackPlayer { get => _shouldAttackPlayer; set => _shouldAttackPlayer = value; }
    public bool IsChasingPlayer { get => _isChasingPlayer; set => _isChasingPlayer = value; }
    public bool IsPatrolling { get => _isPatrolling; set => _isPatrolling = value; }
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public GameObject AimingPivot { get => _aimingPivot; set => _aimingPivot = value; }
    public GameObject BulletSpawn { get => _bulletSpawn; set => _bulletSpawn = value; }

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

        if (IsPlayerInRange())
            enemyMovement.LookAtPlayer();
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
                bullet.transform.position = BulletSpawn.transform.position;
                bullet.transform.localScale = bullet.transform.localScale * Stats.ProjectileScale;
                bullet.SetActive(true);
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

    public void TakeDamage()
    {

    }

    public void OnDeath()
    {

    }
}
