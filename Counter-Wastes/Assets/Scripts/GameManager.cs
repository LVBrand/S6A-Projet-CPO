using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //temporaire, on va le remplacer plus tard
    [SerializeField]
    private GameObject towerPrefab;
    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {

        //int monsterIndex = Random.Range(0, 1); //On changera cette ligne lorsqu'on aura plus d'un ennemi
        int monsterIndex = 0;
        string type = string.Empty;

        switch (monsterIndex)   //Rajouter autant de case qu'il y a d'ennemi
        {
            case 0:
                type = "Gaben";
                break;
        }

        Monster monster = Pool.GetObject(type).GetComponent<Monster>();
        monster.Spawn();
        yield return new WaitForSeconds(2.5f);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
