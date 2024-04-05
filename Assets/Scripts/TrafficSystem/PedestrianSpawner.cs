using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    public static PedestrianSpawner Instance => s_Instance;
    private static PedestrianSpawner s_Instance;

    [SerializeField] private GameObject waypointPrent;
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private int poolSize = 10;
    private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField]private List<GameObject> npcPool = new List<GameObject>();

    private void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
    }
    private void Start()
    {
        InitializeNPCPoolAndSpawn();
    }
    public void InitializeNPCPoolAndSpawn()
    {
        foreach (Transform child in waypointPrent.transform)
        {
            if (child.GetComponent<Waypoint>().WaypointType == WaypointType.Path)
            {
                spawnPoints.Add(child);
            }
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject npc = Instantiate(npcPrefab, transform.position, Quaternion.identity);
            npc.transform.SetParent(transform);
            npc.SetActive(false);
            npcPool.Add(npc);
        }

        SpawnNPCs();
    }

    private void SpawnNPCs()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject npc = GetFromPool();
            if (npc != null)
            {
                npc.transform.position = spawnPoint.position;
                npc.SetActive(true);
                WaypointNavigator navigator = npc.GetComponent<WaypointNavigator>();
               navigator.SetCurrent(spawnPoint.GetComponent<Waypoint>());
            }
        }
    }
    public void AddToPool(GameObject npc)
    {
        npc.SetActive(false);
    }
    public GameObject GetFromPool()
    {
        foreach (GameObject npc in npcPool)
        {
            if (!npc.activeInHierarchy)
            {
                return npc;
            }
        }
        return null;
    }
    private void MaintainPoolPopulation(int desiredSize)
    {
        foreach (GameObject npc in npcPool)
        {
            if (!npc.activeInHierarchy)
            {
                desiredSize--;
            }
        }
        for (int i = 0; i < desiredSize; i++)
        {
            GameObject npca = Instantiate(npcPrefab, transform.position, Quaternion.identity);
            npca.transform.SetParent(transform);
            npca.SetActive(false);
            npcPool.Add(npca);
        }
    }
    public void RespawnNPC(int desiredSize)
    {
        MaintainPoolPopulation(desiredSize);

        for (int i = 0; i < desiredSize; i++)
        {
            GameObject npc = GetFromPool();
            if (npc != null)
            {
                npc.SetActive(true);
                int index = Random.Range(0, spawnPoints.Count);
                npc.transform.position = spawnPoints[index].position;

                WaypointNavigator navigator = npc.GetComponent<WaypointNavigator>();
                if (navigator != null)
                {
                    navigator.SetCurrent(spawnPoints[index].GetComponent<Waypoint>());
                }
            }
            else
            {
                Debug.Log("NPC pool is empty. Cannot respawn NPC.");
            }
        }
    }
}
