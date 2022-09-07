public class EnemyMovement : HexaEventCallback
{
    [ComponentInject] private EnemyScript enemyScript;

    private UnitMovement unitMovement;

    private new void Awake()
    {
        unitMovement = gameObject.AddComponent<UnitMovement>();
        base.Awake();   
    }

    protected override void OnEnemyMove(EnemyScript enemy, Hex tile)
    {
        if (enemy.Id == enemyScript.Id)
        {
            unitMovement.GoToDestination(tile, 2f);
        }
    }

    protected override void OnEnemyMovingFinished(EnemyScript enemy, Hex hex)
    {
        if (enemy.Id == enemyScript.Id)
        {
            enemyScript.ActionFinished();
        }
    }
}
