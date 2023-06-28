using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BagButton : MonoBehaviour,  IPointerClickHandler, IDragHandler

{

    private Bag bag;

    [SerializeField]
    private Sprite full, empty;

    public Bag MyBag 
    { 
        get 
        {
            return bag;
        }  
        set 
        {
            if (value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }
            bag = value;
        }
     }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            HandScript.MyInstance.TakeMoveable(MyBag);
        }
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
        if (bag != null)
        {
            bag.MyBagScript.OpenClose();
        }
    }

    public void RemoveBag()
    {
        InventoryScripts.MyInstance.RemoveBag(MyBag);
        MyBag.MyBagButton = null;


        foreach (Item item in MyBag.MyBagScript.GetItems())
        {
            InventoryScripts.MyInstance.AddItem(item);

        }
        MyBag = null;
    }
}
