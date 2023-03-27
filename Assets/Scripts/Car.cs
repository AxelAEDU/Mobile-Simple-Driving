using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f; 
    [SerializeField] private float speedGainPerSecond = 0.2f;
    [SerializeField] private float turnSpeed = 200f;

    private int steerValue;

    void Update()
    {
        speed += speedGainPerSecond * Time.deltaTime;

        transform.Rotate(0,steerValue * turnSpeed * Time.deltaTime,0);

        transform.Translate(Vector3.forward * speed *  Time.deltaTime);
    }

    public void Steer(int Value)
    {
        steerValue = Value;
    }
}
