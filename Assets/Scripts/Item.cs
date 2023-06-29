using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject, IMovable ,IDescribable
{

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    private string title;

    private SlotScript slot;

    public Sprite MyIcon { get => icon;  }

    public int MyStackSize { get => stackSize;  }

    public SlotScript MySlot { get => slot; set => slot = value; }

    public string GetDescription()
    {
        return title;
    }

    public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
