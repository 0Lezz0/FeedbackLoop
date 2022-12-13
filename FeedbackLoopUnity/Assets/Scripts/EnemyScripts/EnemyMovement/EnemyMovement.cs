using System.Collections;

public interface EnemyMovement
{
    public IEnumerator Patrol();
    public IEnumerator MoveToPlayerRoutine();
    public void MoveToPlayer();
    public void GoBackToPatrol();
}
