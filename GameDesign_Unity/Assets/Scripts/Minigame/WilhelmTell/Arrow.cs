using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float speedInMPerSecond = 1f;
    private Vector3 velocity;
    private BoxCollider boxCollider = null;
    private bool hit = false;

    private bool fly = false;

    public bool Fly
    {
        set
        {
            if (value)
            {
                transform.SetParent(null);
                boxCollider.isTrigger = true;
            }
            //Debug.Log("fly1=" + fly);
            fly = value;
        }
        get => fly;
    }
    // Start is called before the first frame update
    void Start()
    {
        if ((boxCollider = transform.GetComponent<BoxCollider>()) == null)
        {
            Debug.LogError("boxCollider is NULL in Arrow");
        }
        velocity = new Vector3(-speedInMPerSecond * 1000f / 20f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            //Debug.Log("update fly=" + fly + " with velocity=" + velocity);
            transform.Translate(velocity * Time.deltaTime);
            if (transform.position.z > 0)
            {
                Destroy(this);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("collision object = " + collision.name + "  label=" + label);
        if (collision.name == "Ring3D(Clone)")
        {
            if (!hit)
            {
                hit = true;
                collision.gameObject.GetComponent<Ring3D>().hit();
                FindObjectOfType<WilhelmTell>().arrowShot();
                Destroy(this.gameObject);
            }
        }
    }
}
