using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _stoppingDistance = 0.5f;

    private GameObject _currentTarget;
    private bool _isMoving = false;
    private float _fixedYPosition; // Mantendrá la posición Y inicial

    private void Start()
    {
        _fixedYPosition = transform.position.y; // Guarda la posición Y inicial
    }

    public void MoveToTarget(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target es null en MoveToTarget");
            return;
        }

        _currentTarget = target;
        _isMoving = true;
    }

    private void Update()
    {
        if (!_isMoving || _currentTarget == null) return;

        // Calcula dirección ignorando el eje Y
        Vector3 targetPosition = new Vector3(
            _currentTarget.transform.position.x,
            _fixedYPosition, // Usa la posición Y fija
            _currentTarget.transform.position.z
        );

        Vector3 direction = (targetPosition - transform.position).normalized;

        // Movimiento en XZ
        transform.position += new Vector3(direction.x, 0, direction.z) * _speed * Time.deltaTime;

        // Rotación solo si hay movimiento
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }

        // Verifica distancia de llegada (ignorando Y)
        float horizontalDistance = Vector2.Distance(
            new Vector2(transform.position.x, transform.position.z),
            new Vector2(targetPosition.x, targetPosition.z)
        );

        if (horizontalDistance <= _stoppingDistance)
        {
            _isMoving = false;
            Debug.Log($"Llegó a {_currentTarget.tag}");
        }
    }
}