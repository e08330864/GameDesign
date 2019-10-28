using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfluenceItem : MonoBehaviour
{
    private bool _positive = false;  // true = contribution is positive, false = contribultion is negativ
    private Color _colorPositiv = new Color(200, 240, 65, 255);
    private Color _colorNegativ = new Color(240, 130, 70, 255);
    private Image _image = null;

    // Start is called before the first frame update
    void Start()
    {
        if ((_image == GetComponent<Image>()) == null)
        {
            Debug.LogError("image is NULL in InfluenceItem");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetContribution(bool positive)
    {
        _positive = positive;
        _image.color = _positive ? _colorPositiv : _colorNegativ;
    }
}
