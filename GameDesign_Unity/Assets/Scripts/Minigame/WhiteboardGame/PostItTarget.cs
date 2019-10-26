using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostItTarget : MonoBehaviour
{
    [HideInInspector]
    public PostItSpawner spawner;

    [HideInInspector]
    public bool hasPostIt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PostIt postIt = collision.GetComponent<PostIt>();
        if(postIt != null)
        {
            postIt.currentTarget = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PostIt postIt = collision.GetComponent<PostIt>();
        if (postIt != null)
        {
            postIt.currentTarget = null;
        }
    }


}
