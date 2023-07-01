using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootWindow : MonoBehaviour
{
    [SerializeField]
    private LootButton[] lootButton;

    [SerializeField]
    private Item[] items;

    // Start is called before the first frame update
    void Start()
    {
        AddLoot();
    }

   private void AddLoot()
    {
        lootButton[0].MyIcon.sprite = items[0].MyIcon;

        lootButton[0].gameObject.SetActive(true);

        string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[items[0].MyQuality], items[0].MyTitle);

        lootButton[0].MyTitle.text = title;
    }
}
