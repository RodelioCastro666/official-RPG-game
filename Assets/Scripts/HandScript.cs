using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;

    public static HandScript instance;

    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }

            return instance;
        }
    }

    public IMovable MyMoveable { get; set; }

    private Image icon;

    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        icon.transform.position = Input.mousePosition+offset;
    }

    public void TakeMoveable(IMovable moveable)
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }
}
