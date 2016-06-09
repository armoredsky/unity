using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour {
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    public GameObject prefabSphere;


    // Use this for initialization
    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'Touch' on the Trigger");
        } if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Activated 'TouchDown' on the Trigger");
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Activated 'TouchUp' on the Trigger");
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'GetPress' on the Trigger");
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Activated 'PressDown' on the Trigger");
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Activated 'PressUp' on the Trigger");
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            device.TriggerHapticPulse(1000);
            GameObject.Instantiate(prefabSphere);
            //sphere.transform.position = new Vector3(0, 0, 0);
            //sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            if("Interactable".Equals(col.gameObject.tag))
            {

                device.TriggerHapticPulse();
                Debug.Log("touch down on col tagged: " + col.gameObject.tag);
                col.attachedRigidbody.isKinematic = true;
                col.gameObject.transform.SetParent(gameObject.transform);

            }
            
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (col.gameObject.tag == "Interactable")
            {
                col.attachedRigidbody.isKinematic = false;
                col.gameObject.transform.SetParent(null);
                tossObject(col.attachedRigidbody);
            }
        }
    }

    private void tossObject(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if(origin != null)
        {
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);
        } else
        {
            rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;
        }
        
    }


}
