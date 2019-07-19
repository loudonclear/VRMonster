using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningPower : Power
{
    private GameObject groundMarker;
    private GameObject lightningObj;

    /*public void Initialize(SteamVR_Action_Pose controllerPose, SteamVR_Input_Sources inputSource)
    {
        base.Initialize(controllerPose, inputSource);
    }*/

    public override void TriggerDown()
    {
        base.TriggerDown();

        Vector3 pos = ControllerPose.GetLocalPosition(InputSource);
        pos.y = 0f;
        Quaternion rot = ControllerPose.GetLocalRotation(InputSource);
        groundMarker = Instantiate(Resources.Load("HitMarker"), pos, rot) as GameObject;
    }

    public override void TriggerUp()
    {
        base.TriggerUp();

        Vector3 pos = groundMarker.transform.position;
        pos.y = 10f;
        Quaternion rot = Quaternion.identity;
        GameObject lightning = Instantiate(Resources.Load("Meteor"), pos, rot) as GameObject;
        lightning.GetComponent<Meteor>().SetActive();
        lightning.GetComponent<Rigidbody>().velocity = new Vector3(0f, -10f, 0f);
    }

    public override void UpdateControllerState()
    {
        //doesn't currently update based off controller position
    }

    public override void UpdateTouchpad(Vector2 currentPos, Vector2 lastPos)
    {
        Vector2 trackpadDir = currentPos - lastPos;
        //IF DOESN'T WORK, TRY DIFFERENT ANGLE.
        trackpadDir = rotateVector(trackpadDir, transform.eulerAngles.y);

        Vector3 newPos = groundMarker.transform.position;
        newPos.x += 3 * trackpadDir.x;
        newPos.z += 3 * trackpadDir.y;
        groundMarker.transform.position = newPos;
    }

    private Vector2 rotateVector(Vector2 vec, float angle)
    {
        Vector3 temp = new Vector3(vec.x, 0f, vec.y);
        temp = Quaternion.AngleAxis(angle, Vector3.up) * temp;
        return new Vector2(temp.x, temp.z);
    }
}
