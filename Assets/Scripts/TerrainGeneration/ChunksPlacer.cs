using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunksPlacer : MonoBehaviour
{
    public Transform Player;
    public Chunk[] ChunkPrefabs;
    public Chunk FirstChunk;

    private List<Chunk> spawnedChunks = new List<Chunk>();

    private void Start()
    {
        spawnedChunks.Add(FirstChunk);
    }

    private void Update()
    {
        if (Player.position.z > spawnedChunks[spawnedChunks.Count - 1].End1.position.z - 50)
        {
            SpawnChunk(spawnedChunks[spawnedChunks.Count - 1].End1);
            Debug.Log("Проверка на срабатывание End1");
        }
        if (Player.position.z < spawnedChunks[spawnedChunks.Count - 1].End2.position.z + 50)
        {
            SpawnChunk(spawnedChunks[spawnedChunks.Count - 1].End2);
            Debug.Log("Проверка на срабатывание End2");
        }
        if (Player.position.x > spawnedChunks[spawnedChunks.Count - 1].End3.position.x - 50)
        {
            SpawnChunk(spawnedChunks[spawnedChunks.Count - 1].End3);
            Debug.Log("Проверка на срабатывание End3");
        }
        if (Player.position.x < spawnedChunks[spawnedChunks.Count - 1].End4.position.x + 50)
        {
            SpawnChunk(spawnedChunks[spawnedChunks.Count - 1].End4);
            Debug.Log("Проверка на срабатывание End4");
        }
    }

    private void SpawnChunk(Transform End)
    {
        Chunk newChunk = Instantiate(GetRandomChunk());
        newChunk.End1.gameObject.SetActive(false);
        newChunk.ContainerB_E.SetActive(true);
        newChunk.transform.position = End.position - newChunk.Begin2.localPosition;        
        spawnedChunks.Add(newChunk);
        Debug.Log("Спавн чанка из-за триггера  " + End);



        if (spawnedChunks.Count >= 50)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }

    private Chunk GetRandomChunk()
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < ChunkPrefabs.Length; i++)
        {
            chances.Add(ChunkPrefabs[i].ChanceFromDistance.Evaluate(Player.transform.position.z));
        }

        float value = Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum)
            {
                return ChunkPrefabs[i];
            }
        }

        return ChunkPrefabs[ChunkPrefabs.Length-1];
    }
}