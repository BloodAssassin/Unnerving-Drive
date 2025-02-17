using System.Collections.Generic;
using UnityEngine;

public class SpawnSurroundings : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] PlayerController playerRef;
    [SerializeField] Transform playerTransform;
    List<GameObject> surroundingObjects = new();

    void Update()
    {
        if (!this.playerRef.IsRoadRestarted())
            return;

        // Destroy When Distanced
        CleanProps();

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

    private void CleanProps()
    {
        foreach (var obj in surroundingObjects)
        {
            if (Vector3.Distance(playerTransform.position, obj.transform.position) > 5.0f)
            {
                DestroyImmediate(obj, true);
            }
        }
    }
}