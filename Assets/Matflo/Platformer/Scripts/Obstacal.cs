using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacal : MonoBehaviour
{
    public ObstacalsName ObstacalName;
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GameLose();
            Destroy(collider.gameObject);
        }
    }
}
