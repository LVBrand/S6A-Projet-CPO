using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float damage;

    public float Damage { get { return damage;} }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 destination = new Vector2(7f, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if ((Vector2)transform.position == destination)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

}
