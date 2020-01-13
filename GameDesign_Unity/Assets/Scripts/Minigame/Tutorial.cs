using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject yesMouse;
    public GameObject noMouse;

    private LevelController controller;

    private void Start()
    {
        controller = FindObjectOfType<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.enabled)
        {
            yesMouse.SetActive(true);
            noMouse.SetActive(true);
            this.enabled = false;
        }
        
    }
}
