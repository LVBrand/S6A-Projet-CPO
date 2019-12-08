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

    private float life;

    private float speed;

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
    }

    public void Spawn()
    {
        int nbLane = Random.Range(0, 6);
        transform.position = LevelManager.Instance.spawn[nbLane].transform.position;
        life = maxLife;
        this.speed = maxSpeed;
        transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
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
        if (otherObject.tag == "heavy_tower")
        {
            speed = 0;
            //otherObject.gameObject.GetComponent<Tower>().Life -= damage;
            //InvokeRepeating("attackTower", 0.0f, attackSpeed);
            StartCoroutine(attackTower(otherObject.gameObject));
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
            StartCoroutine(tower.GetComponent<Tower>().damageFlash());
            tower.GetComponent<Tower>().Life -= damage;

            if (tower.GetComponent<Tower>().Life == 0)
            {
                tower.transform.parent.GetComponent<TileScript>().IsEmpty = true;
                tower.transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
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
    }
}
