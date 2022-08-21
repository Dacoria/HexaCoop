using System.Collections.Generic;
using UnityEngine;

public class TooltipEnemy : MonoBehaviour, ITooltipUIText
{
    [ComponentInject] private EnemyHealth enemyHealth;
    [ComponentInject] private EnemyAttack enemyAttack;

    public void Awake()
    {
        this.ComponentInject();
        gameObject.AddComponent<TooltipUIHandler>(); // regelt het tonen vd juiste text + gedrag -> via ITooltipUIText
    }

    public string GetHeaderText() => GetDisplayName(gameObject.name);
    public string GetContentText()
    {
        var content = new List<string>();
        content.Add("Current Health: " + enemyHealth.CurrentHitPoints);
        content.Add("Attack damage: " + 1);

        return string.Join("\n", content);
    }

    private string GetDisplayName(string name)
    {
        return name.Replace("Prefab", "").Replace("(Clone)", "").Trim();
    }
}
