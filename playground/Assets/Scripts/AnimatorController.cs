using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour {
    public Animator anim;
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        

    }
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("somehting fucked up");
        }
    }
   
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Activated 'PressDown' on the Trigger");
            anim.SetBool("Walk", true);
        }
        else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Activated 'PressUP' on the Trigger");
            anim.SetBool("Walk", false);
        }
      
    }
}
