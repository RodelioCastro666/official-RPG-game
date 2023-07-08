using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour,  IDragHandler, IDropHandler,IEndDragHandler
{
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private Image icon;

    private Armor equippedArmor;

    

    public void EquipArmor(Armor armor)
    {
        armor.Remove();

        if (equippedArmor != null)
        {
            if (equippedArmor != armor)
            {
                armor.MySlot.AddItem(equippedArmor);
            }
           
            UiManager.MyInstance.RefreshToolTip(equippedArmor);
        }

        
        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        icon.color = Color.white;
        this.equippedArmor = armor;
        HandScript.MyInstance.Drop();
       

        if (HandScript.MyInstance.MyMoveable == (armor as IMovable))
        {
            HandScript.MyInstance.Drop();
        }
    }

    

    public void DequipArmor()
    {
        icon.color = Color.white;
        icon.enabled = false;
        equippedArmor = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (equippedArmor != null)
        {
            UiManager.MyInstance.ShowToolTip(equippedArmor);
        }


        if (HandScript.MyInstance.MyMoveable == null && equippedArmor != null)
        {
                HandScript.MyInstance.TakeMoveable(equippedArmor);
                CharacterPanel.MyInstance.MySelectedButton = this;
                icon.color = Color.grey;
                Debug.Log(" char drag ");
        }
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        UiManager.MyInstance.HideToolTip();

        if (eventData.button == PointerEventData.InputButton.Left )
        {
            if (HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor tmp = (Armor)HandScript.MyInstance.MyMoveable;

                if (tmp.MyArmorType == armorType)
                {
                    EquipArmor(tmp);
                    Debug.Log("char drop");
                    
                }
            }
           
        }
    }

    private bool PutItemBack()
    {
        if (CharacterPanel.MyInstance.MySelectedButton == this)
        {
            CharacterPanel.MyInstance.MySelectedButton.icon.color = Color.white;
            return true;
        }
        return false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PutItemBack();
        HandScript.MyInstance.Drop();
    }
}
