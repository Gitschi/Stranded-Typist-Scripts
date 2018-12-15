using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour: MonoBehaviour {

    [SerializeField] private GameObject explosionPrefab;

    private Audiomanager AM;
    private GameManager GM;
    private Animator anim;
    private CapsuleCollider capsCol;
    private BoxCollider boxCol;
    private EnemyNav enemyNav;
    private float velocity;
    public NavMeshAgent navAgent;
    private Rigidbody rb;

    private bool canAttack;
    private float swingTimer;
    private float swingCooldown;
    private bool hasSwung;
    private float sinkSpeed;
    private bool isSinking;

    public EnemyType enemyType;

    private void Awake()
    {
        AM = FindObjectOfType<Audiomanager>();
        GM = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
        capsCol = GetComponent<CapsuleCollider>();
        boxCol = GetComponent<BoxCollider>();
        enemyNav = GetComponent<EnemyNav>();
    }

    private void Start()
    {
        canAttack = true;
        swingCooldown = 3f;
        sinkSpeed = 2.5f;
        hasSwung = false;
        isSinking = false;

        SetTag();
    }

    void Update () {
        // Updates velocity on agent
        if (navAgent)
        {
            anim.SetFloat("Velocity", navAgent.velocity.magnitude);
        }

        // Attacks player and starts cooldown
        if(hasSwung || navAgent && enemyNav && TargetProximityCheck() && VelocityCheck())
        {
            if (canAttack)
            {
                Attack();
                canAttack = false;
                swingTimer = 0;
                hasSwung = true;
            }
        }

        // Increment swingtimer if enemy can't attack
        if (!canAttack)
        {
            swingTimer += Time.deltaTime;

            if(swingTimer >= swingCooldown)
            {
                canAttack = true;
            }
        }

        // Slowly sinks enemy into the floor when dead
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    // Triggers Enemy to attack
    public void Attack()
    {
        if (anim)
        {
            anim.SetTrigger("Attack");
        }
    }

    // Triggers from animation when player is hit
    public void HitPlayer()
    {
        GM.DamagePlayer();

        hasSwung = true;

        DestroyComponents();

        // Play sound effect on hitting player
        switch (enemyType)
        {
            case EnemyType.WOLF:
                if (GetRand(2) == 0)
                {
                    AM.Play("warg_attack1");
                }
                else
                {
                    AM.Play("warg_attack2");
                }
                break;

            case EnemyType.SKELETON:
                AM.Play("ice8");
                break;

            default:
                break;
        }
    }

    // Plays Die animation and removes unnecessary parts
    public void Die()
    {
        anim.SetTrigger("Die");

        isSinking = true;

        DestroyComponents();

        if (GM.gameMode != GameMode.Arcade)
        {
            GM.SpeedModeSpawnReset();
        }

        // Handle behavior on death depending on enemyType
        switch (enemyType)
        {
            case EnemyType.WOLF:
                if (GetRand(2) == 0)
                {
                    AM.Play("warg_death1");
                }
                else
                {
                    AM.Play("warg_death2");
                }
                break;

            case EnemyType.SKELETON:
                if (GetRand(2) == 0)
                {
                    AM.Play("beast_death1");
                }
                else
                {
                    AM.Play("beast_death2");
                }
                break;

            case EnemyType.TREE:
                AM.Play("nature5");
                GetComponent<TreeEntity>().DestroyActiveRock();
                break;

            default:
                break;
        }

        Instantiate(explosionPrefab, transform);
    }

    // Destroys game object
    public void Delete()
    {
        Destroy(gameObject);
    }

    // Returns magnitude between current position and player
    private float magnitudeChecker()
    {
        Vector3 vec = enemyNav.GetTargetPos() - transform.position;
        return vec.magnitude;
    }

    // Checks if enemy is close enough to attack
    private bool TargetProximityCheck()
    {
        return magnitudeChecker() <= 5;
    }

    // Cecks if enemy is moving
    private bool VelocityCheck()
    {
        return navAgent.velocity.magnitude <= 0.5f;
    }

    // Sets tag for enemy
    private void SetTag()
    {
        if(GM.gameMode == GameMode.Miss || GM.gameMode == GameMode.Speed)
        {
            gameObject.tag = "DrillEnemy";
        }
    }

    // Destoys extra components
    private void DestroyComponents()
    {
        if (rb)
        {
            Destroy(rb);
        }

        if (navAgent)
        {
            Destroy(enemyNav);
            Destroy(navAgent);
            //navAgent.isStopped = true;
        }

        if (capsCol)
        {
            Destroy(capsCol);
        }
        else if (boxCol)
        {
            Destroy(boxCol);
        }    
    }

    // Creates random number 0 and input
    private int GetRand(int maxNumber)
    {
        return Random.Range(0, maxNumber);
    }

    // Enum of enemy types
    public enum EnemyType {WOLF, SKELETON, TREE}
}