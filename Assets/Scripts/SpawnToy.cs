using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToy : MonoBehaviour
{
    [Header("Objeto a Spawnear")]
    public GameObject objetoRecolectable;

    [Header("Parámetros de Spawn")]
    public float alturaInicial = 10f;
    public LayerMask naveLayer;
    public float radioSpawn = 5f;
    public float tiempoEntreSpawns = 5f;

    [Header("Referencia al Centro del Área")]
    public Transform areaCentro;

    private float tiempoActual = 0f;

    void Update()
    {
        tiempoActual += Time.deltaTime;

        if (tiempoActual >= tiempoEntreSpawns)
        {
            SpawnSobreNave();
            tiempoActual = 0f;
        }
    }

    void SpawnSobreNave()
    {
        if (areaCentro == null)
        {
            Debug.LogWarning("No se asignó un área de centro para el spawn.");
            return;
        }

        Vector3 randomOffset = Random.insideUnitSphere * radioSpawn;
        randomOffset.y = 0;

        Vector3 puntoInicial = areaCentro.position + randomOffset + Vector3.up * alturaInicial;

        if (Physics.Raycast(puntoInicial, Vector3.down, out RaycastHit hit, Mathf.Infinity, naveLayer))
        {
            // Añade +0.1 en el eje Y al punto de impacto
            Vector3 spawnPosition = hit.point + Vector3.up * 0.1f;

            Instantiate(objetoRecolectable, spawnPosition, Quaternion.identity);
            Debug.Log("Objeto generado en: " + spawnPosition);
        }
        else
        {
            Debug.Log("El raycast no golpeó la nave, no se generó objeto.");
        }
    }

    // Para depurar en la escena
    private void OnDrawGizmosSelected()
    {
        if (areaCentro != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(areaCentro.position, radioSpawn);
        }
    }
}