using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
Escena main: ZonaSpawnEnemigos 1

Este objeto que tiene el script es un plano sin mesh renderer y sin mesh collider
Toma una lista de transforms que son objetos iguales al ZonaSpawnEnemigos 1 pero sin este script
Cada vez que se itera una marmota o jefe se decide un punto de spawn entre la lista de transforms

Constantemente se obtiene la cantidad de enemigos restantes (objetos activos con tag enemy)
Cuando los enemigos restantes es 0 se pasa de oleada

Cuando oleadaActual es igual a maxOleada Y quedan 0 enemigos se activa la condicion de victoria

La condicion de victoria es mostrar el canva victoria que permite volver al menu principal
*/

public class ZonaDeSpawnManager : MonoBehaviour
{
    public GameObject pantallaVictoria;
    public GameObject prefabJefe;
    public GameObject prefabEnemigo;       
    public Transform[] puntosSpawn;
    public float tiempoEntreSpawn = 10f;
    public int maxOleadas = 5;
    public int enemigosPorOleada = 2;      
    public int jefePorOleada = 1;
    public int oleadaActual = 0;          
    public TextMeshProUGUI enemigosRestantes;
    public TextMeshProUGUI oleadas;

    
    void Start()
    {
        StartCoroutine(GenerarOleadas());
    }

    // genera oleadas de enemigos
    IEnumerator GenerarOleadas()
    {
        while (oleadaActual < maxOleadas) // Mientras que oleadaActual sea mayor a maxOleadas(el maximo de oleadas)
        {
            // Busca si hay enemigos vivos
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return new WaitForSeconds(3f);
            }
            oleadaActual++;
            Debug.Log($"¡Oleada {oleadaActual} comenzando!");
            // Genera enemigos en la posición del GameObject donde está asignado este script
            SpawnOleadaEnemigos(prefabEnemigo, (enemigosPorOleada * oleadaActual));
            if ((oleadaActual % 5) == 0 || oleadaActual == maxOleadas)
            {
                SpawnJefeFinal(prefabJefe, jefePorOleada);
            }

            yield return new WaitForSeconds(tiempoEntreSpawn);  // Espera antes de verificar nuevamente
        }
    }

    void Update()
    {
        //verifica que NO haya enemigos vivos, y que oleada actual sea igual a maxOleadas
        if ((GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) && oleadaActual == maxOleadas)
        {
            Time.timeScale = 0;
            pantallaVictoria.SetActive(true); //muestra un canva con la pantalla de victoria
            Debug.Log("¡Todas las oleadas completadas!");

        }
        //muestra los enemigos restantes en el Hud del Player
        enemigosRestantes.SetText("Enemigos restantes: " + GameObject.FindGameObjectsWithTag("Enemy").Length);
        //muestra las oleadas restantes en el Hud del Player
        oleadas.SetText("Oleada: " + oleadaActual);

    }
    // Método para generar una oleada de enemigos
    public void SpawnOleadaEnemigos(GameObject prefabEnemigo, int cantidadEnemigos)
    {
        for (int i = 0; i < cantidadEnemigos; i++) //for que recorre en base a la cantidad de enemigos 
        {
            Transform puntoSpawn = puntosSpawn[Random.Range(0, puntosSpawn.Length)]; 
            Vector3 posicionSpawn = puntoSpawn.position + new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f));
            //genera aleatoriamente puntos de spawn
            GameObject nuevoEnemigo = Instantiate(prefabEnemigo, posicionSpawn, Quaternion.identity); 
            nuevoEnemigo.SetActive(true);
            //Crea un enemigo 
            Debug.Log("Una nueva marmota ha aparecido en la zona: " + puntoSpawn);
        }
    }
    public void SpawnJefeFinal(GameObject prefabJefe, int cantidadJefe)
    {
        for (int i = 0; i < cantidadJefe; i++)
        {
            Transform puntoSpawn = puntosSpawn[Random.Range(0, puntosSpawn.Length)];
            Vector3 posicionSpawn = puntoSpawn.position + new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f));
            GameObject nuevoJefe = Instantiate(prefabJefe, posicionSpawn, Quaternion.identity);
            nuevoJefe.SetActive(true);
            Debug.Log("Una nueva marmota ha aparecido en la zona: " + puntoSpawn);
            //lo mismo que el for de arriba pero basándose en el jefe Marmota
        }
    }
}