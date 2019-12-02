using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //temporaire, on va le remplacer plus tard
    [SerializeField]
    private GameObject towerPrefab;
    public ObjectPool Pool { get; set; }

    [SerializeField]
    private GameObject startButton;         //Le bouton de départ du lvl

    private List<Monster> activeMonsters = new List<Monster>();     //un liste contenant les monstres actifs

    public bool WaveActive
    {
        get
        {
            return activeMonsters.Count > 0;
        }
    }

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

        startButton.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < 10; i++)
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

            activeMonsters.Add(monster);

            monster.Spawn();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void RemoveMonster(Monster m)
    {
        activeMonsters.Remove(m);
        if (!WaveActive)
        {
            startButton.SetActive(true);
        }
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
