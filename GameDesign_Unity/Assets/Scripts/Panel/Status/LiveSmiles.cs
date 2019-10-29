using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveSmiles : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> smiley_prefab = new List<GameObject>();
    private List<GameObject> smilies = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (GameObject sp in smiley_prefab)
        {
            GameObject smiley =  Instantiate(sp, transform);
            smilies.Add(smiley);
            smiley.SetActive(false);
        }
    }

    public void SetValue(int value)
    {
        value = value < 0 ? 0 : value > smilies.Count ? smilies.Count : value;
        for (int i = 0; i < smilies.Count; i++)
        {
            if (value >= i + 1)
            {
                smilies[i].SetActive(true);
            }
            else
            {
                smilies[i].SetActive(false);
            }
        }
    }
}
