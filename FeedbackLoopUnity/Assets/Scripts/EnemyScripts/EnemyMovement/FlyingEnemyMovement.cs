using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour, EnemyMovement
{
    private EnemyStats stats;
    public void GoBackToPatrol()
    {
        Debug.Log("Going back to my normal flight patterns");
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

    public IEnumerator MoveToPlayerRoutine()
    {
        while(true) {
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
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerPosition.transform.position, step);
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public IEnumerator Patrol()
    {
        throw new System.NotImplementedException();
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
