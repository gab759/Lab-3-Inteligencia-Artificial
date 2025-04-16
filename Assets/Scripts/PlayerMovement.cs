using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 0.5f;

    private NavMeshAgent _navMeshAgent;
    private GameObject _currentTarget;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        ConfigureNavMeshAgent();
    }

    private void ConfigureNavMeshAgent()
    {
        _navMeshAgent.angularSpeed = _rotationSpeed * 100; // Ajuste para rotación suave
        _navMeshAgent.stoppingDistance = _stoppingDistance;
        _navMeshAgent.updateRotation = true; // NavMesh controla la rotación
        _navMeshAgent.updateUpAxis = false; // Ignora cambios en el eje Y
    }

    public void MoveToTarget(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target es null en MoveToTarget");
            return;
        }

        _currentTarget = target;

        // Configura el destino manteniendo la posición Y actual
        Vector3 targetPosition = new Vector3(
            target.transform.position.x,
            transform.position.y,
            target.transform.position.z
        );

        _navMeshAgent.SetDestination(targetPosition);
    }

    private void Update()
    {
        // Verifica si llegó al destino
        if (_currentTarget != null &&
            !_navMeshAgent.pathPending &&
            _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            Debug.Log($"Llegó a {_currentTarget.tag}");
            _currentTarget = null;
        }
    }
}