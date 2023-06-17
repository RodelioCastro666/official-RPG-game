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

    private void Awake()
    {
        
    }

    void Start()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");

        SetUseable(actionButtons[0], SpellBook.MyInstance.GetSpell("lightning"));
        SetUseable(actionButtons[1], SpellBook.MyInstance.GetSpell("Frost"));
        SetUseable(actionButtons[2], SpellBook.MyInstance.GetSpell("Fire"));

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

    public void SetUseable(ActionButtons btn, IUsable useable)
    {
        
        btn.MyIcon.sprite = useable.MyIcon;
        btn.MyIcon.color = Color.white;
        btn.MyUseable = useable;
    }

    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }
}
