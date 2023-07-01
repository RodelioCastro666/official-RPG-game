using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 1)]
public class HealthPotion :Item, IUsable
{
    [SerializeField]
    private int health;

    public void Use()
    {
        if (Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance. MyHealth.MyMaxValue)
        {
            Remove();

            Player.MyInstance.MyHealth.MyCurrentValue += health;
        }

        
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n Use: Restores {0} health", health);
    }
}
