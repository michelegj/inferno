using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyManager : MonoBehaviour
{
    [Header("Spin Mov")]
    public GameObject[] spinObjects;
    public float spinSpeed = 1.0f;
    public Vector3 spinAxis = Vector3.up;
    public bool spinEnabled = false;

    [Header("Color Mov")]
    public GameObject[] colorObjects;
    public float colorChangeInterval = 0.32f; 
    private float timer = 0.0f; 
    public bool colorEnabled = false;

    [Header("Random Mov")]
    public GameObject[] randomObjects;
    public float moveDistance = 1.0f;
    public bool randomEnabled = false;

    [Header("Floating Mov")]
    public GameObject[] floatObjects;
    public float floatSpinSpeed = 10.0f;
    public float floatSpinMov = 0.1f;
    public bool floatEnabled = false;

    void Update()
    {
        if (randomEnabled)
            RandomMovement();
        
        if (spinEnabled)
            SpinMovement();
        
        if (colorEnabled)
            timer += Time.deltaTime;
            if (timer >= colorChangeInterval)
            {
                timer = 0.0f;
                ChangeColor();
            }
        
        if (floatEnabled)
            FloatingMovement();
        
    }

    private void RandomMovement()
    {
        for (int i = 0; i < randomObjects.Length; i++)
        {
            GameObject go = randomObjects[i];
            Transform[] children = go.GetComponentsInChildren<Transform>();
            for (int j = 0; j < children.Length; j++)
            {
                Transform child = children[j];
                if (child != go.transform)
                {
                    Vector3 spinDirection = Random.onUnitSphere; 
                    Vector3 moveDirection = Random.onUnitSphere; 
                    child.Rotate(spinDirection * Time.deltaTime);
                    child.position += moveDirection * moveDistance * Time.deltaTime;
                }
            }
        }
    }



    private void SpinMovement()
    {
        for (int i = 0; i < spinObjects.Length; i++)
        {
            GameObject go = spinObjects[i];
            Transform[] children = go.GetComponentsInChildren<Transform>();
            for (int j = 0; j < children.Length; j++)
            {
                Transform child = children[j];
                if (child != go.transform)
                {
                    child.Rotate(spinAxis, spinSpeed * Time.deltaTime);
                }
            }
        }
    }



    public void ChangeColor()
    {
        for (int i = 0; i < colorObjects.Length; i++)
        {
            GameObject go = colorObjects[i];
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                float hue = Random.Range(0.0f, 1.0f);
                float saturation = 1.0f;
                float value = 1.0f;
                Color color = Color.HSVToRGB(hue, saturation, value);
                renderer.material.color = color;
            }
        }
    }

    private void FloatingMovement()
    {
        float frequency = 1.0f; 
        Vector3 spinAxis = Vector3.up; 
        for (int i = 0; i < floatObjects.Length; i++)
        {
            GameObject go = floatObjects[i];
            Transform[] children = go.GetComponentsInChildren<Transform>();
            for (int j = 0; j < children.Length; j++)
            {
                Transform child = children[j];
                if (child != go.transform)
                {
                    Vector3 position = child.position;
                    position.y += Mathf.Sin(Time.time * frequency) * floatSpinMov;
                    position.x += Mathf.Cos(Time.time * frequency) * floatSpinMov;
                    child.position = position;
                    child.Rotate(spinAxis, floatSpinSpeed * Time.deltaTime);
                }
            }
        }
    }


}
