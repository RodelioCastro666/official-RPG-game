using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UiManager : MonoBehaviour
{

    public static UiManager instance;

    public static UiManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiManager>();
            }

            return instance;
        }
    }

   
    

    private Stat healthStat;

    private GameObject[] keybindButtons;

    [SerializeField]
    private Image portraitFrame;


    [SerializeField]
    private CanvasGroup keybindsMenu;

    [SerializeField]
    private CanvasGroup spellBook;



    [SerializeField]
    private ActionButtons[] actionButtons;

   

    [SerializeField]
    private GameObject targetFrame;

    [SerializeField]
    private GameObject toolTip;

    [SerializeField]
    private CharacterPanel charPanel;

    private Text toolTipText;

   

    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        toolTipText = toolTip.GetComponentInChildren<Text>();
    }

    void Start()
    {
       

       

        healthStat = targetFrame.GetComponentInChildren<Stat>();

       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose(keybindsMenu);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenClose(spellBook);
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            InventoryScripts.MyInstance.OpenClose();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            charPanel.OpenClose();
        }

    }

   

    public void ShowTargetFrame(Npc target)
    {
        targetFrame.SetActive(true);
        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);

        portraitFrame.sprite= target.MyPortrait;

        target.healthChanged += new HealthChanged(UpdateTargetFrame);

        target.characterRemoved += new CharacterRemoved(HideTargetFrame);
    }

    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    public void UpdateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }

    

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
        
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    

    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void UpdateStackSize(IClickable clickable)
    {

        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;

        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }

        if (clickable.MyCount == 0)
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
        }
    }

    public void ClearStackCount(IClickable clickable)
    {
        clickable.MyStackText.color = new Color(0, 0, 0, 0);
        clickable.MyIcon.color = Color.white;
    }

    public void ShowToolTip(IDescribable description)
    {
        
        toolTip.SetActive(true);
        toolTipText.text= description.GetDescription();
    }

    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }

    public void RefreshToolTip(IDescribable description)
    {
        toolTipText.text = description.GetDescription();
    }
}
