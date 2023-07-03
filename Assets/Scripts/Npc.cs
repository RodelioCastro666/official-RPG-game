using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);

public delegate void CharacterRemoved();

public class Npc : Character
{
    public event HealthChanged healthChanged;

    public event CharacterRemoved characterRemoved;

    [SerializeField]
    private Sprite portrait;

    public Sprite MyPortrait { get => portrait;  }

    public virtual void DeSelect()
    {
        healthChanged -= new HealthChanged(UiManager.MyInstance.UpdateTargetFrame);
        characterRemoved -= new CharacterRemoved(UiManager.MyInstance.HideTargetFrame);
    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
    }

    public void OnCharacterRemoved()
    {
        if (characterRemoved != null)
        {
            characterRemoved();
        }

        Destroy(gameObject);
    }

    public virtual void Interact()
    {
        Debug.Log("abcdefghijklmnopqrstuvwxyz");
    }
}
