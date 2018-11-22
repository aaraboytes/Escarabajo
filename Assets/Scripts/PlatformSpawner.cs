using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
    Transform player;
    public int numberOfTiles;
    public float ySize;
    public Pool tiles;
    public Pool poop;
    public Pool obstacle;
    public Pool Butterflies;
    Vector3 firstTileSpawned;
	void Start () {
        player = Transform.FindObjectOfType<Player>().transform;       
        for(int i = 1; i <= numberOfTiles; i++)
        {
            SpawnTile(new Vector3(0.0f, 0.0f, i * ySize + player.transform.position.z));
        }
        firstTileSpawned = new Vector3(0, 0, 0);
    }

    void Update() {
        if (player.transform.position.z > firstTileSpawned.z+ySize) {
            SpawnTile(new Vector3(0.0f, 0.0f, numberOfTiles * ySize + player.transform.position.z));
        }
	}

    void SpawnTile(Vector3 position)
    {
        GameObject tile = tiles.Recycle(position);
        firstTileSpawned = new Vector3(0.0f, 0.0f, firstTileSpawned.z + ySize);
        if(Random.value>0.8f)
            SpawnObj(position+Vector3.up,poop);
        else if (Random.value > 0.4f)
        {
            SpawnObj(position + Vector3.up, obstacle);
        }
        if (Random.value > 0.9f)
        {
            SpawnObj(position + (Vector3.up * Random.Range(2.5f, 3.5f)), Butterflies);
        }
    }

    void SpawnObj(Vector3 position,Pool go)
    {
        int selector = (int)Random.Range(0, 3);
        if (selector == 1)
            position.x = 1.5f;
        else if (selector == 2)
            position.x = -1.5f;
        else
            position.x = 0.0f;
        go.Recycle(position);
    }
}
