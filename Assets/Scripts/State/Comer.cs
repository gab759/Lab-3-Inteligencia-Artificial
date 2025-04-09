using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comer : Humano
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _comedor;

    private void Awake()
    {
        typestate = TypeState.Comer;
        LocadComponent();
    }

    public override void LocadComponent()
    {
        base.LocadComponent();
        _playerMovement = GetComponent<PlayerMovement>();
        if (_comedor == null) _comedor = GameObject.FindWithTag("Repas");
    }

    public override void Enter()
    {
        if (_comedor != null)
        {
            _playerMovement.MoveToTarget(_comedor);
            _DataAgent.LoadEnergy(); // Inicia recarga de Energy
        }
    }

    public override void Execute()
    {
        // Energy se recupera automáticamente (LoadEnergy)
        _DataAgent.DiscountSleep(); // Sleep sigue bajando
        _DataAgent.DiscountWC();    // WC sigue bajando

        // Solo vuelve a Jugar cuando Energy esté llena
        if (_DataAgent.Energy.value >= _DataAgent.Energy.valueMax)
        {
            _StateMachine.ChangeState(TypeState.Jugar);
        }
        base.Execute();

    }
}