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

    [SerializeField]
    private Quality quality;

    public Sprite MyIcon { get => icon;  }

    public int MyStackSize { get => stackSize;  }

    public SlotScript MySlot { get => slot; set => slot = value; }

    public Quality MyQuality { get => quality; }

    public string MyTitle { get => title;  }

    public virtual string GetDescription()
    {
       
        return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[MyQuality],MyTitle);
    }

    public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
