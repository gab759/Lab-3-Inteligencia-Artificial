using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Hace que el objeto mire hacia la c�mara manteniendo su orientaci�n "frontal"
            Vector3 direction = transform.position - mainCamera.transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}