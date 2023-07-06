using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArmorType { Helmet, Shoulders, Chest, Gloves, Boots, Sword, Orb, Necklace, Ring, Belt}

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]

public class Armor : Item
{
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private int intellect;

    [SerializeField]
    private int Strength;

    [SerializeField]
    private int stamina;

    internal ArmorType MyArmorType { get => armorType; set => armorType = value; }

    public override string GetDescription()
    {
        string stats = string.Empty;

        if (intellect > 0)
        {
            stats += string.Format("\n +{0} intellect", intellect);
        }
        if (Strength > 0)
        {
            stats += string.Format("\n +{0} strength", intellect);
        }
        if (stamina> 0)
        {
            stats += string.Format("\n +{0} stamina", intellect);
        }
       
        return base.GetDescription() + stats;
    }

    public void Equip()
    {
        CharacterPanel.MyInstance.EquipArmor(this);
    }
}
