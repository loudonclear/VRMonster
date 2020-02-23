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
    
    private Power selectedPower;
    private bool powerActive = false;
    enum POWER
    {
        METEOR,
        LIGHTNING
    }
    private POWER powerIndex;

    private GameObject menu;
    private bool menuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        powerIndex = POWER.LIGHTNING;
        switch(powerIndex)
        {
            case POWER.METEOR:
                selectedPower = new MeteorPower();
                break;
            case POWER.LIGHTNING:
                selectedPower = new LightningPower();
                break;
        }
        selectedPower.Initialize(transform, ControllerPose, InputSource);
    }

    // Update is called once per frame
    void Update()
    {
        if(TriggerPress.GetStateDown(InputSource))
        {
            selectedPower.TriggerDown();
            powerActive = true;
        }
        if(TriggerPress.GetStateUp(InputSource))
        {
            selectedPower.TriggerUp();
            powerActive = false;
        }
        

        if(PadPress.GetStateDown(InputSource) && !powerActive)
        {
            //Open power select menu
            Destroy(GetComponent<Power>());
            if(powerIndex == POWER.METEOR)
            {
                selectedPower = new LightningPower();
                powerIndex = POWER.LIGHTNING;
            }
            else
            {
                selectedPower = new MeteorPower();
                powerIndex = POWER.METEOR;
            }
            selectedPower.Initialize(transform, ControllerPose, InputSource);
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
        if(powerActive)
        {
            selectedPower.UpdateControllerState();
            if (SteamVR_Actions._default.TrackpadTouch.GetState(InputSource) && SteamVR_Actions._default.TrackpadTouch.GetLastState(InputSource))
            {
                selectedPower.UpdateTouchpad(SteamVR_Actions._default.Trackpad.GetAxis(InputSource), SteamVR_Actions._default.Trackpad.GetLastAxis(InputSource));
            }
        }


    }
}
