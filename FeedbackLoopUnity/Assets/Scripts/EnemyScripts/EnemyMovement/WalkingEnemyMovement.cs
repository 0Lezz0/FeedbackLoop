using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyMovement : MonoBehaviour, EnemyMovement
{
    private EnemyStats stats;
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
            float distance = movementDirection.magnitude;
            if (distance > stats.Range)
            {
                Vector3 targetPosition = new Vector3(playerPosition.transform.position.x,
                                                        gameObject.transform.position.y,
                                                        playerPosition.transform.position.z);
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, step);
            }
        }
    }

    public IEnumerator Patrol()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator MoveToPlayerRoutine()
    {
        while (true)
        {
            while (gameObject.GetComponent<Enemy>().ShouldChasePlayer)
            {
                float step = Time.deltaTime / stats.BaseMovementSpeed;
                Transform playerPosition = GameController.GetPlayerPosition();
                Vector3 movementDirection = playerPosition.position - gameObject.transform.position;
                float distance = movementDirection.magnitude;
                while (playerPosition != null && distance > stats.Range)
                {
                    movementDirection = playerPosition.position - gameObject.transform.position;
                    distance = movementDirection.magnitude;
                    Vector3 targetPosition = new Vector3(playerPosition.transform.position.x, 
                                                            gameObject.transform.position.y, 
                                                            playerPosition.transform.position.z);
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, step);
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<Enemy>().Stats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
