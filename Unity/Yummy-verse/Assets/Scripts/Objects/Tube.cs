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
        if (other.CompareTag("Player")) {
            playerVicino = true;
            ControllaEspulsione();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
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



/*using System.Collections;
using UnityEngine;

public class Tube : MonoBehaviour {
    public GameObject prefabCacca;  
    public Transform puntoEspulsione; 
    public int numeroCacche = 3; 
    public float intervalloEspulsione = 0.5f; 
    private bool playerVicino = false; 
    private bool caccheGenerate = false; 
    public Transform carrello;  // Riferimento al carrello
    public float distanzaSufficiente = 1.0f; // Distanza minima tra il tubo e il carrello per l'espulsione
    private bool carrelloSotto = false; // Controlla se il carrello è sotto il tubo
    private bool espulsioneInCorso = false; // Per evitare che il carrello si muova mentre espelliamo le cacche

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playerVicino = true;
            ControllaEspulsione();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            playerVicino = false;
        }
    }

    void ControllaEspulsione() {
        // Verifica se il carrello è sotto il tubo
        carrelloSotto = Vector3.Distance(carrello.position, puntoEspulsione.position) <= distanzaSufficiente;

        if (playerVicino && carrelloSotto && !caccheGenerate && !espulsioneInCorso) {  
            caccheGenerate = true; 
            StartCoroutine(EspelliCacche());
        }
    }

    IEnumerator EspelliCacche() {
        espulsioneInCorso = true;  // Ferma il carrello
        // Imposta il movimento del carrello a fermo, se necessario (puoi fare un controllo sul carrello)
        CarrelloMovimento(false); 

        for (int i = 0; i < numeroCacche; i++) {
            GameObject cacca = Instantiate(prefabCacca, puntoEspulsione.position, Quaternion.identity);
            Rigidbody rb = cacca.GetComponent<Rigidbody>();

            if (rb != null) {
                rb.velocity = Vector3.down * 2; 
            }

            yield return new WaitForSeconds(intervalloEspulsione);
        }

        espulsioneInCorso = false;  // Ripristina il carrello
        CarrelloMovimento(true); // Permetti di muovere il carrello
    }

    void CarrelloMovimento(bool attivo) {
        // Aggiungi logica per attivare o disattivare il movimento del carrello
        // Ad esempio se hai un componente che controlla il movimento del carrello, lo attivi o disattivi
        // Puoi anche usare un semplice flag se non hai un sistema di movimento complesso
        if (carrello != null) {
            Rigidbody rbCarrello = carrello.GetComponent<Rigidbody>();
            if (rbCarrello != null) {
                rbCarrello.isKinematic = !attivo; // Disabilita fisica per fermarlo
            }
        }
    }
}
*/
