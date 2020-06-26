using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountFire : MonoBehaviour
{

    BoxCollider2D boxCollider;

    public float fireCount = 0;
    public Text countext;

    public StateManager sm;
    public GameObject done;

    bool donelighting = false;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (donelighting) return;
        Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);
        fireCount = overlap.Length - 1;
        countext.text = fireCount + "/6 flames";
        if(fireCount >= 6)
        {
            donelighting = true;
            done.SetActive(true);
            sm.SetDone();
        }
    }
}
