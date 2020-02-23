using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructBuilding : MonoBehaviour
{

    [SerializeField]
    private GameObject Cube;

    [SerializeField]
    private int xDim = 3;
    [SerializeField]
    private int yDim = 5;
    [SerializeField]
    private int zDim = 3;
    [SerializeField]
    private float cubeScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < xDim; x++)
        {
            for(int y = 0; y < yDim; y++)
            {
                for(int z = 0; z < zDim; z++)
                {
                    Vector3 pos = transform.position;
                    pos.x += ((x - (xDim / 2f)) * cubeScale);
                    pos.y += (y * cubeScale);
                    pos.z += ((z - (zDim / 2f)) * cubeScale);

                    GameObject obj = Instantiate(Cube, pos, Quaternion.identity);
                    obj.transform.localScale = new Vector3(cubeScale,cubeScale, cubeScale);
                    obj.transform.parent = transform;
                    BuildingComponent comp = obj.AddComponent<BuildingComponent>();
                    comp.SetParent(this.gameObject);
                }
            }
        }
    }
}
