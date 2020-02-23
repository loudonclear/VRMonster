using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public void SetActive()
    {
        GetComponent<Rigidbody>().useGravity = true;
        Invoke("Despawn", 10f);
    }

    public void SetUnactive()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void Despawn()
    {
        Destroy(this.gameObject);
    }
}
