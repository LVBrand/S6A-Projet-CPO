using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private Text waveText;

    [SerializeField]
    private Text currencyTxt;

    [SerializeField]
    private GameObject waveButton;

    [SerializeField]
    private Text texteVie;

    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private GameObject inGameMenu;

    [SerializeField]
    private GameObject optionsMenu;

    //current selected tower
    private Tower selectedTower;

    public ObjectPool Pool { get; set; }

    private int remainingMonsters;

    private int wave = 0;


    private bool gameOver = false;

    private int vies;

    public int Vies
    {
        get
        {
            return vies;
        }
        set
        {
            this.vies = value;

            if (vies <= 0)
            {
                this.vies = 0;
                GameOver();
            }
            texteVie.text = vies.ToString();
        }
    }

    private int currency;

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

    public TowerButton ClickedButton
    {
        get; set;
    }

    private List<Monster> activeMonsters = new List<Monster>();     //un liste contenant les monstres actifs

    public List<Monster> ActiveMonsters // ça permet de savoir si ya un monster dans la lane
    {
        get
        {
            return activeMonsters;
        }
    }
     


    public bool WaveActive
    {
        get
        {
            return activeMonsters.Count > 0 && remainingMonsters==0;
        }
    } 

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Vies = 3;
        Currency = 2000;
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

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
        selectedTower.Select();
    }


    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Hover.Instance.IsVisible)
            {
                ShowIngameMenu();
            }
            

            if (Hover.Instance.IsVisible)
            {
                DropTower();
            }
        }  
    }


    public void StartWave()
    {
        wave++;

        waveText.text = string.Format("Wave: <color=lime>{0}</color>", wave);
        remainingMonsters += 10;
        StartCoroutine(SpawnWave());

        waveButton.SetActive(false);

    }

    

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < wave+3; i++)
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

            activeMonsters.Add(monster);

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        remainingMonsters -= 1;

        if (!WaveActive && !gameOver && remainingMonsters==0)
        {
            waveButton.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


    public void ShowIngameMenu()
    {
        if (optionsMenu.activeSelf)
        {
            ShowMain();
        }
        else
        {
            inGameMenu.SetActive(!inGameMenu.activeSelf);
            if (!inGameMenu.activeSelf)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }

    }

    private void DropTower()
    {
        ClickedButton = null;
        Hover.Instance.Deactivate();
    }

    public void ShowOptions()
    {
        inGameMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ShowMain()
    {
        inGameMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

}
