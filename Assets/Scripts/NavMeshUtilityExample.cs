using UnityEngine;
using UnityEngine.AI;

public class NavMeshUtilityExample : MonoBehaviour
{
    public NavMeshAgent agente;
    public float rangoDeBusqueda = 10f; // Rango m�s grande para m�s posibilidades
    private Vector3 posicionAleatoria; // Guarda la posici�n generada por la tecla 1

    void Update()
    {
        // 1. Genera posici�n aleatoria en NavMesh
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (SamplePosition(transform.position, rangoDeBusqueda, out posicionAleatoria))
            {
                Debug.Log("Posici�n aleatoria generada: " + posicionAleatoria);
                Debug.DrawRay(posicionAleatoria, Vector3.up * 3f, Color.green, 3f);
            }
            else
            {
                Debug.LogWarning("No se encontr� posici�n v�lida en el NavMesh.");
            }
        }

        // 2. Mueve el agente a la posici�n generada
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (posicionAleatoria != Vector3.zero)
            {
                MoveToTargePosition(posicionAleatoria);
            }
            else
            {
                Debug.LogWarning("Primero genera una posici�n con la tecla 1");
            }
        }

        // 3. Calcula distancia entre posici�n actual y la posici�n aleatoria
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (posicionAleatoria != Vector3.zero)
            {
                CalculatePath(posicionAleatoria);
            }
            else
            {
                Debug.LogWarning("Primero genera una posici�n con la tecla 1");
            }
        }

        // 4. Encuentra el borde m�s cercano del NavMesh
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Vector3 bordeCercano;
            if (FindClosestEdge(transform.position, out bordeCercano))
            {
                Debug.Log("Borde m�s cercano encontrado en: " + bordeCercano);
                Debug.DrawRay(bordeCercano, Vector3.up * 3f, Color.yellow, 3f);
            }
            else
            {
                Debug.LogWarning("No se encontr� un borde cercano.");
            }
        }
    }

    // Funci�n 1: Genera posici�n aleatoria en NavMesh
    bool SamplePosition(Vector3 centro, float rango, out Vector3 resultado)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 puntoAleatorio = centro + Random.insideUnitSphere * rango;
            puntoAleatorio.y = 0; // Ignorar altura

            NavMeshHit hit;
            if (NavMesh.SamplePosition(puntoAleatorio, out hit, rango, NavMesh.AllAreas))
            {
                resultado = hit.position;
                return true;
            }
        }

        resultado = Vector3.zero;
        return false;
    }

    // Funci�n 2: Mueve el agente a la posici�n guardada
    void MoveToTargePosition(Vector3 destino)
    {
        if (agente == null)
        {
            Debug.LogError("�No hay NavMeshAgent asignado!");
            return;
        }

        agente.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        agente.SetDestination(destino);
        Debug.Log("Agente en movimiento hacia: " + destino);
    }

    // Funci�n 3: Calcula y muestra distancia
    void CalculatePath(Vector3 destino)
    {
        float distancia = Vector3.Distance(transform.position, destino);
        Debug.Log($"Distancia entre tu posici�n ({transform.position}) y el destino ({destino}): {distancia.ToString("F2")} unidades");
    }

    // Funci�n 4: Encuentra el borde m�s cercano del NavMesh
    bool FindClosestEdge(Vector3 posicion, out Vector3 bordeCercano)
    {
        NavMeshHit hit;
        if (NavMesh.FindClosestEdge(posicion, out hit, NavMesh.AllAreas))
        {
            bordeCercano = hit.position;
            return true;
        }

        bordeCercano = Vector3.zero;
        return false;
    }
}