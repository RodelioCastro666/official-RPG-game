using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName ="Items/Bag", order =1)]
public class Bag : Item, IUsable
{

    private int slots;

    [SerializeField]
    private GameObject bagPrefab;

    public BagScript MyBagScript { get; set; }

    public int Slot { get => slots;  }

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
            
        }

      
    }

   
}
