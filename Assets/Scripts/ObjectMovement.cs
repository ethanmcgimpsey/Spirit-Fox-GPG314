using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float initialSpeed = 5f;
    public float speedIncreaseRate = 0.1f;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
        currentSpeed += speedIncreaseRate * Time.deltaTime;
    }
}
