using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMoss : MonoBehaviour {

    [SerializeField] private GameObject effectPrefab;

    private GameObject player;
    private WordPosition wordPosition;
    private Audiomanager AM;
    private GameManager GM;
    private WordManager wordManager;
    private PreSpawnHandler preSpawnHandler;

    private bool wasLaunched;
    private float flightSpeed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wordPosition = GetComponent<WordPosition>();
        AM = FindObjectOfType<Audiomanager>();
        GM = FindObjectOfType<GameManager>();
        wordManager = FindObjectOfType<WordManager>();
        preSpawnHandler = FindObjectOfType<PreSpawnHandler>();
    }

    private void Start()
    {
        flightSpeed = 0.5f;
        wasLaunched = false;
    }

    void Update ()
    {
        if (wasLaunched)
        {
            Move();
        }
    }

    // Launches rock
    public void BeThrown()
    {
        transform.parent = null;
        wasLaunched = true;
    }

    // Moves rock towards player
    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * flightSpeed);
    }

    // Triggers collision with player
    // !! This connects to a different function within the manager because it doesn't invoke wordTyped() !!
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (wordPosition.textObj)
            {
                WordDisplay displayObj = wordPosition.textObj.GetComponentInChildren<WordDisplay>();
                wordManager.DeleteProjectileChar(displayObj);
            }
            GM.DamagePlayer();
            Delete();
        }
    }

    // Triggers on Destroy, either by hitting player or being defeated
    private void OnDestroy()
    {
        AM.Play("earthquake1");
        
        GM.hasActiveProjectile = false;

        preSpawnHandler.TrySetOnRails();
    }

    // Deletes gameobject
    public void Delete()
    {
        if (wordPosition.textObj)
        {
            wordManager.DeleteProjectileWord(wordPosition.textObj.GetComponentInChildren<WordDisplay>());
            wordPosition.textObj.GetComponentInChildren<WordDisplay>().RemoveWord();
        }

        //  Instantiate particle effect and detaches it
        GameObject go = Instantiate(effectPrefab, transform);
        go.transform.parent = null;

        Destroy(gameObject);
    }
}
