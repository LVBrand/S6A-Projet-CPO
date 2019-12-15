using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int price;

    [SerializeField]
    private Text priceTxt;

    [SerializeField]
    private bool buttonRemove;

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }
    }

    private void Start()
    {
        if(!buttonRemove)
            priceTxt.text = price + "$";
        else
        {
            priceTxt.text = "Delete Tower";
        }
    }
}
