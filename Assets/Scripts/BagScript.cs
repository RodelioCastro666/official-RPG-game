using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagScript : MonoBehaviour
{


    [SerializeField]
    private GameObject slotPrefab;

    private CanvasGroup canvasGroup;

    private List<SlotScript> slots = new List<SlotScript>();

    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
            slot.MyBag = this;
            MySlots.Add(slot);
        }
    }

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1; 

        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in MySlots)
            {
                if (slot.IsEmpty)
                {
                    count++;
                }
            }
            return count;
        }
    }

    public List<Item> GetItems()
    {
        List<Item> items = new List<Item>();

        foreach (SlotScript slot in slots)
        {
            if (!slot.IsEmpty)
            {
                foreach(Item item in slot.MyItems)
                {
                    items.Add(item);
                }
            }
        }

        return items;
    }

    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    public List<SlotScript> MySlots { get => slots;  }

    public bool AddItem(Item item)
    {
        foreach(SlotScript slot in MySlots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);

                return true;
            }
        }
        return false;
    }

 
}
