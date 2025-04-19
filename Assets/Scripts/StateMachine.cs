using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State[] states;
    public State currentState = null;
    public TypeState Startstate = TypeState.Jugar; // Siempre inicia en Jugar

    void Start()
    {
        states = GetComponents<State>();
        ChangeState(Startstate);
    }

    public void ChangeState(TypeState newState)
    {
        // Bloquea cambios si ya está en FollowToy (excepto para salir)
        if (currentState != null && currentState.typestate == TypeState.FollowToy && newState != TypeState.Jugar)
            return;

        foreach (var state in states)
        {
            if (state.typestate == newState)
            {
                if (currentState != null)
                {
                    currentState.Exit();
                    currentState.enabled = false;
                }

                currentState = state;
                currentState.enabled = true;
                currentState.Enter();
                Debug.Log($"Cambiado a {newState}");
                return;
            }
        }
        Debug.LogError($"Estado {newState} no encontrado");
    }

    // Nuevo método para forzar FollowToy (prioridad absoluta)
    public void ForceFollowToy()
    {
        ChangeState(TypeState.FollowToy);
    }

    private void Update()
    {
        if (currentState != null)
            currentState.Execute();
    }
}