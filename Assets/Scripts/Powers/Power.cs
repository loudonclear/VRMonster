using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public abstract class Power : MonoBehaviour
{

    protected Transform ControllerTransform;
    protected SteamVR_Action_Pose ControllerPose;
    protected SteamVR_Input_Sources InputSource;

    private bool active = false;

    public void Initialize(Transform controllerTransform, SteamVR_Action_Pose controllerPose, SteamVR_Input_Sources inputSource)
    {
        ControllerTransform = controllerTransform;
        ControllerPose = controllerPose;
        InputSource = inputSource;
    }

    public virtual void TriggerDown()
    {
        active = true;
    }
    public virtual void TriggerUp()
    {
        active = false;
    }

    public abstract void UpdateControllerState();
    public abstract void UpdateTouchpad(Vector2 currentPos, Vector2 lastPos);



}
