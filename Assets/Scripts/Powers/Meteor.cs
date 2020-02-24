using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public void SetActive()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Invoke("Despawn", 10f);
    }

    public void SetUnactive()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Despawn()
    {
        Destroy(this.gameObject);
    }
}
