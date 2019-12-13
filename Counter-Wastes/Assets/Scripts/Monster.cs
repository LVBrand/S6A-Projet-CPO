using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float maxLife;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private int bounty;

    private float life;

    private float speed;

    public AnimationCurve yCurve, lCurve, hCurve;
    private float timeElapsed = 0;
    private float YstartPosition;
    private float LstartScale;
    private float HstartScale;

    public float Life
    {
        get
        {
            return life;
        }
        set
        {
            this.life = value;
            if (life <= 0)
            {
                life = 0;
                Release();
            }
        }
    }

    public Point GridPosition { get; set; }


    private void Update()
    {
        Move();
        animationUpdate();
    }

    private void animationUpdate()
    {
        Debug.Log(timeElapsed);
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= yCurve[yCurve.length - 1].time) timeElapsed = 0;
        transform.position = new Vector2(transform.position.x, YstartPosition + 0.3f*yCurve.Evaluate(timeElapsed));
        transform.localScale = new Vector2(LstartScale + 1f * lCurve.Evaluate(timeElapsed), HstartScale + 1f * hCurve.Evaluate(timeElapsed)); ;
    }
    public void Spawn()
    {
        timeElapsed = 0;
        int nbLane = Random.Range(0, 6);
        transform.position = LevelManager.Instance.spawn[nbLane].transform.position;
        life = maxLife;
        this.speed = maxSpeed;
        transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        YstartPosition = transform.position.y;
        LstartScale = transform.localScale.x;
        HstartScale = transform.localScale.y;
    }

    private void Move()
    {
        Vector2 destination = new Vector2(-5, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if ((Vector2)transform.position == destination)
        {
            Release();
            GameManager.Instance.Vies--;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "heavy_tower" || otherObject.tag == "sun_tower" || otherObject.tag == "scout_tower" )
        {
            speed = 0;
            //otherObject.gameObject.GetComponent<Tower>().Life -= damage;
            //InvokeRepeating("attackTower", 0.0f, attackSpeed);
            if (this)
            {
                StartCoroutine(attackTower(otherObject.gameObject));
            }
        }
        if (otherObject.tag == "projectile")
        {
            StartCoroutine(damageFlash());
            Life-=otherObject.gameObject.GetComponent<Projectile>().Damage;
            GameManager.Instance.Pool.ReleaseObject(otherObject.gameObject);
        }
    }


    public IEnumerator attackTower(GameObject tower)
    {
        for (int nbDeCoupsNecessaires = (int)(tower.GetComponent<Tower>().Life / damage) + 1; nbDeCoupsNecessaires >= 0; nbDeCoupsNecessaires--)
        {

            if (!tower)
            {
                speed = maxSpeed;
                break;
            }
            tower.GetComponent<Tower>().flickering();
            tower.GetComponent<Tower>().Life -= damage;

            if (tower.GetComponent<Tower>().Life == 0)
            {
                if (tower.tag == "sun_tower")
                {
                    tower.transform.parent.GetComponent<TileScript>().IsEmpty = true;
                    GameManager.Instance.RemoveSunTower(tower.GetComponent<Tower>());
                    Destroy(tower);
                    speed = maxSpeed;
                    break;
                }
                tower.transform.parent.GetComponent<TileScript>().IsEmpty = true;
                Destroy(tower);
                speed = maxSpeed;
                break;
            }
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    IEnumerator damageFlash()
    {
        for(int i=0; i<4;i++)
        {
            switch (i)
            {
                case 0:
                    transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                    break;
                case 1:
                    transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                    break;
                case 2:
                    transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                    break;
                case 3:
                    transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                    break;
            }
            yield return new WaitForSeconds(0.10f);
        }
    }

    private void Release()
    {

        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
        GameManager.Instance.Currency += this.bounty;
    }
}
