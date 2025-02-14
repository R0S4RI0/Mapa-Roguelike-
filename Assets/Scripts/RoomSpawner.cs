using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openSide; // Para saber cuál es el sitio abierto

    // 1 Necesita una puerta abajo
    // 2 Necesita una puerta arriba
    // 3 Necesita una puerta a la izquierda
    // 4 Necesita una puerta a la derecha 

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false; // Para que el spawn lo haga solo una vez

    // Start is called before the first frame update
    void Start()
    {
        // Usamos GetComponent para obtener el componente de RoomTemplates desde el objeto con tag "Rooms"
        GameObject roomsObject = GameObject.FindGameObjectWithTag("Rooms");
        if (roomsObject != null)
        {
            templates = roomsObject.GetComponent<RoomTemplates>(); // Aseguramos que templates no sea nulo
            if (templates != null)
            {
                Invoke("Spawn", 0.1f);
                templates.rooms.Add(this.gameObject);
                Debug.Log("Sala añadida a la lista: " + this.gameObject.name);
            }
            else
            {
                Debug.LogError("No se encontró el componente RoomTemplates en el objeto con tag 'Rooms'");
            }
        }
        else
        {
            Debug.LogError("No se encontró el objeto con tag 'Rooms'");
        }
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openSide == 1)
            {
                // Necesitamos puerta abajo
                rand = Random.Range(0, templates.bottomRooms.Length); // Cogemos una sala aleatoria de todas las que tenemos.
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openSide == 2)
            {
                // Necesitamos puerta arriba
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openSide == 3)
            {
                // Necesitamos puerta izquierda
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openSide == 4)
            {
                // Necesitamos puerta derecha
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    // Método para que no se instancien dos en el mismo sitio
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
