using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptDungeonRoom : MonoBehaviour
{
    [SerializeField] private GameObject exitPortal;
    [SerializeField] private GameObject spawnNode;
    [SerializeField] private GameObject attackHolder;
    [SerializeField] private GameObject enemyHolder;
    [SerializeField] private GameObject dropsHolder;
    [SerializeField] private GameObject dungeonHolder;
    [SerializeField] private GameObject[] chest;

    private void Awake()
    {
        _ = Instantiate(exitPortal, Vector3.zero, Quaternion.identity, dungeonHolder.transform);
    }
}
