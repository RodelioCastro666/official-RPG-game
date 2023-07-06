using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandScript : MonoBehaviour 
{
    [SerializeField]
    private Vector3 offset;

    public static HandScript instance;

    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }

            return instance;
        }
    }

    public IMovable MyMoveable { get; set; }

    private Image icon;

    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        icon.transform.position = Input.mousePosition + offset;

        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
        {
            DeleteItem();
        }

       
    }

    public void TakeMoveable(IMovable moveable)
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }

    public IMovable Put()
    {
        IMovable tmp = MyMoveable;
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        InventoryScripts.MyInstance.FromSlot = null;

    }

    public void DeleteItem()
    {
        

        if (MyMoveable is Item && InventoryScripts.MyInstance.FromSlot != null)
        {
            (MyMoveable as Item).MySlot.Clear();
            Debug.Log("delete");
            Drop();
        }
        else if (MyMoveable is Item)
        {
            (MyMoveable as Item).MySlot.Clear();
            Drop();
        }
       

       





        InventoryScripts.MyInstance.FromSlot = null;
    }

   
}