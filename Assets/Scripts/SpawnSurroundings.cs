using System.Collections.Generic;
using UnityEngine;

public class SpawnSurroundings : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] PlayerController playerRef;
    [SerializeField] Vector2 XZSize;
    List<GameObject> surroundingObjects = new();

    void Update()
    {
        if (!this.playerRef.IsRoadRestarted())
            return;

        // Destroy When Distanced
        foreach (var obj in this.surroundingObjects)
            Destroy(obj);

        for (int i = 0; i < Random.Range(2, 5); i++)
            SpawnObject();
    }

    private void SpawnObject()
    {
        GameObject instance = GameObject.Instantiate(this.objects[Random.Range(0, this.objects.Length)]);

        // Random Position
        Vector3 newPosition = instance.transform.position;
        newPosition.x = Random.Range(-39.39f, -47.93f);
        instance.transform.position = newPosition;

        // Set Parent
        instance.transform.parent = transform;

        this.surroundingObjects.Clear();
        this.surroundingObjects.Add(instance);
    }
}