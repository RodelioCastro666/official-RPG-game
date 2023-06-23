using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable
{
    private Stack<Item> items = new Stack<Item>();

    [SerializeField]
    private Image icon;

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    public Item MyItem
    {
        get
        {
            if(!IsEmpty) 
            {
                return items.Peek();
            }
            return null;
        }
    }

    public Image MyIcon
    {
        get 
        {
            return icon;
        } 
        set 
        {
            icon = value;
        }
        
    }

    public int MyCount
    {
        get { return items.Count; }
    }

    public bool AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UseItem();
        }
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            items.Pop();
            UiManager.MyInstance.UpdateStackSize(this);
        }
    }

    public void UseItem()
    {
        if (MyItem is IUsable)
        {
            (MyItem as IUsable).Use();
        }
    }
}
