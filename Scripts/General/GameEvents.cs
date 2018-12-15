using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private PlayerBehaviour playerBehaviour;
    private WaveSpawner waveSpawner;
    private PreSpawnHandler preSpawnHandler;
    private GameManager GM;

    private void Awake()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        preSpawnHandler = FindObjectOfType<PreSpawnHandler>();
        GM = FindObjectOfType<GameManager>();
    }

    // Starts playing event of waypoint
    public void PlayEvent(Waypoint waypoint)
    {
        switch (waypoint.eventType)
        {
            case EventType.Dismount:
                playerBehaviour.Dismount();
                break;

            case EventType.Wave:
                playerBehaviour.gameState = GameStates.PlayScene;
                waveSpawner.StartSpawn();
                break;

            case EventType.WaveAndPre:
                playerBehaviour.gameState = GameStates.PlayScene;
                waveSpawner.StartSpawn();
                preSpawnHandler.Spawn();
                break;

            case EventType.Wait:
                playerBehaviour.gameState = GameStates.PlayScene;
                break;

            case EventType.End:
                GM.SetGameOver();
                break;

            default:
                break;
        }
    }
}
