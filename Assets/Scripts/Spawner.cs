using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] tetrominoes;

    private void Start()
    {
        SpawnTetrominoe();
    }

    public void SpawnTetrominoe()
    {
        Instantiate(tetrominoes[Random.Range(0, tetrominoes.Length)],transform);
    }

}
