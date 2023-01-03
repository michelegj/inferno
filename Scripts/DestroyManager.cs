using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> destroys;
    void OnTriggerEnter(Collider collision)
    {
        foreach (var item in destroys)
            Destroy(item); 
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        foreach (var item in destroys)
            Destroy(item); 
    }
}
