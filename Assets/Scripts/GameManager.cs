using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ReSpawnNPC());
    }
    private IEnumerator ReSpawnNPC()
    {
        yield return new WaitForSeconds(10f);
        PedestrianSpawner.Instance.RespawnNPC(15);
    }
}
