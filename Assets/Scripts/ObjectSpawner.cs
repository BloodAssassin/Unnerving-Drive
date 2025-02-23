using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] GameObject[] obstacles;
    [SerializeField] PlayerController playerRef;
    List<GameObject> surroundingObjects = new();
    List<GameObject> spawnedObstacles = new();
    float timeSinceSpawningObstacles = 0.0f;

    void Update()
    {
        this.timeSinceSpawningObstacles += Time.deltaTime;
        if (this.timeSinceSpawningObstacles > Random.Range(4.0f, 8.0f))
        {
            this.SpawnObstacle();
            this.timeSinceSpawningObstacles = 0.0f;
        }

        if (!this.playerRef.IsRoadRestarted())
            return;

        // Destroy When Distanced
        foreach (var obj in this.surroundingObjects)
            Destroy(obj);

        this.surroundingObjects.Clear();

        foreach (var obj in this.spawnedObstacles)
        {
            if (Vector3.Distance(obj.transform.position, this.playerRef.transform.position) < 350.0f)
                continue;

            Destroy(obj);
        }

        this.spawnedObstacles.Clear();

        for (int i = 0; i < Random.Range(5, 10); i++)
            this.SpawnSideRoadObject();
    }

    private void SpawnSideRoadObject()
    {
        Vector3 newPosition = new()
        {
            x = Random.Range(-40.0f, -55.0f),
            z = Random.Range(-50.0f, -20.0f)
        };

        foreach (var obj in this.surroundingObjects)
        {
            // This prevents the spawning of new objects if they're too close to each other
            if (Vector3.Distance(obj.transform.position, newPosition) < 5.0f)
                return;
        }

        GameObject instance = Instantiate(this.objects[Random.Range(0, this.objects.Length)]);
        instance.transform.position = newPosition;
        instance.transform.parent = this.transform.parent;
        this.surroundingObjects.Add(instance);
    }

    private void SpawnObstacle()
    {
        Vector3 position = new()
        {
            x = -30.0f + (10.0f * Random.Range(0, 3)),
            z = Random.Range(-20.0f, 0.0f)
        };

        GameObject obstacle = Instantiate(this.obstacles[Random.Range(0, this.obstacles.Length - 1)], this.transform);
        obstacle.transform.position = position;
        this.spawnedObstacles.Add(obstacle);
    }
}