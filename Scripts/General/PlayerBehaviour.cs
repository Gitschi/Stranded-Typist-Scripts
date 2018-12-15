using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private GameEvents gameEvents;
    private GameManager GM;
    private int currentSeg;
    private float transition;
    private bool isCompleted;
    private int goalIndex;

    public RailSystem railSys;
    public GameStates gameState;
    public List<Waypoint> waypoints = new List<Waypoint>();

    private void Awake()
    {
        gameEvents = FindObjectOfType<GameEvents>();
        GM = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        AddWaypoints();

        currentSeg = 1;
        gameState = GameStates.OnRail;
        goalIndex = 2;
    }

    void Update ()
    {
        // returns if no rail exists
        if (!railSys)
        {
            return;
        }

        // Moves player along rail
        if (gameState == GameStates.OnRail && !GM.hasActiveProjectile)
        {
            MovePlayer();
        }
	}

    // Creates all waypoints and adds them to the list
    private void AddWaypoints()
    {
        Waypoint[] createdWaypoints =
        {
            new Waypoint(0, EventType.None),       // 1 To on-boat
            new Waypoint(6, EventType.Dismount),   // 2 To Docking-point
            new Waypoint(1, EventType.WaveAndPre), // 3 To Docking-house
            new Waypoint(2, EventType.Wave),       // 4 To Beach House
            new Waypoint(3, EventType.Wave),       // 5 To Dragon Boat
            new Waypoint(2, EventType.Wait),       // 6 To Between Boat and Beach-Living
            new Waypoint(4, EventType.Wave),       // 7 To Beach living
            new Waypoint(3, EventType.WaveAndPre), // 8 To Stones
            new Waypoint(3, EventType.Wait),       // 9 To AfterStones
            new Waypoint(4, EventType.WaveAndPre), // 10 To Rune
            new Waypoint(4, EventType.Wait),       // 11 to BlockingTree
            new Waypoint(4, EventType.WaveAndPre), // 12 To NorthHouse
            new Waypoint(4, EventType.Wait),       // 13 To AfterNorthHouse
            new Waypoint(5, EventType.WaveAndPre), // 14 To NorthBackturned
            new Waypoint(3, EventType.Wait),       // 15 To After Crane
            new Waypoint(3, EventType.Wave),       // 16 To CenterBarracks
            new Waypoint(3, EventType.Wave),       // 17 To Center
            new Waypoint(4, EventType.WaveAndPre), // 18 To CenterFacingWater
            new Waypoint(3, EventType.Wait),       // 19 To CenterTreeGuy
            new Waypoint(3, EventType.WaveAndPre), // 20 To BlackSmith
            new Waypoint(4, EventType.Wait),       // 21 To AfterBlackSmith
            new Waypoint(4, EventType.Wave),       // 22 To NorthWall
            new Waypoint(3, EventType.WaveAndPre), // 23 To HighHouse
            new Waypoint(3, EventType.Wait),       // 24 To After HighHouse
            new Waypoint(2, EventType.None),       // 25 To NorthExit
            new Waypoint(3, EventType.End),        // 26 To BossArea
            new Waypoint(3, EventType.End)         // 27 End
        };

        for (int i = 0; i < createdWaypoints.Length; i++)
        {
            waypoints.Add(createdWaypoints[i]);
        }
    }

    // Moves player along rail
    private void MovePlayer()
    {
        transition += Time.deltaTime / waypoints[currentSeg].transTime;

        if(transition > 1)
        {
            // Triggers Game Event
            gameEvents.PlayEvent(waypoints[currentSeg]);
            goalIndex++;

            // Transitions to next point on rail
            transition = 0;
            currentSeg++;
        }

        // Sets target location to next node if it exists
        if(railSys.waypoints[currentSeg + 1])
        {
            transform.position = railSys.LinearPosition(currentSeg, transition);
            transform.rotation = railSys.Orientation(currentSeg, transition);
        }
        else
        {
            railSys = null;
        }
    }

    // Dismounts from Vehicle
    public void Dismount()
    {
        GameObject vehicle = GameObject.FindGameObjectWithTag("Vehicle");
        vehicle.transform.parent = null;
    }
}

// Enum of game states
public enum GameStates
{
    OnRail,
    PlayScene,
    CutScene
};