using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ActionButtons : MonoBehaviour,  IPointerClickHandler, IClickable,IDropHandler
{
    public IUsable MyUseable { get; set; }
    
    public Button MyButton { get; private set; }

    private Stack<IUsable> useables = new Stack<IUsable>();

    [SerializeField]
    private Text stackSize;

    private int count;

    [SerializeField]
    private Image icon;

    public Image MyIcon { get => icon; set => icon = value; }

    public int MyCount
    {
        get
        {
            return count;
        }
    }

    public Text MyStackText
    {
        get { return stackSize; }
    }

    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        InventoryScripts.MyInstance.itemCountChanged += new ItemCountChanged(UpdateItemCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {

            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            if(useables != null && useables.Count > 0)
            {
                useables.Peek().Use();
            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (eventData.button == PointerEventData.InputButton.Left)
        //{
        //    if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUsable)
        //    {
        //        SetUseable(HandScript.MyInstance.MyMoveable as IUsable);
        //    }
        //}
    }

    public void SetUseable(IUsable useable)
    {
        if (useable is Item)
        {
            useables = InventoryScripts.MyInstance.GetUsables(useable);
            count = useables.Count;
            InventoryScripts.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventoryScripts.MyInstance.FromSlot = null;
        }
        else
        {
            useables.Clear();
            this.MyUseable = useable;
        }

        count = useables.Count;
        UpodateVisual();
    }

    public void UpodateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;

        if(count > 1)
        {
            UiManager.MyInstance.UpdateStackSize(this);
        }
        else if(MyUseable is Spell)
        {
            UiManager.MyInstance.ClearStackCount(this);
        }

        
    }

    public void UpdateItemCount(Item item)
    {
        if(item is IUsable && useables.Count > 0)
        {
            if (useables.Peek().GetType() == item.GetType())
            {
                useables = InventoryScripts.MyInstance.GetUsables(item as IUsable);

                count = useables.Count;

                UiManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUsable)
            {
                SetUseable(HandScript.MyInstance.MyMoveable as IUsable);
            }
        }
    }
}
