using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;

    [SerializeField]
    private float reloadTime;

    [SerializeField]
    private float maxLife;


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
            }
        }
    }

    private float life;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        life = maxLife;
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

    public IEnumerator damageFlash()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!this) { break; }
            switch (i)
            {
                case 0:
                    transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                    Debug.Log("a");
                    break;
                case 1:
                    transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                    break;
                case 2:
                    transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                    Debug.Log("b");
                    break;
                case 3:
                    transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                    break;
            }
            yield return new WaitForSeconds(0.10f);
        }
    }


    private void SpawnProjectile()
    {
        if (!monsterInLane()) { return; }
        if (this.tag == "heavy_tower")
        {
            Projectile projectile = GameManager.Instance.Pool.GetObject("Sandvich").GetComponent<Projectile>();
            projectile.transform.position = transform.position + new Vector3(0.5f, -0.5f, 0);
            transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }
        else if (this.tag != "heavy_tower")
        {
            Projectile projectile = GameManager.Instance.Pool.GetObject("projectile").GetComponent<Projectile>();
            projectile.transform.position = transform.position + new Vector3(0.5f, -0.5f, 0);
            transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }
        else if (this.tag == "sun_tower")
        {
            return;
        }

    }

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
        
    }

    

}
