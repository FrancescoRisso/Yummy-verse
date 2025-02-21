using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(PercentageToggleManager))]
public class CartEmptier : MonoBehaviour {
	private List<DraggableOnCart> _items = new List<DraggableOnCart>();

	private bool _emptying = false;

	[SerializeField]
	private Button _button;

	private PercentageToggleManager _perc;

	[Range(0, 90)]
	[SerializeField]
	private float _max_angle;

	[Range(0, 2)]
	[SerializeField]
	private float _wait_before_lowering;

	[SerializeField]
	private ItemPicker _picker;

	void Start() {
		Assert.IsNotNull(_button, $"{name} does not have its button assigned");
		Assert.IsNotNull(_picker, $"{name} does not have its item picker assigned");

		_perc = GetComponent<PercentageToggleManager>();
		Assert.IsNotNull(_perc, $"{name} does not have its percentage toggle manager assigned");

		_button.activated += StartEmptying;
		_perc.OnPercentageChange += (float val) => {
			if(val == 0) StopEmptying();
			if(val == 1) StartCoroutine(WaitThenLower());

			transform.localEulerAngles = new Vector3(val * _max_angle, 0, 0);
		};
	}

	private IEnumerator WaitThenLower() {
		foreach(DraggableOnCart item in _items) item.GetComponent<Rigidbody>()?.AddForce(transform.forward, ForceMode.Impulse);

		yield return new WaitForSeconds(_wait_before_lowering);
		_perc._toggle.Invoke();
	}

	private void StopEmptying() {
		_items = new List<DraggableOnCart>();
		_emptying = false;
		_picker.enabled = true;
	}

	private void StartEmptying() {
		if(!_emptying) {
			Debug.LogError("Start emptying");
			foreach(DraggableOnCart item in _items) item.Undrag();

			_emptying = true;
			_picker.enabled = false;

			_perc._toggle.Invoke();
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if(!_emptying) {
			DraggableOnCart item = collision.collider.GetComponent<DraggableOnCart>();
			if(item) {
				_items.Add(item);
				item.Drag(transform);
			}
		}
	}

	private void OnCollisionExit(Collision collision) {
		DraggableOnCart item = collision.collider.GetComponent<DraggableOnCart>();
		if(item) _items.Remove(item);
	}
}
