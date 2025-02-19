using System.Collections;
using UnityEngine;

public class Tube : MonoBehaviour {
    public GameObject prefabCacca;  
    public Transform puntoEspulsione; 
    public int numeroCacche = 3; 
    public float intervalloEspulsione = 0.5f; 
    private bool playerVicino = false; 
    private bool caccheGenerate = false; 

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Carrello")) {
            playerVicino = true;
            ControllaEspulsione();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Carrello")) {
            playerVicino = false;
        }
    }

    void ControllaEspulsione() {
        if (playerVicino && !caccheGenerate) {  
            caccheGenerate = true; 
            StartCoroutine(EspelliCacche());
        }
    }

    IEnumerator EspelliCacche() {
        for (int i = 0; i < numeroCacche; i++) {
            GameObject cacca = Instantiate(prefabCacca, puntoEspulsione.position, Quaternion.identity);

            // Draggable draggable = cacca.AddComponent<Draggable>();
            Rigidbody rb = cacca.GetComponent<Rigidbody>();

            if (rb != null) {
                rb.velocity = Vector3.down * 2; 
            }

            yield return new WaitForSeconds(intervalloEspulsione);
        }
    }
}
