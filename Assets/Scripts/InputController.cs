using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    public SteamVR_Input_Sources InputSource;
    public SteamVR_Action_Pose ControllerPose;

    public SteamVR_Action_Boolean TriggerPress;
    public SteamVR_Action_Boolean PadPress;
    public SteamVR_Action_Boolean GripPress;

    public GameObject MeteorPrefab;

    private float gripTimer;

    private GameObject activeMeteor;


    // Start is called before the first frame update
    void Start()
    {
        gripTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(TriggerPress.GetStateDown(InputSource))
        {
            activeMeteor = Instantiate(MeteorPrefab, transform.position, transform.rotation);
        }
        if(TriggerPress.GetStateUp(InputSource))
        {
            activeMeteor.GetComponent<Meteor>().SetActive();
            Debug.Log(ControllerPose.GetVelocity(InputSource));
            activeMeteor.GetComponent<Rigidbody>().velocity = 5f * ControllerPose.GetVelocity(InputSource);
            activeMeteor = null;
        }

        if(PadPress.GetStateDown(InputSource))
        {
            Debug.Log("Pad");
        }

        if(GripPress.GetStateDown(InputSource))
        {
            gripTimer = 0f;
        }
        if(GripPress.GetState(InputSource))
        {
            gripTimer += Time.deltaTime;
            if(gripTimer > 2f)
            {
                SceneManager.LoadScene("PowerTest");
            }
        }
    }

    private void FixedUpdate()
    {
        if(TriggerPress.GetState(InputSource))
        {
            activeMeteor.transform.position = transform.position;
        }
    }
}
