using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banno : Humano
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _banno;

    private void Awake()
    {
        typestate = TypeState.Banno;
        LocadComponent();
    }

    public override void LocadComponent()
    {
        base.LocadComponent();
        _playerMovement = GetComponent<PlayerMovement>();
        if (_banno == null) _banno = GameObject.FindWithTag("WC");
    }

    public override void Enter()
    {
        if (_banno != null)
        {
            _playerMovement.MoveToTarget(_banno);
            _DataAgent.LoadWC(); // Inicia recarga de WC
        }
    }

    public override void Execute()
    {
        // WC se recupera automáticamente (LoadWC)
        _DataAgent.DiscountEnergy(); // Energy sigue bajando
        _DataAgent.DiscountSleep();  // Sleep sigue bajando

        // Solo vuelve a Jugar cuando WC esté lleno
        if (_DataAgent.WC.value >= _DataAgent.WC.valueMax)
        {
            _StateMachine.ChangeState(TypeState.Jugar);
        }
        base.Execute();
    }
}