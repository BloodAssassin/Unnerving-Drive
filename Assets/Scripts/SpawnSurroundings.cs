using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnSurroundings : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] PlayerController playerRef;
    List<GameObject> surroundingObjects = new();

    void Update()
    {
        if (!this.playerRef.IsRoadRestarted())
            return;

        foreach (var obj in objects)
            Destroy(obj);

        this.surroundingObjects.Clear();
        this.surroundingObjects.Add(GameObject.Instantiate(this.objects[Random.Range(0, this.objects.Length)]));
    }
}