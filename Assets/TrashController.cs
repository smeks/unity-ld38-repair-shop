using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TrashController : MonoBehaviour {
    public Transform WorldRoot;
    public List<GameObject> Items;
    public List<Transform> TrashSpawns;
    public float spawnInterval;

    float timeSpawned;

    void SpawnStuff()
    {
        timeSpawned = Time.fixedTime;
        for (var i = 0; i < TrashSpawns.Count; i++)
        {
            var itemToCreate = UnityEngine.Random.Range(0, Items.Count);
            var instance = Instantiate(Items[itemToCreate]);
            instance.transform.parent = WorldRoot;
            instance.transform.position = TrashSpawns[i].position;
            instance.transform.rotation = TrashSpawns[i].rotation;
        }
    }

    // Use this for initialization
    void Start () {
        SpawnStuff();
    }
	
	// Update is called once per frame
	void Update () {
		if((Time.fixedTime - timeSpawned) > spawnInterval)
        {
            SpawnStuff();
        }
	}
}
