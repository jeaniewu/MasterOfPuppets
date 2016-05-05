using UnityEngine;
using System.Collections;

public class receiveSignalJail : receiveSignal
{

    private Collider2D collider2D2;
    private Renderer renderer2;

    // Use this for initialization
    void Start()
    {
        collider2D2 = GetComponent<Collider2D>();
        renderer2 = GetComponent<Renderer>();
    }


    public override void activate()
    {

        if (collider2D2 != null)
        {
            collider2D2.enabled = false;
        }

        if (renderer2 != null)
        {
            renderer2.enabled = false;
        }
    }

    public override void deactivate()
    {
        if (collider2D2 != null)
        {
            collider2D2.enabled = true;
        }
        if (renderer2 != null)
        {
            renderer2.enabled = true;
        }
    }
}
