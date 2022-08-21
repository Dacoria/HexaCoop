public class EnemyAttack : HexaEventCallback
{
    [ComponentInject] private EnemyScript enemyScript;

    private UnitMovement unitMovement;

    private new void Awake()
    {
        unitMovement = gameObject.AddComponent<UnitMovement>();
        base.Awake();
    }

    protected override void OnEnemyAttack(EnemyScript enemy, PlayerScript player)
    {
        if (enemy.Id == enemyScript.Id)
        {
            var unitAttack = gameObject.GetSet<UnitAttack>();
            unitAttack.AttackUnitOnHex(player.CurrentHexTile);
        }
    }

    protected override void OnEnemyAttackHit(EnemyScript enemy, Hex hex, int damage)
    {
        if (enemy.Id == enemyScript.Id)
        {
            enemyScript.ActionFinished();
        }
    }
}
