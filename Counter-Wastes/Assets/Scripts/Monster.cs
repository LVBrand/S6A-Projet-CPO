using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public void Spawn()
    {
        int nbLane = Random.Range(0, 6);
        transform.position = LevelManager.Instance.spawn[nbLane].transform.position;
    }
}
