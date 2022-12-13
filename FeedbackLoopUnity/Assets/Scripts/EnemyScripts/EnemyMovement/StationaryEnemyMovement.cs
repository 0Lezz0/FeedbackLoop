using System.Collections;
using UnityEngine;

public class StationaryEnemyMovement : MonoBehaviour, IEnemyMovement
{
    private EnemyStats stats;
    private GameObject pivotPoint;
    public void GoBackToPatrol()
    {
        Debug.Log("Patrlon't");
    }

    public void MoveToPlayer()
    {
        Transform playerPosition = GameController.GetPlayerPosition();
        if (playerPosition != null)
        {
            Vector3 movementDirection = playerPosition.position - gameObject.transform.position;
            float distance = movementDirection.magnitude;
            if (distance <= stats.Range)
            {
                pivotPoint.transform.LookAt(playerPosition);
            }
        }
    }

    public IEnumerator MoveToPlayerRoutine()
    {
        return null;
    }

    public IEnumerator Patrol()
    {
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        Enemy enemy;
        if (gameObject.TryGetComponent(out enemy))
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
