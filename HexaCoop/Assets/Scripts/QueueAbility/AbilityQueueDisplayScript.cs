using UnityEngine;
using UnityEngine.UI;

public class AbilityQueueDisplayScript : MonoBehaviour
{
    [SerializeField] private Image AbilityImage;
    [SerializeField] private Image BackgroundAbility;

    private int QueueItemId;
    private bool _isActivated;
    public bool IsActivated
    {
        get => _isActivated;
        set
        {
            _isActivated = value;
            BackgroundAbility.color = IsActivated ?
                new Color(68 / 255f, 178 / 255f, 74 / 255f) : // donker groen 
                new Color(154 / 255f, 184 / 255f, 231 / 255f); // grauw blauw
        }
    }

    private void Start()
    {
        IsActivated = false;
    }

    public void SetAbility(int id, AbilityType abilityType)
    {
        QueueItemId = id;
        AbilityImage.sprite = Rsc.SpriteMap.Get(abilityType.ToString());
    }

    public void RemoveAbility() => ActionEvents.RemoveQueueItem?.Invoke(AbilitiesQueueScript.instance.Get(QueueItemId));
}
