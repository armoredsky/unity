using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    private Color startColor;
    private Material material;
    public bool isTargeted = false;

    // Use this for initialization
    void Start () {
        material = GetComponent<Renderer>().material;
        startColor = material.color;

    }
	
	// Update is called once per frame
	void Update () {
        if (isTargeted)
        {
            Target();
            isTargeted = false;
        }
        else
        {
            Untarget();
        }
	
	}

    void Target()
    {
        material.color = Color.green;
    }

    void Untarget()
    {
        material.color = startColor;
    }
}
