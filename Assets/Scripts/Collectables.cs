using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 2f;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * rotSpeed, 0f));
    }
}
