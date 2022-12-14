using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour, IEnemyMovement
{
    private EnemyStats stats;
    private GameObject pivotPoint;

    public void LookAtPlayer()
    {
        Transform playerPosition = GameController.GetPlayerPosition();
        if (playerPosition != null)
        {
            pivotPoint.transform.LookAt(playerPosition);
        }
    }

    public void MoveToPlayer()
    {
        float step = Time.deltaTime * stats.BaseMovementSpeed;
        Transform playerPosition = GameController.GetPlayerPosition();
        if (playerPosition != null)
        {
            Vector3 movementDirection = playerPosition.position - gameObject.transform.position;
            float distance = movementDirection.magnitude;
            if (distance > stats.AttackRange)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerPosition.transform.position, step);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.TryGetComponent(out Enemy enemy))
        {

            stats = enemy.Stats;
            pivotPoint = enemy.AimingPivot;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
