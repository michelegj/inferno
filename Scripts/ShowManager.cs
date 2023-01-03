using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowManager : MonoBehaviour
{
    [SerializeField] List<GameObject> shows;

    void Start()
    {
        foreach (var item in shows)
            item.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        foreach (var item in shows)
            item.SetActive(true);
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        foreach (var item in shows)
            item.SetActive(true);
    }
}
