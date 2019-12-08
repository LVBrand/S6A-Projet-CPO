using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;

    [SerializeField]
    private float reloadTime;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("SpawnProjectile", 0.0f, reloadTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool monsterInLane()
    {
        foreach (Monster m in GameManager.Instance.ActiveMonsters)
        {
            if (m.transform.position.y+0.5f == transform.position.y && m.transform.position.x > transform.position.x)
            {
                return true;
            }
        }
        return false;
    }


    private void SpawnProjectile()
    {
        if (!monsterInLane()) { return; }
        Projectile projectile = GameManager.Instance.Pool.GetObject("projectile").GetComponent<Projectile>();
        projectile.transform.position = transform.position + new Vector3(0.5f, -0.5f, 0);
    }

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
        
    }

}
