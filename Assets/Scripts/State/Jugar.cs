using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugar : Humano
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _playZone;
    private GameObject _comedor, _dormitorio, _banno;
    private void Awake()
    {
        typestate = TypeState.Jugar;
        LocadComponent();
    }
    public override void LocadComponent()
    {
        base.LocadComponent();
        _playerMovement = GetComponent<PlayerMovement>();

        if (_playZone == null) _playZone = GameObject.FindWithTag("Play");
        _comedor = GameObject.FindWithTag("Repas");
        _dormitorio = GameObject.FindWithTag("Sleep");
        _banno = GameObject.FindWithTag("WC");

        if (_playZone == null) Debug.LogError("No se encontró PlayZone");
    }

    public override void Enter()
    {
        if (_playZone != null)
        {
            _playerMovement.MoveToTarget(_playZone);
        }
        else
        {
            Debug.LogWarning("PlayZone no asignado");
        }
    }

    public override void Execute()
    {
        _DataAgent.DiscountEnergy();
        _DataAgent.DiscountSleep();
        _DataAgent.DiscountWC();

        // Prioridad de transiciones (Energy > Sleep > WC)
        if (_DataAgent.Energy.value < 0.25f)
        {
            _StateMachine.ChangeState(TypeState.Comer);
        }
        else if (_DataAgent.Sleep.value < 0.25f)
        {
            _StateMachine.ChangeState(TypeState.Dormir);
        }
        else if (_DataAgent.WC.value < 0.25f)
        {
            _StateMachine.ChangeState(TypeState.Banno);
        }

        base.Execute();
    }
}