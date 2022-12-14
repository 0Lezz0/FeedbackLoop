using System.Collections;
using UnityEngine;

public class StationaryEnemyMovement : MonoBehaviour, IEnemyMovement
{
    private EnemyStats stats;
    private GameObject pivotPoint;

    public void MoveToPlayer()
    {
       //This class can't move
    }

    public void LookAtPlayer()
    {
        Transform playerPosition = GameController.GetPlayerPosition();
        if (playerPosition != null)
        {
            Vector3 movementDirection = playerPosition.position - gameObject.transform.position;
            float distance = movementDirection.magnitude;
            if (distance <= stats.TargetingRange)
            {
                pivotPoint.transform.LookAt(playerPosition);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.TryGetComponent(out Enemy enemy))
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
