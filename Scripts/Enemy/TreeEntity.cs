using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEntity : MonoBehaviour {

    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private Transform rockHolder;
    private GameManager GM;
    private Animator anim;
    private GameObject player;

    private bool hasBeenActivated;
    private bool isHoldingRock;
    private float timer;
    private float throwCooldown;
    private GameObject activeRock;
    private float activationDistance;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        GM = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        hasBeenActivated = false;
        throwCooldown = 5.0f;
        activationDistance = 10f;

        // Set timer high, so that the first throw can be done instantly
        timer = 5.0f;

        SpawnRock();
    }

    private void Update()
    {
        if (hasBeenActivated)
        {
            timer += Time.deltaTime;

            if(timer >= throwCooldown)
            {
                RockThrowAnim();
            }
        }

        if(!hasBeenActivated && MagnitudeChecker() <= activationDistance)
        {
            hasBeenActivated = true;
        }
    }

    // Spawns a rock in this enemy's hand
    private void SpawnRock()
    {
        GameObject rock = Instantiate(rockPrefab, rockHolder.position, Quaternion.identity);
        rock.transform.SetParent(rockHolder);
        isHoldingRock = true;
        anim.SetBool("IsHoldingRock", isHoldingRock);

        activeRock = rock;
    }

    // Communicates with the rocks script to launch it
    private void RockThrowAnim()
    {
        // Actual Launch will be triggered from animation
        anim.SetTrigger("ThrowRock");

        // Rock Gathering animation is triggered through animator here
    }

    // Launches Rock
    public void LaunchRock()
    {
        isHoldingRock = false;
        anim.SetBool("IsHoldingRock", isHoldingRock);

        activeRock.GetComponent<RockMoss>().BeThrown();
        activeRock.GetComponent<TagTrigger>().SpawnTag();
        timer = 0.0f;

        GM.hasActiveProjectile = true;
        activeRock = null;
    }

    // Checks Magnitude
    private float MagnitudeChecker()
    {
        Vector3 vec = player.transform.position - transform.position;
        return vec.magnitude;
    }

    // Destroys held rocks if tree dies
    public void DestroyActiveRock()
    {
        if (activeRock)
        {
            Destroy(activeRock);
        }
    }
}
