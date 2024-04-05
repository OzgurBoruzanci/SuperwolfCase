using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaypointType
{
    Path,
    Branch,
    ExitBranch,
    Exit
}
public class Waypoint : MonoBehaviour
{
    [SerializeField] private List<Waypoint> branches;
    [SerializeField] private Waypoint previousWaypoint;
    [SerializeField] private Waypoint nextWaypoint;
    [SerializeField, Range(0, 5f)] private float with = 1f;
    [SerializeField, Range(0, 1f)] private float branchRatio = 0.5f;
    [SerializeField] private WaypointType waypointType;
    #region Properties
    public Waypoint PreviousWaypoint { get => previousWaypoint; set => previousWaypoint = value; }
    public Waypoint NextWaypoint { get => nextWaypoint; set => nextWaypoint = value; }
    public float With { get => with; }
    public float BranchRatio { get => branchRatio; }
    public List<Waypoint> Branches { get => branches; }
    public WaypointType WaypointType { get => waypointType; set => waypointType = value; }
    #endregion
    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * with / 2f;
        Vector3 maxBound = transform.position - transform.right * with / 2f;
        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }
}
