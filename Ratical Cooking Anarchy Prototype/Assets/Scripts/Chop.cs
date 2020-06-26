using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chop : MonoBehaviour
{
    public float speed = 3;
    public float acceleration = 1.5f;
    public bool dir = true;
    public float bound = 3;

    public int ingredindex = 0;
    public float ingredpercent = 0;

    public Text percentxt;
    public Text indextxt;

    public GameObject[] chopranges;

    BoxCollider2D boxCollider;

    public StateManager sm;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ingredindex > 2) return;
        float xpos = transform.position.x;
        if (dir)
        {
            xpos += (speed + ingredindex * acceleration) * Time.deltaTime;
            if (xpos > bound) xpos = bound;
        }
        else
        {
            xpos -= (speed + ingredindex * acceleration) * Time.deltaTime;
            if (xpos < -bound) xpos = -bound;
        }
        transform.position = new Vector3(xpos, transform.position.y, transform.position.z);
        if (Mathf.Abs(xpos) == bound) dir = !dir;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float points = 0;

            Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);
            foreach (Collider2D col in overlap)
            {
                if (col.gameObject.name == "Good")
                {
                    points = 50;
                    break;
                }
            }
            if (points == 0)
            {
                foreach (Collider2D col in overlap)
                {
                    if (col.gameObject.name == "OK")
                    {
                        points = 25;
                        break;
                    }
                }
            }
            ingredpercent += points;
            percentxt.text = ingredpercent + "%";
            if (ingredpercent >= 100)
            {
                if(ingredindex ==2) sm.SetDone();
                nextIngredient();
            }
        }
    }

    void nextIngredient()
    {
        if (ingredindex > 2) return;
        if (ingredindex == 2)
        {
            sm.SetDone();
            ingredindex++;
            return;
        }
        chopranges[ingredindex].SetActive(false);

        ingredindex++;
        indextxt.text = (ingredindex + 1) + "/3";
        chopranges[ingredindex].SetActive(true);

        ingredpercent = 0;
        percentxt.text = ingredpercent + "%";
    }
}
