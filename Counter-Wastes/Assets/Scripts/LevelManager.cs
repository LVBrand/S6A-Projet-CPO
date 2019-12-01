using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class LevelManager : Singleton<LevelManager>
{

    [SerializeField]
    private GameObject[] tilePrefabs;

    /*//création des points de spawns
    private Point spawnPoint0, spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4, spawnPoint5;*/

    //créations d'un préfab de points de spawn
    [SerializeField]
    private GameObject spawnPrefab;

    [SerializeField]
    private Transform map;

    //création d'un dictionnaire pour chopper la position des tiles
    public Dictionary<Point, TileScript> Tiles { get; set; }
    
    // calculer la taille des tiles, on l'utilise pour bien positionner les tiles 
    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }



    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }



    // Update is called once per frame
    void Update()
    {
        
    }



    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        // instanciation temporaire de la tilemap, on utilise un documen texte pour le remplacer
        string[] mapData = ReadLevelText();

        // Calcul de x (map)
        int mapSizeX = mapData[0].ToCharArray().Length;

        // Calcul de y (map)
        int mapSizeY = mapData.Length;

        Vector2 maxTile = Vector2.zero;

        //On calcule le point de départ du la map, le point en haut à gauche en gros
        Vector2 worldStart = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));


        for (int y = 0; y < mapSizeY; y++) // positions en y (8)
        {
            //on prend tous les char de mapdata et on en fait un tableau
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapSizeX; x ++) // positions en x (15)
            {
                // Placer le tile dans le monde
                PlaceTile(newTiles[x].ToString(),x,y,worldStart);
            }
        }

        maxTile = Tiles[new Point(mapSizeX - 1, mapSizeY - 1)].transform.position;

        SpawnPoints();
    }



    private void PlaceTile(string tileType, int x, int y, Vector2 worldStart)
    {
        // par exemple : "1" devient 1
        int tileIndex = int.Parse(tileType);

        // Création d'un nouveau tile et référencement de ce dernier dans la variable newTile
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        // Utilisation de la variable newTile pour changer la position du tile
        newTile.Setup(new Point(x, y), new Vector2(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y)),map);

       /* //On ajoute chaque newTile dans le dictionnaire Tiles
        Tiles.Add(new Point(x, y), newTile); */

    }

    
    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        //on coupe par le caractère "-" dans notre fichier texte
        return data.Split('-');

    }

    
    private void SpawnPoints()
    {
        //on va générer les spawns
        List<Point> spawnPoint = new List<Point>(6);
        for(int i = 1; i < 7; i++)
        {
            spawnPoint.Add(new Point(14, i));
        }

        /*spawnPoint0 = new Point(14, 1);
        spawnPoint1 = new Point(14, 2);
        spawnPoint2 = new Point(14, 3);
        spawnPoint3 = new Point(14, 4);
        spawnPoint4 = new Point(14, 5);
        spawnPoint5 = new Point(14, 6);*/

        //on instancie tous les spawns centrés sur des tiles bien précis
        foreach (Point sPoint in spawnPoint)
        {
            GameObject tmp = (GameObject)Instantiate(spawnPrefab, Tiles[sPoint].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        }

        //on instancie tous les spawns centrés sur des tiles bien précis
        /*Instantiate(spawnPrefab, Tiles[spawnPoint0].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(spawnPrefab, Tiles[spawnPoint1].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(spawnPrefab, Tiles[spawnPoint2].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(spawnPrefab, Tiles[spawnPoint3].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(spawnPrefab, Tiles[spawnPoint4].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        Instantiate(spawnPrefab, Tiles[spawnPoint5].GetComponent<TileScript>().WorldPosition, Quaternion.identity);*/
    }
}
