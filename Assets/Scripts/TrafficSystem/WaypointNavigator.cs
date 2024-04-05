using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] private Waypoint currentWaypoint;
    private CharacterNavigationController controller;
    private int direction;

    public Waypoint CurrentWaypoint { get => currentWaypoint; set => currentWaypoint = value; }

    private void Awake()
    {
        controller = GetComponent<CharacterNavigationController>();
    }
    private void Update()
    {
        if (controller.ReachedDestination)
        {
            bool shouldBranch = false;
            if (currentWaypoint.Branches != null && currentWaypoint.Branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.BranchRatio ? true : false;
            }
            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.Branches[Random.Range(0, currentWaypoint.Branches.Count - 1)];
            }
            else
            {
                if (direction == 0)
                {
                    if (currentWaypoint.NextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.NextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.PreviousWaypoint;
                        direction = 1;
                    }
                }
                else if (direction == 1)
                {
                    if (currentWaypoint.PreviousWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.PreviousWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.NextWaypoint;
                        direction = 0;
                    }
                }
            }
            
            controller.SetDestination(currentWaypoint.GetPosition());
        }
        if(currentWaypoint.WaypointType==WaypointType.Exit)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void SetCurrent(Waypoint current)
    {
        currentWaypoint = current;
        direction = Mathf.RoundToInt(Random.Range(0, 1));
        controller.SetDestination(currentWaypoint.GetPosition());
    }
}
