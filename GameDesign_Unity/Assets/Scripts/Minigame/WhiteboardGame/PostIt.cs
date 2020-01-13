using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostIt : MonoBehaviour
{
    public PostItTarget currentTarget;
    public PostItSpawner spawner;

    private void Update()
    {
        this.transform.position = Input.mousePosition;
    }

    public void Release()
    {
        if(this.currentTarget != null && this.spawner == currentTarget.spawner)
        {
            this.enabled = false;
            this.currentTarget.hasPostIt = true;
            this.currentTarget.GetComponent<Collider2D>().enabled = false;
            spawner.updateCorrect();
        }
        else
        {
            GameObject.DestroyImmediate(this.gameObject);
        }
    }
}
