using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootWindow : MonoBehaviour
{
    [SerializeField]
    private LootButton[] lootButtons;

    private List<List<Item>> pages = new List<List<Item>>();

    private int pageIndex = 0;

    [SerializeField]
    private Text pageNumber;

    [SerializeField]
    private Item[] items;

    [SerializeField]
    private GameObject nxtBtn, previousBtn;

    // Start is called before the first frame update
    void Start()
    {
        List<Item> tmp = new List<Item>();
        for (int i = 0; i < items.Length; i++)
        {
            tmp.Add(items[i]);
        }

        CreatePages(tmp);
    }

   private void AddLoot()
    {
        if (pages.Count > 0)
        {
            pageNumber.text = pageIndex + 1 + "/" + pages.Count;

            previousBtn.SetActive(pageIndex > 0);
            nxtBtn.SetActive(pages.Count > 1 && pageIndex < pages.Count - 1);


            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex][i] != null)
                {
                    lootButtons[i].MyIcon.sprite = pages[pageIndex][i].MyIcon;

                    lootButtons[i].MyLoot = pages[pageIndex][i];

                    lootButtons[i].gameObject.SetActive(true);

                    string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[pages[pageIndex][i].MyQuality], pages[pageIndex][i].MyTitle);

                    lootButtons[i].MyTitle.text = title;
                }
               
            }
        }

       

       
    }

    public void CreatePages(List<Item> items)
    {
        List<Item> page = new List<Item>();

        for (int i = 0; i < items.Count; i++)
        {
            page.Add(items[i]);

            if (page.Count == 5 || i == items.Count -1)
            {
                pages.Add(page);
                page = new List<Item>();
            }
        }

        AddLoot();
    }

    public void ClearButtons()
    {
        foreach (LootButton btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (pageIndex < pages.Count -1)
        {
            pageIndex++;
            ClearButtons();
            AddLoot();
        }
    }
    public void PreviuosPage()
    {
        if (pageIndex > 0)
        {
            pageIndex--;
            ClearButtons();
            AddLoot();
        }
    }

    public void TakeLoot(Item loot)
    {
        pages[pageIndex].Remove(loot);

        if (pages[pageIndex].Count == 0)
        {
            pages.Remove(pages[pageIndex]);

            if (pageIndex == pages.Count && pageIndex > 0)
            {
                pageIndex--;
            }

            AddLoot();
        }
    }
}
