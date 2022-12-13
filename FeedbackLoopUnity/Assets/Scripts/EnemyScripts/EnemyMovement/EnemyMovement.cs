using System.Collections;

public interface EnemyMovement
{
    public IEnumerable Patrol();
    public void MoveToPlayer();
    public void GoBackToPatrol();
}
