using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{ 
    public int waiting = 3;
    [SerializeField] List<GameObject> portals;
    [SerializeField] List<GameObject> portalWall;

    [Header("Multiple Portals")]
    [SerializeField] List<string> scenes;

    void Awake()
    {
        foreach (var item in portals)
            item.SetActive(false);   

        foreach (var item in portalWall)
            item.SetActive(false);  
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Portal"))
            StartCoroutine(waiter());
        for (int i = 0; i < portalWall.Count; i++)
            if (collision.gameObject == portalWall[i])
            {
                SceneManager.LoadScene(scenes[i]);
                break;
            }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Portal"))
            StartCoroutine(waiter());
        for (int i = 0; i < portalWall.Count; i++)
            if (collision.gameObject == portalWall[i])
            {
                SceneManager.LoadScene(scenes[i]);
                break;
            }
            
    }

    IEnumerator waiter()
    {    
        yield return new WaitForSeconds(waiting); 
        foreach (var item in portals)
            item.SetActive(true);

        foreach (var item in portalWall)
            item.SetActive(true);  
    }
}
