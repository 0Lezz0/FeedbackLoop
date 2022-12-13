using UnityEngine;

public class WalkingEnemyMovement : MonoBehaviour, IEnemyMovement
{
    private EnemyStats stats;
    private GameObject pivotPoint;
    public void GoBackToPatrol()
    {
        Debug.Log("Going back  to my morning stroll");
    }

    public void MoveToPlayer()
    {
        float step = Time.deltaTime * stats.BaseMovementSpeed;
        Transform playerPosition = GameController.GetPlayerPosition();
        if (playerPosition != null)
        {
            Vector3 movementDirection = playerPosition.position - gameObject.transform.position;
            Vector3 bodyRotationDirection = new Vector3(movementDirection.x, 0, movementDirection.z);
            float distance = movementDirection.magnitude;
            Quaternion targetBodyRotation = Quaternion.LookRotation(bodyRotationDirection);
            Quaternion targetPivotRotation = Quaternion.LookRotation(movementDirection);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetBodyRotation, 1);
            pivotPoint.transform.rotation = Quaternion.Slerp(pivotPoint.transform.rotation, targetPivotRotation, 1);

            if (distance > stats.Range)
            {
                Vector3 targetPosition = new Vector3(playerPosition.transform.position.x,
                                                        gameObject.transform.position.y,
                                                        playerPosition.transform.position.z);
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, step);
            }
        }
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