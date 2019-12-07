using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tower : Singleton<Tower>
{
    private SpriteRenderer mySpriteRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    public IEnumerator GenerateCurrency()
    {
        GameManager.Instance.Currency += 5;

        yield return new WaitForSeconds(0.5f);
    }

    public void Solaire()
    {
        if (GameManager.Instance.WaveActive)
        {
            StartCoroutine(GenerateCurrency());
        } 
    }


}
