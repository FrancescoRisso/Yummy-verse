using UnityEngine;

public class ChewingLights : MonoBehaviour {
    [SerializeField] private Renderer light1;
    [SerializeField] private Renderer light2;
    [SerializeField] private PercentageCycleNotifier notifier;
    [SerializeField] private float emissionIntensity = 2f;

    private int numChewings = 0;
    private Material light1Material;
    private Material light2Material;

    void Start() {
        if (notifier != null)
            notifier.OnNewIteration += UpdateLights;

        if (light1 != null)
            light1Material = light1.material;
        
        if (light2 != null)
            light2Material = light2.material;
    }

    private void UpdateLights(int num) {
        numChewings = num;

        if (numChewings >= 1 && light1Material != null) {
            light1Material.EnableKeyword("_EMISSION");
            light1Material.SetColor("_EmissionColor", Color.yellow * emissionIntensity);
        }

        if (numChewings >= 2 && light2Material != null) {
            light2Material.EnableKeyword("_EMISSION");
            light2Material.SetColor("_EmissionColor", Color.yellow * emissionIntensity);
        }
    }
}
