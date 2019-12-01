using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{

    public Point GridPosition
    {
        get; private set;
    }

    //surligner les tiles occupées par une entité
    private Color32 fullColor = new Color32(255, 142, 142, 255);

    private Color32 emptyColor = new Color32(96, 255, 90, 255);

    public bool IsEmpty
    {
        get; private set;
    }

    private SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Point gridPos, Vector2 worldPos, Transform parent)
    {
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);

        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    // chopper la position du curseur
    private void OnMouseOver()
    {
        ColorTile(fullColor);

        // vérifie si la souris n'est pas sur un bouton, pour ne pas placer une tour sur un bouton
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton != null)
        {
            if (IsEmpty)
            {
                ColorTile(emptyColor);
            }
            if (!IsEmpty)
            {
                ColorTile(fullColor);
            }

            //si elle n'est pas vide, on place une tour, sinon, non
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
    }

    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {
        // on instancie les tours
        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedButton.TowerPrefab, transform.position, Quaternion.identity);

        // on fait en sorte que les sprites se chevauchent suivant l'ordre de Y (relief)
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        // fait en sorte que quand on place une tour, elle est instanciée comme objet fille du tile où elle est placée
        tower.transform.SetParent(transform);

        ColorTile(Color.white);

        IsEmpty = false;

        GameManager.Instance.BuyTower();
    }

    private void ColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }
}
