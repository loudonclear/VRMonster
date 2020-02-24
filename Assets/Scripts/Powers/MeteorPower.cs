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
        Vector3 pos = ControllerTransform.position;
        Quaternion rot = ControllerTransform.rotation;
        meteorObj = Instantiate(Resources.Load("Meteor"), pos, rot) as GameObject;
        meteorObj.GetComponent<Meteor>().SetUnactive();
    }

    public override void TriggerUp()
    {
        base.TriggerUp();
        meteorObj.GetComponent<Meteor>().SetActive();
        meteorObj.GetComponent<Rigidbody>().velocity = 5f * ControllerPose.GetVelocity(InputSource);
        Debug.Log(ControllerPose.GetVelocity(InputSource));
    }

    public override void UpdateControllerState()
    {
        meteorObj.transform.position = ControllerTransform.position;
        meteorObj.transform.rotation = ControllerTransform.rotation;
    }

    public override void UpdateTouchpad(Vector2 currentPos, Vector2 lastPos)
    {
        //doesn't use touch pad
    }
}
