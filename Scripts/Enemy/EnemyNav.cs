using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour {

    private NavMeshAgent agent;
    private GameObject target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
    }

    void Start ()
    {
        agent.SetDestination(target.transform.position);
        StartCoroutine(SetAgentDestination());
	}

    //Sets Target position every 0.1 seconds
    private IEnumerator SetAgentDestination()
    {
        while (true)
        {
            if (agent && agent.isActiveAndEnabled)
            {
                agent.SetDestination(target.transform.position);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield break;
            }
        }
    }

    // Gets position of target
    public Vector3 GetTargetPos()
    {
        return target.transform.position;
    }
}
