using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class ChunksPlacer : MonoBehaviour
{
    public Transform Player;
    public Chunk[] ChunkPrefabs;
    public Chunk FirstChunk;

    [SerializeField] private List<Chunk> spawnedChunks = new List<Chunk>();
    public int LastEndPoint = 0;

    private void Start()
    {
        spawnedChunks.Add(FirstChunk);
    }

    private void Update()
    {
        if (Player.position.z > spawnedChunks[spawnedChunks.Count - 1].End1.position.z - 50 && LastEndPoint != 2)
        {
            LastEndPoint = 1;
            SpawnChunk(spawnedChunks[spawnedChunks.Count - 1].End1, 1);
            Debug.Log("Проверка на срабатывание End1");
        }
        if (Player.position.z < spawnedChunks[spawnedChunks.Count - 1].End2.position.z + 50 && LastEndPoint != 1)
        {
            LastEndPoint = 2;
            SpawnChunk(spawnedChunks[spawnedChunks.Count - 1].End2, 2);
            Debug.Log("Проверка на срабатывание End2");
        }
        if (Player.position.x > spawnedChunks[spawnedChunks.Count - 1].End3.position.x - 50 && LastEndPoint != 4)
        {
            LastEndPoint = 3;
            SpawnChunk(spawnedChunks[spawnedChunks.Count - 1].End3, 3);
            Debug.Log("Проверка на срабатывание End3");
        }
        if (Player.position.x < spawnedChunks[spawnedChunks.Count - 1].End4.position.x + 50 && LastEndPoint != 3)
        {
            LastEndPoint = 4;
            SpawnChunk(spawnedChunks[spawnedChunks.Count - 1].End4, 4);
            Debug.Log("Проверка на срабатывание End4");
        }
    }

    private void SpawnChunk(Transform End, int EndNumber)
    {
        if (EndNumber == 1 && CanWeSpawn(End, 1))
        {
            Chunk newChunk = Instantiate(GetRandomChunk());
            newChunk.End2.gameObject.SetActive(false);
            newChunk.ContainerB_E.SetActive(true);
            newChunk.transform.position = End.position - newChunk.Begin1.localPosition;
            spawnedChunks.Add(newChunk);
            Debug.Log("Спавн чанка из-за триггера  " + End);
        }
        if (EndNumber == 2 && CanWeSpawn(End, 2))
        {
            Chunk newChunk = Instantiate(GetRandomChunk());
            newChunk.End1.gameObject.SetActive(false);
            newChunk.ContainerB_E.SetActive(true);
            newChunk.transform.position = End.position - newChunk.Begin2.localPosition;
            spawnedChunks.Add(newChunk);
            Debug.Log("Спавн чанка из-за триггера  " + End);
        }
        if (EndNumber == 3 && CanWeSpawn(End, 3))
        {
            Chunk newChunk = Instantiate(GetRandomChunk());
            newChunk.End4.gameObject.SetActive(false);
            newChunk.ContainerB_E.SetActive(true);
            newChunk.transform.position = End.position - newChunk.Begin3.localPosition;
            spawnedChunks.Add(newChunk);
            Debug.Log("Спавн чанка из-за триггера  " + End);
        }
        if (EndNumber == 4 && CanWeSpawn(End, 4))
        {
            Chunk newChunk = Instantiate(GetRandomChunk());
            newChunk.End3.gameObject.SetActive(false); //Destroy(newChunk.End3.gameObject);
            newChunk.ContainerB_E.SetActive(true);
            newChunk.transform.position = End.position - newChunk.Begin4.localPosition;
            spawnedChunks.Add(newChunk);
            Debug.Log("Спавн чанка из-за триггера  " + End);
        }




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
            chances.Add(ChunkPrefabs[i].ChanceFromDistance.Evaluate(spawnedChunks.Count));
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

        return ChunkPrefabs[ChunkPrefabs.Length - 1];
    }

    private bool CanWeSpawn(Transform End, int SideNumber)
    {
        Vector3 CheckSide;        

        Vector3 offsetTop = End.parent.parent.position + new Vector3(0,0,250);        
        Vector3 offsetBot = End.parent.parent.position + new Vector3(0, 0, -250);
        Vector3 offsetRight = End.parent.parent.position + new Vector3(250, 0, 0);
        Vector3 offsetLeft = End.parent.parent.position + new Vector3(-250, 0, 0);

        if (SideNumber == 1)
        {
            CheckSide = offsetTop;
        }
        else if (SideNumber == 2)
        {
            CheckSide = offsetBot;
        }
        else if (SideNumber == 3)
        {
            CheckSide = offsetLeft;
        }else
        {
            CheckSide = offsetBot;
        }

        Debug.Log("Позиция которую мы проверяем на наличие там чанка: " + CheckSide);

        for (int i = 0; i < spawnedChunks.Count; i++)
        {
            Debug.Log("Итерация CanWeSpawn " + i);
            if (spawnedChunks[i].transform.position == CheckSide)
            {
                Debug.Log("Итерация CanWeSpawn " + i + " -- Найдено совпадение, возвращаем false");
                return false; 
            }            
        }
        Debug.Log("Проверка CanWeSpawn пройдена, разрешаем спавн ");
        return true;
        
        
    }
}