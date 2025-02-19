using System;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;
using System.Collections;

public class PulsantiLuciAscensore : MonoBehaviour {
    private Button _faringe_button;
    private Button _laringe_button;
    private Button _esofago_button;
    private MeshRenderer _faringe_light;
    private MeshRenderer _laringe_light;
    private MeshRenderer _esofago_light;

    private Material _active_light;
    private Material _inactive_light;

    public Action OnCorrectButtonPress;

    [SerializeField] private GameObject luceEmergenza1;
    [SerializeField] private GameObject luceEmergenza2;
    [SerializeField] private GameObject schermo;
    [SerializeField] private Material schermoMaterialeGrigio;
    [SerializeField] private Material schermoMaterialeTexture;
    [SerializeField] private float blinkInterval = 0.3f;
    [SerializeField] private float emissionIntensity = 1.5f; 
    [SerializeField] private string emissionColorHex = "#6B0000"; 

    private Coroutine emergencyLightsCoroutine;
    private Coroutine screenEmissionCoroutine;
    private bool emergencyActive = false;
    private Color emissionColor;

    void Start() {
        _faringe_button = Children.FindChild(gameObject, "Button (1)").GetComponent<Button>();
        _laringe_button = Children.FindChild(gameObject, "Button").GetComponent<Button>();
        _esofago_button = Children.FindChild(gameObject, "Button (2)").GetComponent<Button>();

        _faringe_light = Children.FindChild(gameObject, "tastoFaringe").GetComponent<MeshRenderer>();
        _laringe_light = Children.FindChild(gameObject, "tastoLaringe").GetComponent<MeshRenderer>();
        _esofago_light = Children.FindChild(gameObject, "tastoEsofago").GetComponent<MeshRenderer>();

        Assert.IsNotNull(_faringe_button, $"{name} cannot find the button for the faringe");
        Assert.IsNotNull(_laringe_button, $"{name} cannot find the button for the laringe");
        Assert.IsNotNull(_esofago_button, $"{name} cannot find the button for the esofago");

        Assert.IsNotNull(_faringe_light, $"{name} cannot find the light for the faringe");
        Assert.IsNotNull(_laringe_light, $"{name} cannot find the light for the laringe");
        Assert.IsNotNull(_esofago_light, $"{name} cannot find the light for the esofago");

        _active_light = _faringe_light.material;
        _inactive_light = _esofago_light.material;

        Assert.IsNotNull(_active_light, $"{name} cannot find the active light material");
        Assert.IsNotNull(_inactive_light, $"{name} cannot find the inactive light material");

        _esofago_button.activated += CorrectChoice;
        _laringe_button.activated += WrongChoice;

        Assert.IsNotNull(schermo, "Schermo non assegnato!");
        Assert.IsNotNull(schermoMaterialeGrigio, "Materiale GRIGIO non assegnato!");
        Assert.IsNotNull(schermoMaterialeTexture, "Materiale con TEXTURE non assegnato!");

        ResetScreenMaterial();

        if (!ColorUtility.TryParseHtmlString(emissionColorHex, out emissionColor)) {
            Debug.LogError($"Colore esadecimale non valido: {emissionColorHex}. Usando bianco di default.");
            emissionColor = Color.white;
        }
    }

    private void CorrectChoice() {
        OnCorrectButtonPress?.Invoke();
        StopEmergencyLights();
        ResetScreenMaterial();
    }

    private void WrongChoice() {
        if (!emergencyActive) {
            emergencyActive = true;
            emergencyLightsCoroutine = StartCoroutine(EmergencyLightsBlink());
            ChangeScreenMaterial();
        }
    }

    public void LeavingFaringe() {
        _faringe_light.material = _inactive_light;
    }

    public void ArrivingEsofago() {
        _esofago_light.material = _active_light;
    }

    private IEnumerator EmergencyLightsBlink() {
        Material redMaterial = new Material(Shader.Find("Standard"));
        redMaterial.SetColor("_EmissionColor", Color.red * 2f);
        redMaterial.EnableKeyword("_EMISSION");

        MeshRenderer renderer1 = luceEmergenza1.GetComponent<MeshRenderer>();
        MeshRenderer renderer2 = luceEmergenza2.GetComponent<MeshRenderer>();

        while (true) {
            renderer1.material = redMaterial;
            renderer2.material = _inactive_light;
            yield return new WaitForSeconds(blinkInterval);

            renderer1.material = _inactive_light;
            renderer2.material = redMaterial;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    public void StopEmergencyLights() {
        if (emergencyLightsCoroutine != null) {
            StopCoroutine(emergencyLightsCoroutine);
            emergencyActive = false;

            luceEmergenza1.GetComponent<MeshRenderer>().material = _inactive_light;
            luceEmergenza2.GetComponent<MeshRenderer>().material = _inactive_light;
        }

        StopScreenEmissionBlink();
    }

    private void ChangeScreenMaterial() {
        MeshRenderer screenRenderer = schermo.GetComponent<MeshRenderer>();
        Material[] materials = screenRenderer.materials;

        if (materials.Length > 1) {
            materials[1] = schermoMaterialeTexture;
            screenRenderer.materials = materials;

            StartScreenEmissionBlink();
        }
    }

    private void ResetScreenMaterial() {
        MeshRenderer screenRenderer = schermo.GetComponent<MeshRenderer>();
        Material[] materials = screenRenderer.materials;

        if (materials.Length > 1) {
            materials[1] = schermoMaterialeGrigio;
            screenRenderer.materials = materials;
        }

        StopScreenEmissionBlink();
    }

    private void StartScreenEmissionBlink() {
        if (screenEmissionCoroutine == null) {
            screenEmissionCoroutine = StartCoroutine(ScreenEmissionBlink());
        }
    }

    private void StopScreenEmissionBlink() {
        if (screenEmissionCoroutine != null) {
            StopCoroutine(screenEmissionCoroutine);
            screenEmissionCoroutine = null;

            Material screenMaterial = schermo.GetComponent<MeshRenderer>().materials[1];
            screenMaterial.SetColor("_EmissionColor", emissionColor * 0.0f);
            screenMaterial.EnableKeyword("_EMISSION");
        }
    }

    private IEnumerator ScreenEmissionBlink() {
        MeshRenderer screenRenderer = schermo.GetComponent<MeshRenderer>();
        Material screenMaterial = screenRenderer.materials[1];

        while (true) {
            screenMaterial.SetColor("_EmissionColor", emissionColor * emissionIntensity);
            screenMaterial.EnableKeyword("_EMISSION");

            yield return new WaitForSeconds(blinkInterval);

            screenMaterial.SetColor("_EmissionColor", emissionColor * 0.0f);
            screenMaterial.EnableKeyword("_EMISSION");

            yield return new WaitForSeconds(blinkInterval);
        }
    }
}


