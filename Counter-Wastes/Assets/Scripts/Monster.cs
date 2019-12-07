using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField]
    private float speed;

    public Point GridPosition { get; set; }

    public bool monsterInLane()
    {
        foreach (Monster m in GameManager.Instance.ActiveMonsters)
        {
            if (m.transform.position.y == transform.position.y)
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        Move();
    }

    public void Spawn()
    {
        int nbLane = Random.Range(0, 6);
        transform.position = LevelManager.Instance.spawn[nbLane].transform.position;
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
        if (otherObject.tag == "tower")
        {
            speed = 0;
            //Rajouter les dégats fait à la tour.
        }
    }

    private void Release()
    {
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }


}
