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

    private float gripTimer = 0f;
    
    private Power activePower;
    private bool isActive = false;
    enum POWER
    {
        METEOR,
        LIGHTNING
    }
    private POWER powerIndex;


    // Start is called before the first frame update
    void Start()
    {
        switch(powerIndex)
        {
            case POWER.METEOR:
                activePower = new MeteorPower();
                break;
            case POWER.LIGHTNING:
                activePower = new LightningPower();
                break;
        }
        activePower.Initialize(ControllerPose, InputSource);
    }

    // Update is called once per frame
    void Update()
    {
        if(TriggerPress.GetStateDown(InputSource))
        {
            activePower.TriggerDown();
            isActive = true;
        }
        if(TriggerPress.GetStateUp(InputSource))
        {
            activePower.TriggerUp();
            isActive = false;
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
        if(isActive)
        {
            activePower.UpdateControllerState();
            activePower.UpdateTouchpad(SteamVR_Actions._default.Trackpad.GetAxis(InputSource), SteamVR_Actions._default.Trackpad.GetLastAxis(InputSource));
        }
    }
}
