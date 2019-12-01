using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{

    public Point GridPosition { get; private set; }

    //avoir le centre d'un tile
    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
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

    public void Setup(Point gridPos, Vector2 worldPos, Transform parent)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);

        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    // chopper la position du curseur
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        // on instancie les tours
        GameObject tower = (GameObject)Instantiate(GameManager.Instance.TowerPrefab, transform.position, Quaternion.identity);

        // on fait en sorte que les sprites se chevauchent suivant l'ordre de Y (relief)
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
    
        // fait en sorte que quand on place une tour, elle est instanciée comme objet fille du tile où elle est placée
        tower.transform.SetParent(transform);
    }
}
