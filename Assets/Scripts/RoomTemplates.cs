using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public GameObject Boss;
    public GameObject SimpleEnemy;

    private void Start()
    {
        Invoke("SpawnEnemies", 3f);
    }

    void SpawnEnemies()
    {
        // Asegúrate de que la última sala sea la correcta para el Boss
        if (rooms.Count > 0 && rooms[rooms.Count - 1] != null)
        {
            // Instanciamos el Boss en la posición de la última sala
            Instantiate(Boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
        }
        

        // Spawnea enemigos simples en todas las salas excepto la última
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            if (rooms[i] != null) // Verificamos que el objeto de la sala no haya sido destruido
            {
                // Instanciamos enemigos simples en la posición de la sala
                Instantiate(SimpleEnemy, rooms[i].transform.position, Quaternion.identity);
            }
          
        }
    }

}
