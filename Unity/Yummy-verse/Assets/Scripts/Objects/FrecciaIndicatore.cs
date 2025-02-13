using System;
using UnityEngine;

public class PercentageIndicator : MonoBehaviour {
    [SerializeField] private Transform arrowTransform;
    [SerializeField] private float minRotation = 0f;
    [SerializeField] private float maxRotation = 180f;
    [SerializeField] private PercentageDecayManager decayManager;

    void Start() {
        if (decayManager != null)
            decayManager.OnPercentageChange += UpdateArrowRotation;
    }

    void UpdateArrowRotation(float percentage) {
        float targetRotation = Mathf.Lerp(minRotation, maxRotation, percentage);
        arrowTransform.rotation = Quaternion.Euler(0, targetRotation, 0);
    }
}
