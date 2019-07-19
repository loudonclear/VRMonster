using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class MeteorPower : Power
{

    private GameObject meteorObj;

    /*public void Initialize(SteamVR_Action_Pose controllerPose, SteamVR_Input_Sources inputSource)
    {
        base.Initialize(controllerPose, inputSource);
    }*/

    public override void TriggerDown()
    {
        base.TriggerDown();
        Vector3 pos = ControllerPose.GetLocalPosition(InputSource);
        Quaternion rot = ControllerPose.GetLocalRotation(InputSource);
        meteorObj = Instantiate(Resources.Load("Meteor"), pos, rot) as GameObject;
    }

    public override void TriggerUp()
    {
        base.TriggerUp();
        meteorObj.GetComponent<Meteor>().SetActive();
        meteorObj.GetComponent<Rigidbody>().velocity = 5f * ControllerPose.GetVelocity(InputSource);
    }

    public override void UpdateControllerState()
    {
        meteorObj.transform.position = transform.position;
    }

    public override void UpdateTouchpad(Vector2 currentPos, Vector2 lastPos)
    {
        //doesn't use touch pad
    }
}
