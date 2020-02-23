using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructBuilding : MonoBehaviour
{

    [SerializeField]
    private GameObject Cube;

    [SerializeField]
    private int xDim;
    [SerializeField]
    private int yDim;
    [SerializeField]
    private int zDim;

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < xDim; x++)
        {
            for(int y = 0; y < yDim; y++)
            {
                for(int z = 0; z < zDim; z++)
                {
                    GameObject obj = Instantiate(Cube, transform.position + new Vector3(x, y, z), Quaternion.identity);
                    obj.transform.parent = transform;
                }
            }
        }
    }
}
