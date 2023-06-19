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
    private Item[] items;

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialized(16);
        bag.Use();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
