using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagTrigger : MonoBehaviour
{
    private bool hasTriggered;
    private float maxDistance;

    public bool hasTriggerBlock;
    public GameObject player;
    public WordPosition wordPosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wordPosition = GetComponent<WordPosition>();
    }

    private void Start()
    {
        hasTriggered = false;
        maxDistance = 12f;
        hasTriggerBlock = false;
    }

    void Update ()
    {
        if (!hasTriggered && !gameObject.CompareTag("Projectile"))
        {
            CheckLineOfSight();
        }
	}

    // Checks if enemy is in players line of sight
    private void CheckLineOfSight()
    {
        RaycastHit hit;
        Vector3 enemySight = transform.position + new Vector3(0f, 0.5f, 0f);
        Vector3 direction = player.GetComponentInChildren<Camera>().transform.position - enemySight;

        Debug.DrawRay(enemySight, direction, Color.red);

        if (Physics.Raycast(enemySight, direction, out hit, maxDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                SpawnTag();
            }
        }
    }

    // Spawns tag on enemy
    public void SpawnTag()
    {
        wordPosition.SpawnTag();
        hasTriggered = true;
    }
}
