using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void ItemCountChanged(Item item);

public class InventoryScripts : MonoBehaviour
{
    public event ItemCountChanged itemCountChanged;

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
    { get
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
            bag.Initialized(12);
            AddItem(bag);
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
        if (Input.GetKeyDown(KeyCode.H))
        {
           
            AddItem((Armor)Instantiate(items[2]));
            AddItem((Armor)Instantiate(items[3]));
            AddItem((Armor)Instantiate(items[4]));
            AddItem((Armor)Instantiate(items[5]));
            AddItem((Armor)Instantiate(items[6]));
            AddItem((Armor)Instantiate(items[7]));
            AddItem((Armor)Instantiate(items[8]));
            AddItem((Armor)Instantiate(items[9]));
            AddItem((Armor)Instantiate(items[10]));
            AddItem((Armor)Instantiate(items[11]));
            AddItem((Armor)Instantiate(items[12]));


        }
    }

    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bags.Add(bag);
                bag.MyBagButton = bagButton;
                break;
            }
        }
    }

    public void OpenClose()
    {
        bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);

        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }

    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }

    public bool AddItem(Item item)
    {
        if (item.MyStackSize  > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }

       return PlaceInEmpty(item);
    }

    private bool PlaceInEmpty(Item item)
    {
        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }
        return false;
    }

    private bool PlaceInStack(Item item)
    {
        foreach(Bag bag in bags)
        {
            foreach(SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }

        return false;
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag  in bags)
            {
                count += bag.MyBagScript.MyEmptySlotCount; 
            }
            return count;
        }
    }

    public void RemoveBag(Bag bag)
    {
        bags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }

    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in bags)
            {
                count += bag.MyBagScript.MySlots.Count;
            }

            return count;
        }
    }

    //public void Swapbags(Bag oldBag, Bag newBag)
    //{
    //    int newSlotCount = (MyTotalSlotCount - oldBag.Slots) + newBag.Slots;

    //    if (newSlotCount - MyFullSlotCount >= 0)
    //    {
    //        List<Item> bagItems = oldBag.MyBagScript.GetItems();

    //        RemoveBag(oldBag);

    //        newBag.MyBagButton = oldBag.MyBagButton;


    //        newBag.Use();

    //        foreach (Item item in bagItems)
    //        {
    //            if (item != newBag)
    //            {
    //                AddItem(item);
    //            }
    //        }

    //        AddItem(oldBag);

    //        HandScript.MyInstance.Drop();

    //        MyInstance.fromSlot = null;
    //    }

    //}

    //public void  AddBag(Bag bag, BagButton bagButton)
    //{
    //    bags.Add(bag);
    //    bagButton.MyBag = bag;
    //}


    public Stack<IUsable> GetUsables(IUsable type) 
    {
        Stack<IUsable> useables = new Stack<IUsable>();

        foreach (Bag bag in bags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach (Item item in slot.MyItems)
                    {
                        useables.Push(item as IUsable);
                    }
                }
            }
        }

        return useables;
    }

    public void OnItemCountChanged(Item item)
    {
        if (itemCountChanged != null)
        {
            itemCountChanged.Invoke(item);
        }
    }
}
