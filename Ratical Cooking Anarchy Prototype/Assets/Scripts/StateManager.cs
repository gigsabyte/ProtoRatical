using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    int gamesDone = 0;
    int state = 0;

    public GameObject collect;
    public GameObject light;
    public GameObject stir;
    public GameObject chop;

    public GameObject start;
    public GameObject done;

    bool cancontinue = false;

    // Start is called before the first frame update
    void Start()
    {
        collect.SetActive(false);
        light.SetActive(false);
        stir.SetActive(false);
        chop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        collect.SetActive(true);
        light.SetActive(true);
        start.SetActive(false);
    }

    public void SetDone()
    {
        gamesDone++;
        if(state == 0 && gamesDone == 2)
        {
            collect.SetActive(false);
            light.SetActive(false);
            gamesDone = 0;
            state = 1;
            StartCoroutine(Wait());
        }
        else if(state == 1 && gamesDone >= 1)
        {
            stir.SetActive(false);
            chop.SetActive(false);
            done.SetActive(true);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        stir.SetActive(true);
        chop.SetActive(true);
    }
}
