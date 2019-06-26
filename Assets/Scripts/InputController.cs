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
    public SteamVR_Action_Boolean TrackpadTouch;

    public GameObject MeteorPrefab;
    public GameObject HitMarker;
    public GameObject LightningStrike;

    private float gripTimer;

    private GameObject activePower;


    // Start is called before the first frame update
    void Start()
    {
        gripTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Reenable for meteor
        /*if(TriggerPress.GetStateDown(InputSource))
        {
            activePower = Instantiate(MeteorPrefab, transform.position, transform.rotation);
        }
        if(TriggerPress.GetStateUp(InputSource))
        {
            activePower.GetComponent<Meteor>().SetActive();
            Debug.Log(ControllerPose.GetVelocity(InputSource));
            activePower.GetComponent<Rigidbody>().velocity = 5f * ControllerPose.GetVelocity(InputSource);
            activePower = null;
        }*/

        if (TriggerPress.GetStateDown(InputSource))
        {
            Vector3 pos = transform.position;
            pos.y = 0.1f;
            activePower = Instantiate(HitMarker, pos, Quaternion.LookRotation(Vector3.up));
        }
        if(TriggerPress.GetStateUp(InputSource))
        {
            Vector3 pos = activePower.transform.position;
            pos.y = 10f;
            GameObject obj = Instantiate(LightningStrike, pos, transform.rotation);
            obj.GetComponent<Rigidbody>().velocity = new Vector3(0f, -10f, 0f);
            Destroy(activePower);
            activePower = null;
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
        //Meteor
        /*if(TriggerPress.GetState(InputSource))
        {
            activePower.transform.position = transform.position;
        }*/
        if (TriggerPress.GetState(InputSource))
        {
            if(TrackpadTouch.GetState(InputSource) && TrackpadTouch.GetLastState(InputSource))
            {
                Vector2 trackpadDir = SteamVR_Actions._default.Trackpad.GetAxis(InputSource) - SteamVR_Actions._default.Trackpad.GetLastAxis(InputSource);
                trackpadDir = rotateVector(trackpadDir, transform.eulerAngles.y);

                Vector3 newPos = activePower.transform.position;
                newPos.x += 3 * trackpadDir.x;
                newPos.z += 3 * trackpadDir.y;
                activePower.transform.position = newPos;
            }
        }
    }

    private Vector2 rotateVector(Vector2 vec, float angle)
    {
        Vector3 temp = new Vector3(vec.x, 0f, vec.y);
        temp = Quaternion.AngleAxis(angle, Vector3.up) * temp;
        return new Vector2(temp.x, temp.z);
    }
}
