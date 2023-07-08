using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private CharButton helmet, chest, necklace, ring, boots, belt,sword,orb;

    public CharButton MySelectedButton { get; set; }

    private static CharacterPanel instance;

    public static CharacterPanel MyInstance 
    { 
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CharacterPanel>();
            }

            return instance;
        }
    }

    public void OpenClose()
    {
        if (canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }

    public void EquipArmor(Armor armor)
    {
        switch (armor.MyArmorType)
        {
            case ArmorType.Helmet:
                helmet.EquipArmor(armor);
                break;
            case ArmorType.Shoulders:
                break;
            case ArmorType.Chest:
                chest.EquipArmor(armor);
                break;
            case ArmorType.Gloves:
                break;
            case ArmorType.Boots:
                boots.EquipArmor(armor);
                break;
            case ArmorType.Sword:
                sword.EquipArmor(armor);
                break;
            case ArmorType.Orb:
                orb.EquipArmor(armor);
                break;
            case ArmorType.Necklace:
                necklace.EquipArmor(armor);
                break;
            case ArmorType.Ring:
                ring.EquipArmor(armor);
                break;
            case ArmorType.Belt:
                belt.EquipArmor(armor);
                break;
        }


    }
}
