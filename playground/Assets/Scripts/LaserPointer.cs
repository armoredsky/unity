﻿using UnityEngine;
using System.Collections;

public class LaserPointer : MonoBehaviour {

    GameObject hitObject;
    RaycastHit hit;
		
	// Update is called once per frame
	void Update () {
	    if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) {
            hitObject = hit.transform.gameObject;
            Interactable interactable = hitObject.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.isTargeted = true;
            }

        }
	}
}
