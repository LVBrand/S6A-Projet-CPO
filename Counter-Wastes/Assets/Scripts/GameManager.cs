using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    public TowerButton ClickedButton
    {
        get; set;
    }


    private int currency;


    [SerializeField]
    private Text currencyTxt;


    public ObjectPool Pool { get; set; }


    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            this.currency = value;
            this.currencyTxt.text = "<color=lime>$</color>" + value.ToString();
        }
    }


    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Currency = 50;
    }


    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }


    public void PickTower(TowerButton towerButton)
    {
        if (Currency >= towerButton.Price)
        {
            //Stores the clicked button
            this.ClickedButton = towerButton;
            //active l'icone de Hover
            Hover.Instance.Activate(towerButton.Sprite);
        }

    }


    public void BuyTower()
    {
        if (Currency >= ClickedButton.Price)
        {
            Currency -= ClickedButton.Price;
            Hover.Instance.Deactivate();
        }

    }


    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }  
    }


    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }


    private IEnumerator SpawnWave()
    {
        int monsterIndex = Random.Range(0, 2);

        string type = string.Empty;

        switch (monsterIndex)
        {
            case 0:
                type = "monster_gaben";
                break;
            case 1:
                type = "monster_knight";
                break;
            default:
                break;

        }

        Monster monster = Pool.GetObject(type).GetComponent<Monster>();

        monster.Spawn();

        yield return new WaitForSeconds(0.5f);
    }

}
