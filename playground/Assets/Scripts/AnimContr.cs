using UnityEngine;
using System.Collections;

public class AnimContr : MonoBehaviour {
    public Animator anim;
    // Use this for initialization

    SteamVR_Controller.Device device;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("somehting fucked up");
        }
    }

    void Update()
    {
        controllerUpdate();
        keyboardUpdate();
    }
    
    void keyboardUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Walk", true);
        } 
        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Walk", false);
        }
    }

    void controllerUpdate()
    {
        device = SteamVR_Controller.Input(1);
        Debug.Log("device: " + device.ToString());
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Activated 'PressDown' on the Trigger");
            anim.SetBool("Walk", true);
        }
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Activated 'PressUP' on the Trigger");
            anim.SetBool("Walk", false);
        }

    }
}
