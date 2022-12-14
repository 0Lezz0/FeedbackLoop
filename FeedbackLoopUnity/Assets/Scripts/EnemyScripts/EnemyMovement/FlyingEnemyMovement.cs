using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour, IEnemyMovement
{
    private EnemyStats stats;
    private GameObject pivotPoint;
    public void GoBackToPatrol()
    {
        Debug.Log("Going back to my normal flight patterns");
    }

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
            if (distance > stats.Range)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerPosition.transform.position, step);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Enemy enemy;
        if(gameObject.TryGetComponent(out enemy))
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
