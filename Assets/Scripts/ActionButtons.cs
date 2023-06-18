using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButtons : MonoBehaviour,  IPointerClickHandler
{
    public IUsable MyUseable { get; set; }
    
    public Button MyButton { get; private set; }

   

    [SerializeField]
    private Image icon;

    public Image MyIcon { get => icon; set => icon = value; }

    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (MyUseable != null)
        {
            MyUseable.Use();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUsable)
            {
                SetUseable(HandScript.MyInstance.MyMoveable as IUsable);
            }
        }
    }

    public void SetUseable(IUsable useable)
    {

        this.MyUseable = useable;
        UpodateVisual();
    }

    public void UpodateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;
    }

}
