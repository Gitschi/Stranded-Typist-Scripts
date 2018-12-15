using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour {

    private float RotateSpeed;
	
	void Update () {
        RotateSkyBox();
    }

    private void Start()
    {
        RotateSpeed = 0.3f;
    }

    // Rotates skybox
    private void RotateSkyBox()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);
    }
}
