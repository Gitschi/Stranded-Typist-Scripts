using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour {

	void Start () {
        Invoke("Delete", 3.0f);
	}
	
    // Deletes particle effect gameobject
    private void Delete()
    {
        Destroy(gameObject);
    }
}
