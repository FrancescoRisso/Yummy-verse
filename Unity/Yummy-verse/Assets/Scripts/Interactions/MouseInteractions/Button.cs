using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class Button : OneShotInteractable {
    private Component _button_top;

    [SerializeField]
    private float _press_speed;
    [SerializeField]
    private float _release_speed;

    public PercentageToggleManager _perc;

    private Vector3 _original_pos;
    private float _delta_h = 0.01f;
    private Vector3 _pressed_pos;

    // Aggiunta per il suono
    private AudioSource _audioSource;

    void Start() {
        _button_top = Children.FindChild(gameObject, "pulsante");
        Assert.IsNotNull(_button_top, $"{name} cannot find its button to be moved");

        _perc = GetComponent<PercentageToggleManager>();
        Assert.IsNotNull(_button_top, $"{name} cannot find its percentage toggle manager");

        _original_pos = _button_top.transform.localPosition;
        _pressed_pos = _button_top.transform.localPosition - _delta_h * Vector3.up;

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    protected override void LocalActionOnClick() {
        switch(_perc.CurrentStatus()) {
            case Operation.Stopped:
                _perc.OnPercentageChange += UpdatePosition;
                _perc._speed = _press_speed;
                _perc._toggle.Invoke();
                StartPlayingSound();  
                break;

            case Operation.Increasing:
                _perc._speed = _press_speed;
                _perc._toggle.Invoke();
                _perc._toggle.Invoke();
                StartPlayingSound();  
                break;

            case Operation.Decreasing: break;
        }
    }

    private void UpdatePosition(float perc) {
        Vector3 pos = _button_top.transform.localPosition;
        _button_top.transform.localPosition = perc * _original_pos + (1 - perc) * _pressed_pos;

        if (perc == 0) {
            _perc._speed = _release_speed;
            _perc._toggle.Invoke();
            StopPlayingSound();  
        }

        if (perc == 1) {
            _perc.OnPercentageChange -= UpdatePosition;
            StopPlayingSound();  
        }
    }

    private void StartPlayingSound() {
        if (_audioSource != null && !_audioSource.isPlaying) {
            _audioSource.Play();
        }
    }

    private void StopPlayingSound() {
        if (_audioSource != null && _audioSource.isPlaying) {
            _audioSource.Stop();
        }
    }
}
