using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        Invoke("Despawn", 10f);
    }

    public void SetActive()
    {
        rigid.useGravity = true;
    }

    private void Despawn()
    {
        Destroy(this.gameObject);
    }
}
