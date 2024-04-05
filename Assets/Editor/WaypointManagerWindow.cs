using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointManagerWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointManagerWindow>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    private void DrawButtons()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }

        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if (GUILayout.Button("add Branch Waypoint"))
            {
                CreateBranch();
            }
            if (GUILayout.Button("add Exit Branch Waypoint"))
            {
                CreateExitBranch();
            }
            if (GUILayout.Button("Create Waypoint Before"))
            {
                CreateWaypointBefore();
            }

            if (GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointAfter();
            }

            if (GUILayout.Button("Create Exit Waypoint"))
            {
                CreateExitWaypoint();
            }

            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
        }
    }

    private void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        waypoint.WaypointType = WaypointType.Path;
        if (waypointRoot.childCount > 1)
        {
            waypoint.PreviousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.PreviousWaypoint.NextWaypoint = waypoint;
            waypoint.transform.position = waypoint.PreviousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.PreviousWaypoint.transform.forward;
        }

        Selection.activeGameObject = waypoint.gameObject;
    }

    private void CreateWaypointBefore()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        newWaypoint.WaypointType = WaypointType.Path;
        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if (selectedWaypoint.PreviousWaypoint != null)
        {
            newWaypoint.PreviousWaypoint = selectedWaypoint.PreviousWaypoint;
            selectedWaypoint.PreviousWaypoint.NextWaypoint = newWaypoint;
        }
        newWaypoint.NextWaypoint = selectedWaypoint;
        selectedWaypoint.PreviousWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }
    private void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        newWaypoint.WaypointType = WaypointType.Path;
        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;
        newWaypoint.PreviousWaypoint = selectedWaypoint;
        if (selectedWaypoint.NextWaypoint != null)
        {
            selectedWaypoint.NextWaypoint.PreviousWaypoint = newWaypoint;
            newWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
        }
        selectedWaypoint.NextWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }
    private void RemoveWaypoint()
    {
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        if (selectedWaypoint.NextWaypoint != null)
        {
            selectedWaypoint.NextWaypoint.PreviousWaypoint = selectedWaypoint.PreviousWaypoint;
        }
        if (selectedWaypoint.PreviousWaypoint != null)
        {
            selectedWaypoint.PreviousWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
            Selection.activeGameObject = selectedWaypoint.PreviousWaypoint.gameObject;
        }
        DestroyImmediate(selectedWaypoint.gameObject);
    }
    private void CreateBranch()
    {
        GameObject waypointObject = new GameObject("Branch Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        Waypoint branchedFrom = Selection.activeGameObject.GetComponent<Waypoint>();
        waypoint.WaypointType = WaypointType.Branch;
        branchedFrom.Branches.Add(waypoint);
        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;
        
        Selection.activeGameObject = waypoint.gameObject;
    }
    private void CreateExitBranch()
    {
        GameObject waypointObject = new GameObject("Exit Branch Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        Waypoint branchedFrom = Selection.activeGameObject.GetComponent<Waypoint>();
        waypoint.WaypointType = WaypointType.ExitBranch;
        branchedFrom.Branches.Add(waypoint);
        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }
    private void CreateExitWaypoint()
    {
        GameObject waypointObject = new GameObject("Exit Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        waypoint.WaypointType = WaypointType.Exit;
        if (waypointRoot.childCount > 1)
        {
            waypoint.PreviousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.PreviousWaypoint.NextWaypoint = waypoint;
            waypoint.transform.position = waypoint.PreviousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.PreviousWaypoint.transform.forward;
        }

        Selection.activeGameObject = waypoint.gameObject;
    }
}
