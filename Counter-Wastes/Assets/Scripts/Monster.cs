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
            Life-=otherObject.gameObject.GetComponent<Projectile>().Damage;
            GameManager.Instance.Pool.ReleaseObject(otherObject.gameObject);
        }
    }


    IEnumerator attackTower(GameObject tower)
    {
        for (int nbDeCoupsNecessaires = (int)(tower.GetComponent<Tower>().Life / damage) + 1; nbDeCoupsNecessaires >= 0; nbDeCoupsNecessaires--)
        {
            if (!tower) { break; }
            tower.GetComponent<Tower>().Life -= damage;
            if (tower.GetComponent<Tower>().Life == 0)
            {
                Destroy(tower);
                speed = maxSpeed;
                break;
            }
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private void Release()
    {
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }
}
