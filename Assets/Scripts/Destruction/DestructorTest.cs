using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructorTest : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMeteor();
        }
        if(Input.GetMouseButtonDown(0))
        {
            SpawnMeteor2();
        }
    }

    private void SpawnMeteor()
    {
        GameObject meteor = Instantiate(Resources.Load("Meteor"), new Vector3(0f, 3f, -10f), Quaternion.identity) as GameObject;
        meteor.GetComponent<Meteor>().SetActive();
        meteor.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 5f), 15f);
    }

    private void SpawnMeteor2()
    {
        GameObject meteor = Instantiate(Resources.Load("Meteor"), new Vector3(10f, 3f, 0f), Quaternion.identity) as GameObject;
        meteor.GetComponent<Meteor>().SetActive();
        meteor.GetComponent<Rigidbody>().velocity = new Vector3(15f, Random.Range(0f, 5f), Random.Range(-1f, 1f));
    }
}
