using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheToy : Humano
{
    [Header("Configuración")]
    [SerializeField] private float _destroyDistance = 1.5f;

    private IAEye _toyDetector;
    private Health _currentToy;
    private PlayerMovement _playerMovement;
    private TypeState _previousState;

    private void Awake()
    {
        typestate = TypeState.FollowToy;
        LocadComponent();
    }

    public override void LocadComponent()
    {
        base.LocadComponent();
        _toyDetector = GetComponent<IAEye>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public override void Enter()
    {
        // Guardar estado anterior solo si no estamos ya en FollowToy
        if (_StateMachine.currentState.typestate != TypeState.FollowToy)
        {
            _previousState = _StateMachine.currentState.typestate;
        }

        if (TextState != null)
            TextState.text = "Follow Toy";

        if (_toyDetector != null && _toyDetector.dataView.InSight && _toyDetector.ViewToy != null)
        {
            _currentToy = _toyDetector.ViewToy;
            _playerMovement.FollowToy(_currentToy.transform);
        }
        else
        {
            ReturnToPreviousState();
        }
    }

    public override void Exit()
    {
        CleanUp();
    }

    public override void Execute()
    {
        if (_currentToy == null || !_toyDetector.dataView.InSight)
        {
            ReturnToPreviousState();
            return;
        }

        if (Vector3.Distance(transform.position, _currentToy.transform.position) <= _destroyDistance)
        {
            Destroy(_currentToy.gameObject);
            ReturnToPreviousState();
        }
    }

    private void ReturnToPreviousState()
    {
        CleanUp();
        _StateMachine.ChangeState(_previousState);
    }

    private void CleanUp()
    {
        if (_currentToy != null)
        {
            // Opcional: Resetear la referencia en IAEye
            if (_toyDetector != null && _toyDetector.ViewToy == _currentToy)
            {
                _toyDetector.ViewToy = null;
                _toyDetector.dataView.InSight = false;
            }
            _currentToy = null;
        }

        if (TextState != null)
            TextState.text = _previousState.ToString();
    }
}