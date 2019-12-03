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
}
