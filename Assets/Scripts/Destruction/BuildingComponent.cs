using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingComponent : MonoBehaviour
{

    private GameObject Parent;

    private bool broken = false;

    public void SetParent(GameObject obj)
    {
        Parent = obj;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!broken && collision.gameObject.tag != "BuildingComponent" && collision.gameObject.tag != "Floor")
        {
            Parent.GetComponent<DestructibleBuilding>().ComponentCollision(collision);
        }
        else if(broken && collision.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
        
    }

    public void DeathTimer(float seconds)
    {
        Invoke("Death", seconds);
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }

    public void Break()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        broken = true;
    }
}
