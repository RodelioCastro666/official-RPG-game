using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScripts : MonoBehaviour
{

    public static InventoryScripts instance;

    public static InventoryScripts MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScripts>();
            }

            return instance;
        }
    }

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton[] bagButtons;

    [SerializeField]
    private Item[] items;

    private SlotScript fromSlot;

    public bool CanAddBag
    {
        get { return bags.Count < 3; }
    }

    public SlotScript FromSlot
    {   get
        {
            return fromSlot;
        }
            
        set 
        {
            fromSlot = value;
            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }




    }

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialized(16);
        bag.Use();

      

       

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialized(16);
            bag.Use();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialized(16);
            AddItem(bag);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            HealthPotion potion = (HealthPotion)Instantiate(items[1]);
            AddItem(potion);
        }
    }

    public void AddBag(Bag bag)
    {
        foreach(BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bags.Add(bag);
                break;
            }
        }
    }

    public void OpenClose()
    {
        bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);

        foreach(Bag bag in bags)
        {
            if (bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }

  

    public void AddItem(Item item)
    {
        if (item.MyStackSize  > 0)
        {
            if (PlaceInStack(item))
            {
                return;
            }
        }

        PlaceInEmpty(item);
    }

    private void PlaceInEmpty(Item item)
    {
        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                return;
            }
        }
    }

    private bool PlaceInStack(Item item)
    {
        foreach(Bag bag in bags)
        {
            foreach(SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
