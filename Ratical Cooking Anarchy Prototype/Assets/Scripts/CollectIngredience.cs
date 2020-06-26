using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectIngredience : MonoBehaviour
{
    public GameObject ingredience;
    public float runspeed;
    float initXscale;
    BoxCollider2D boxCollider;
    public float ingredienceCount = 0;

    public Text ingredientText;

    public StateManager sm;
    public GameObject done;


    // Start is called before the first frame update
    void Start()
    {
        initXscale = transform.localScale.x;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ingredienceCount >= 3) return;
        if(Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(initXscale,  transform.localScale.y, transform.localScale.z);
            float dist = runspeed * Time.deltaTime;
            ingredience.transform.Translate(-dist, 0, 0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-initXscale, transform.localScale.y, transform.localScale.z);
            float dist = runspeed * Time.deltaTime;
            ingredience.transform.Translate(dist, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);
            foreach(Collider2D col in overlap)
            {
                if (col.gameObject.tag == "validingredient")
                {
                    Debug.Log("found valid ingredient");
                    ingredienceCount++;
                    Destroy(col.gameObject);
                    ingredientText.text = ingredienceCount + "/3 ingredients";
                    if(ingredienceCount == 3)
                    {
                        sm.SetDone();
                        done.SetActive(true);
                    }
                }
            }
        }
    }
}
