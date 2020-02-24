using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBuilding : MonoBehaviour
{

    [SerializeField]
    private float RequiredForce = 10f;
    [SerializeField]
    private float BreakMultiplier = 1f;

    private bool collisionCooldown = true;
    private List<GameObject> collisionObjectCheck;

    // Start is called before the first frame update
    void Start()
    {
        collisionObjectCheck = new List<GameObject>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        collisionObjectCheck.Clear();
    }

    public void ComponentCollision(Collision col)
    {
 
        float force = col.relativeVelocity.magnitude * col.rigidbody.mass;
        if(force > RequiredForce && !collisionObjectCheck.Contains(col.gameObject))
        {
            collisionObjectCheck.Add(col.gameObject);

            float sphereRadius = col.relativeVelocity.magnitude / RequiredForce * BreakMultiplier;
            Collider[] pieces = Physics.OverlapSphere(col.contacts[0].point, sphereRadius, LayerMask.GetMask("BuildingComponent"));

            foreach(Collider piece in pieces)
            {
                
                piece.GetComponent<BuildingComponent>().Break();
            }
        }
        
    }
}
