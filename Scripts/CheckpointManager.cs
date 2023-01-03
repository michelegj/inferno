using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    Vector3 spawnPoint;
    
    void Start()
    {
        spawnPoint = transform.position;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Border"))
        {
            transform.position = spawnPoint;
        }
        else if (collision.gameObject.tag.Equals("Checkpoint"))
        {
            spawnPoint = transform.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Border"))
        {
            transform.position = spawnPoint;
        }
        else if (collision.gameObject.tag.Equals("Checkpoint"))
        {
            spawnPoint = transform.position;
        }
    }
}
