using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] roads;
    private float spawn = 0;
    private float roadLeanght = 100;
    private int startspawn = 4;
    [SerializeField] private Transform player;
    private List<GameObject> activeroads = new List<GameObject>();
    void Start()
    {
        SpawnRoad(5);
        for (int i = 0; i <= startspawn; i++)
        {
            SpawnRoad(Random.Range(0, 4));
        }

    }

    
    void Update()
    {
        if(player.position.z - 40 > spawn - (startspawn * roadLeanght))
        {
            SpawnRoad(Random.Range(0, roads.Length));
            Deleteroad();
        }
    }
    private void SpawnRoad(int RoadIndex)
    {
        GameObject newroad = Instantiate(roads[RoadIndex], transform.forward * spawn, transform.rotation);
        activeroads.Add(newroad);
        spawn += roadLeanght;
    }
    private void Deleteroad()
    {
        Destroy(activeroads[0]);
        activeroads.RemoveAt(0);
    }
}
