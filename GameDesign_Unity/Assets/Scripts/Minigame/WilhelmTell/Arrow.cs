using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float speedInMPerSecond = 1f;
    private Vector3 velocity;

    private bool fly = false;

    public bool Fly
    {
        set
        {
            if (value)
            {
                transform.SetParent(null);
            }
            Debug.Log("fly1=" + fly);
            fly = value;
        }
        get => fly;
    }
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(-speedInMPerSecond * 1000f / 20f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            Debug.Log("update fly=" + fly + " with velocity=" + velocity);
            transform.Translate(velocity * Time.deltaTime);
            if (transform.position.z > 0)
            {
                Destroy(this);
            }
        }
    }
}
