using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenRowboat : MonoBehaviour {

    private PlayerBehaviour playerBehaviour;
    private float travelSpeed;
    private float dockingDistance;
    private bool hasDocked;
    private Vector3 lastPos;
    private Vector3 goalPos;

    public GameObject dockingPoint;

    private void Awake()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    void Start () {
        travelSpeed = 0.5f;
        dockingDistance = 3f;

        hasDocked = false;
        lastPos = transform.position;
        goalPos = dockingPoint.transform.position;
	}
	
	void Update () {
        if (!hasDocked)
        {
            MoveBoat();

            // Docks the boat when close to dockingPoint
            if(MagnitudeChecker() < dockingDistance)
            {
                hasDocked = true;
            }
        }
        else if (hasDocked)
        {
            DockBoat();
        }
	}

    // Returns magnitude between current position and goal
    private float MagnitudeChecker()
    {
        Vector3 vec = goalPos - transform.position;
        return vec.magnitude;
    }

    // Moves boat towards waypoint
    private void MoveBoat()
    {
        transform.position = Vector3.Lerp(lastPos, goalPos, Time.deltaTime * travelSpeed);
        lastPos = transform.position;
    }

    // Docks boat and detaches player
    private void DockBoat()
    {
        transform.DetachChildren();
        playerBehaviour.gameState = GameStates.OnRail;
        Destroy(gameObject.GetComponent<WoodenRowboat>());
    }
}
