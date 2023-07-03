using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LootButton : MonoBehaviour,   IPointerDownHandler,IPointerUpHandler,IPointerClickHandler
{

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text title;

    private LootWindow lootWindow;

    public Item MyLoot { get; set; }

    public Image MyIcon { get => icon;  }
    public Text MyTitle { get => title;  }

    private void Awake()
    {
        lootWindow = GetComponentInParent<LootWindow>(); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (InventoryScripts.MyInstance.AddItem(MyLoot))
        {
            gameObject.SetActive(false);
            lootWindow.TakeLoot(MyLoot);
            UiManager.MyInstance.HideToolTip();
        }
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UiManager.MyInstance.ShowToolTip(MyLoot);
        Debug.Log("down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UiManager.MyInstance.HideToolTip();
        Debug.Log("Up");
    }
}
