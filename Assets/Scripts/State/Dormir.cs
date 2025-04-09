using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dormir : Humano
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _dormitorio;

    private void Awake()
    {
        typestate = TypeState.Dormir;
        LocadComponent();
    }

    public override void LocadComponent()
    {
        base.LocadComponent();
        _playerMovement = GetComponent<PlayerMovement>();
        if (_dormitorio == null) _dormitorio = GameObject.FindWithTag("Sleep");
    }

    public override void Enter()
    {
        if (_dormitorio != null)
        {
            _playerMovement.MoveToTarget(_dormitorio);
            _DataAgent.LoadSleep(); // Inicia recarga de Sleep
        }
        else
        {
            Debug.LogError("No se encontró el dormitorio (tag 'Sleep')");
        }
    }

    public override void Execute()
    {
        // Sleep se recupera (LoadSleep)
        _DataAgent.DiscountEnergy(); // Energy baja
        _DataAgent.DiscountWC();     // WC baja

        // Solo verifica si Sleep está lleno (ignora otras energías)
        if (_DataAgent.Sleep.value >= _DataAgent.Sleep.valueMax)
        {
            _StateMachine.ChangeState(TypeState.Jugar); // Siempre vuelve a Jugar primero
        }

        base.Execute();
    }

    public override void Exit()
    {
        // Opcional: Resetear variables si es necesario
    }
}