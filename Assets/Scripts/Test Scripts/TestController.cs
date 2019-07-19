using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private Parent activePower;
    private bool isActive = false;
    bool child1;

    private void Start()
    {
        activePower = new Child1();
        child1 = true;
    }

    private void Update()
    {
        if(activePower)
        {
            if(Input.GetMouseButtonDown(0))
            {
                activePower.DoSomething();
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(GetComponent<Parent>());
            if(child1)
            {
                activePower = new Child2();
            }
            else
            {
                activePower = new Child1();
            }
            isActive = true;
            child1 = !child1;
        }

        if(Input.GetMouseButtonDown(1))
        {
            Destroy(GetComponent<Parent>());
            isActive = false;
        }
    }
}
