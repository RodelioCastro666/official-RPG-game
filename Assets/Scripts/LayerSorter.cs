using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{
    private SpriteRenderer parentRenderer;

    private List<Obstacle> obstacle = new List<Obstacle>();

    void Start()
    {
        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            Obstacle o = collision.GetComponent<Obstacle>();
            o.FadeOut();

            if (obstacle.Count == 0 || o.MySpriteRenderer.sortingOrder -1 < parentRenderer.sortingOrder)
            {
                parentRenderer.sortingOrder = o.MySpriteRenderer.sortingOrder - 1;
            }

            obstacle.Add(o);
          
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.tag == "Obstacle")
        {
            Obstacle o = collision.GetComponent<Obstacle>();
            o.FadeIn();

            obstacle.Remove(o);

            if (obstacle.Count == 0)
            {
                parentRenderer.sortingOrder = 200;
            }

            else
            {
                obstacle.Sort();
                parentRenderer.sortingOrder = obstacle[0].MySpriteRenderer.sortingOrder - 1;
            }
           
        }

    }
}
