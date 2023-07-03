using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;


    private Npc currentTarget;
    // Update is called once per frame
    void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() )
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity,512);

            if (hit.collider != null)
            {
                if(currentTarget != null)
                {
                    currentTarget.DeSelect();
                }

                currentTarget = hit.collider.GetComponent<Npc>();

                player.MyTarget = currentTarget.Select();

                UiManager.MyInstance.ShowTargetFrame(currentTarget);
            }
            else
            {
                UiManager.MyInstance.HideTargetFrame();

                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }
                currentTarget = null;
                player.MyTarget = null;
            }
        }
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if (hit.collider != null  && hit.collider.tag == "Enemy" )
            {
                hit.collider.GetComponent<Npc>().Interact();
            }
        }

    }
}
