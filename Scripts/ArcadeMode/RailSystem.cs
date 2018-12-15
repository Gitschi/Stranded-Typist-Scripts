using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RailSystem : MonoBehaviour
{
    public Transform[] waypoints;

    private void Start()
    {
        waypoints = GetComponentsInChildren<Transform>();
    }

    // Interpolates position between first and second point
    public Vector3 LinearPosition(int seg, float ratio)
    {
        Vector3 p1 = waypoints[seg].position;
        Vector3 p2 = waypoints[seg + 1].position;

        return Vector3.Lerp(p1, p2, ratio);
    }

    // Interpolates between rotation between first and second point
    public Quaternion Orientation(int seg, float ratio)
    {
        Quaternion q1 = waypoints[seg].rotation;
        Quaternion q2 = waypoints[seg + 1].rotation;

        return Quaternion.Lerp(q1, q2, ratio);
    }
}
