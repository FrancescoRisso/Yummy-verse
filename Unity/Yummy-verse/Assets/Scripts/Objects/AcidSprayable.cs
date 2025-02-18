using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(MoveObject))]
public class AcidSprayable : MonoBehaviour {
	private MoveObject _drop_script;

	void Start() {
		_drop_script = GetComponent<MoveObject>();
		Assert.IsNotNull(_drop_script, $"{name} cannot find its drop script");

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		float height = mesh.bounds.size.y * transform.lossyScale.y;

		_drop_script.SetDelta(-height);
		_drop_script.SetDirection(Direction.Vertical);
	}

	public void Drop() {
		_drop_script.Trigger();
		Destroy(this);
	}
}
