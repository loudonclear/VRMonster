using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCube : MonoBehaviour
{

    [SerializeField]
    private bool firstPass = false;
    [SerializeField]
    private int firstXDim = 3;
    [SerializeField]
    private int firstYDim = 5;
    [SerializeField]
    private int firstZDim = 3;
    [SerializeField]
    private float minSize = 1f;

    [SerializeField]
    private float RequiredForce = 10f;
    [SerializeField]
    private float BreakMultiplier = 1f;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Floor" && !GetComponent<Rigidbody>().isKinematic)
        {
            Destroy(this.gameObject);
        }
        if(col.gameObject.tag != "BuildingComponent")
        {
            float force = col.relativeVelocity.magnitude * col.rigidbody.mass;
            if(force > RequiredForce)
            {
                float sphereRadius = col.relativeVelocity.magnitude / RequiredForce * BreakMultiplier;
                Collider[] pieces = Physics.OverlapSphere(col.contacts[0].point, sphereRadius, LayerMask.GetMask("BuildingComponent"));

                foreach(Collider piece in pieces)
                {
                    piece.GetComponent<DestructibleCube>().Break();
                }
            }
        }
        
    }

    public void Break()
    {
        GameObject Cube = Resources.Load("Cube-v2") as GameObject;
        float xOffset, yOffset, zOffset;
        float xEnd, yEnd, zEnd;
        float scale;
        if(firstPass)
        {
            scale = 1f;
            xOffset = 1f;
            yOffset = 2f;
            zOffset = 1f;
            xEnd = firstXDim;
            yEnd = firstYDim;
            zEnd = firstZDim;
        }
        else
        {
            scale = transform.localScale.x / 2f;
            xOffset = .5f;
            yOffset = .5f;
            zOffset = .5f;
            xEnd = 2;
            yEnd = 2;
            zEnd = 2;
        }

        if(scale >= minSize)
        {
            for(int i = 0; i < xEnd; i++)
            {
                for(int j = 0; j < yEnd; j++)
                {
                    for(int k = 0; k < zEnd; k++)
                    {
                        Vector3 pos = transform.position;
                        pos.x += ((i - xOffset) * scale);
                        pos.y += ((j - yOffset) * scale);
                        pos.z += ((k - zOffset) * scale);
                        GameObject obj = Instantiate(Cube, pos, Quaternion.identity);
                        obj.transform.localScale = new Vector3(scale, scale, scale);
                    }
                }
            }
            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
        
    }
}
