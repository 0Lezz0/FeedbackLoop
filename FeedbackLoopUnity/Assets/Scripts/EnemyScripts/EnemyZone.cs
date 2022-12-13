using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    [SerializeField]
    private bool isPlayerInTheZone;
    [SerializeField]
    private List<Enemy> enemiesInTheZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameController.PLAYER_TAG))
        {
            isPlayerInTheZone = true;
            foreach (Enemy enemy in enemiesInTheZone)
            {
                enemy.ShouldChasePlayer = isPlayerInTheZone;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(GameController.PLAYER_TAG))
        {
            isPlayerInTheZone = false;
            foreach (Enemy enemy in enemiesInTheZone)
            {
                enemy.ShouldChasePlayer = isPlayerInTheZone;
            }
        }
    }
}
