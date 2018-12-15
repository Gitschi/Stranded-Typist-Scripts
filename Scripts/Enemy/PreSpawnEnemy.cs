using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PreSpawnEnemy : MonoBehaviour {

    private NavMeshAgent agent;
    private Rigidbody rb;
    private GameObject player;
    private bool hasBeenActivated;

    private float activationDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start () {
        StopAgent();

        activationDistance = 20f;
	}
	
	void Update () {
        if(!hasBeenActivated && MagnitudeChecker() <= activationDistance)
        {
            ResumeAgent();
            hasBeenActivated = true;
        }
	}

    // Stops Agent
    private void StopAgent()
    {
        agent.isStopped = true;
        hasBeenActivated = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Resumes Agent
    private void ResumeAgent()
    {
        agent.isStopped = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    // Returns magnitude between current position and goal
    private float MagnitudeChecker()
    {
        Vector3 vec = player.transform.position - transform.position;
        return vec.magnitude;
    }
}
