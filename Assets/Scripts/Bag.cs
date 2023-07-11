using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName ="Items/Bag", order =1)]
public class Bag : Item, IUsable
{

    [SerializeField]
    private int slots;

    [SerializeField]
    private GameObject bagPrefab;

    public BagScript MyBagScript { get; set; }

    public BagButton MyBagButton { get; set; }

    public int Slots { get => slots;  }

    public void Initialized(int slots)
    {
        this.slots = slots;
    }

    public void Use()
    {
        if (InventoryScripts.MyInstance.CanAddBag)
        {
            Remove();
            MyBagScript = Instantiate(bagPrefab, InventoryScripts.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(slots);

            InventoryScripts.MyInstance.AddBag(this);

            //if (MyBagButton == null)
            //{
            //    InventoryScripts.MyInstance.AddBag(this);
            //}
            //else
            //{
            //    InventoryScripts.MyInstance.AddBag(this, MyBagButton);
            //}
        }

      
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n{0} slot bag", slots); 
    }


}
