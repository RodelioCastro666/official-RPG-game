using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IDragHandler,IDropHandler,IEndDragHandler
{
    private ObservableStack<Item> items = new ObservableStack<Item>();

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text StackSize;

    public BagScript MyBag { get; set; }

    public bool IsEmpty
    {
        get
        {
            return MyItems.Count == 0;
        }
    }

    public Item MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return MyItems.Peek();
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

    public bool Isfull
    {
        get
        {
            if (IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }
            return true;
        }
    }

    public int MyCount
    {
        get { return MyItems.Count; }
    }

    public Text MyStackText => StackSize;

    public ObservableStack<Item> MyItems { get => items; }

    private void Awake()
    {
        MyItems.OnPop += new UpdateStackEvent(UpdateSlot);
        MyItems.OnPush += new UpdateStackEvent(UpdateSlot);
        MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    public bool AddItem(Item item)
    {
        MyItems.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
         
        if (eventData.button == PointerEventData.InputButton.Left && eventData.pointerPress != null && InventoryScripts.MyInstance.FromSlot == null)
        {
            UseItem();
            Debug.Log("click");
        }
       
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            MyItems.Pop();

        }
    }

    public void UseItem()
    {
        if (MyItem is IUsable)
        {
            (MyItem as IUsable).Use();
        }
    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStackSize)
        {
            MyItems.Push(item);
            item.MySlot = this;
            return true;
        }

        return false;
    }
    private void UpdateSlot()
    {
        UiManager.MyInstance.UpdateStackSize(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
      
       if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScripts.MyInstance.FromSlot == null && !IsEmpty)
            {
                HandScript.MyInstance.TakeMoveable(MyItem as IMovable);
                InventoryScripts.MyInstance.FromSlot = this;
                Debug.Log("drag");
            }

            else if (InventoryScripts.MyInstance.FromSlot == null && IsEmpty && (HandScript.MyInstance.MyMoveable is Bag))
            {

                
                Bag bag = (Bag)HandScript.MyInstance.MyMoveable;

                if (bag.MyBagScript != MyBag)
                {
                    AddItem(bag);
                }
                  

            }

        }
        

    }


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScripts.MyInstance.FromSlot == null && !IsEmpty)
            {
                HandScript.MyInstance.TakeMoveable(MyItem as IMovable);
                InventoryScripts.MyInstance.FromSlot = this;
                Debug.Log("drag");
            }
            else if (InventoryScripts.MyInstance.FromSlot == null && IsEmpty && (HandScript.MyInstance.MyMoveable is Bag))
            {
                Bag bag = (Bag)HandScript.MyInstance.MyMoveable;

                if (bag.MyBagScript != MyBag && MyBag && InventoryScripts.MyInstance.MyEmptySlotCount - bag.Slots > 0)
                {
                    AddItem(bag);
                    bag.MyBagButton.RemoveBag();
                    HandScript.MyInstance.Drop(); 
                }

               

            }
            else if (InventoryScripts.MyInstance.FromSlot != null)
            {
                if (PutItemBack() || Mergeitems(InventoryScripts.MyInstance.FromSlot) || Swapitems(InventoryScripts.MyInstance.FromSlot) || AddItems(InventoryScripts.MyInstance.FromSlot.MyItems))
                {
                    HandScript.MyInstance.Drop();
                    InventoryScripts.MyInstance.FromSlot = null;
                    Debug.Log("drop");
                }
            }
            

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PutItemBack();
        HandScript.MyInstance.Drop();
        InventoryScripts.MyInstance.FromSlot = null;
        Debug.Log("ENDdrag");

      
        
    }


    private bool PutItemBack()
    {
        if (InventoryScripts.MyInstance.FromSlot == this)
        {
            InventoryScripts.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }





    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;

            for (int i = 0; i < count; i++)
            {
                if (Isfull)
                {
                    return false;
                }
                AddItem(newItems.Pop());
            }

            return true;
        }

        return false;
    }

    private bool Swapitems(SlotScript from)
    {
        if (IsEmpty) 
        {
            return false;
        }
        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount+MyCount > MyItem.MyStackSize)
        {
            ObservableStack<Item> tmpfrom = new ObservableStack<Item>(from.MyItems);

            from.MyItems.Clear();
            from.AddItems(MyItems);
            MyItems.Clear();
            AddItems(tmpfrom);

            return true;
        }

        return false;
    }

    private bool Mergeitems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() == MyItem.GetType() && !Isfull)
        {
            int free = MyItem.MyStackSize - MyCount;

            for (int i = 0; i < free; i++)
            {
                AddItem(from.MyItems.Pop());
            }

            return true;
        }

        return false;
    }

    public void Clear()
    {
        if (MyItems.Count > 0)
        {
            MyItems.Clear();
        }
    }
}

    
  
