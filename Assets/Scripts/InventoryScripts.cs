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

    [SerializeField]
    private BagButton[] bagButtons;

    [SerializeField]
    private Item[] items;

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialized(16);
        bag.Use();
    }

    public void AddBag(Bag bag)
    {
        foreach(BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
            }
        }
    }
}
