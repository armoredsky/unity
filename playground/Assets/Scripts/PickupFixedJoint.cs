using UnityEngine;
using System.Collections;

public class PickupFixedJoint : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    FixedJoint fixedJoint;
    public Rigidbody rigidbodyAttachPoint;
    public Transform sphere;


    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            sphere.transform.position = new Vector3(-1, 1, 0);
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

    }

    void OnTriggerStay(Collider col)
    {
        bool fjn = fixedJoint == null;
        Debug.Log("fixedJoint is null " + fjn.ToString() );
        if(fixedJoint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Attaching!!!");
            fixedJoint = col.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = rigidbodyAttachPoint;
        }
       if (fixedJoint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Detaching!!!");
            GameObject go = fixedJoint.gameObject;
            Rigidbody rb = go.GetComponent<Rigidbody>();

            Object.Destroy(fixedJoint);
            fixedJoint = null;

            tossObject(rb);
        }
       
    }

    private void tossObject(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            Debug.Log("Throwing with Origin!!!");
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }
        else
        {
            Debug.Log("Else Throwing ");
            rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;
        }

    }

}
