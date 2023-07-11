using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellButton : MonoBehaviour, IPointerClickHandler,IDragHandler, IEndDragHandler

{
    [SerializeField]
    private string spellName;

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            HandScript.MyInstance.TakeMoveable(SpellBook.MyInstance.GetSpell(spellName));
        }
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
        HandScript.MyInstance.Drop();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (eventData.button == PointerEventData.InputButton.Left)
        //{
        //    HandScript.MyInstance.TakeMoveable(SpellBook.MyInstance.GetSpell(spellName));
        //}
    }
}
