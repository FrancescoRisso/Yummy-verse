
using UnityEngine;
using UnityEngine.Assertions;

public class NotInserting_SubstanceBoxState : SubstanceBoxState {
	private bool _inserting = false;

	public override void PrepareBeforeAction(SubstanceBoxParam param) {
		param._addInsertIntoBoxListener.Invoke((GameObject _box) => {
			_inserting = true;
			param._game_object.transform.SetParent(_box.transform);

			Draggable draggable_component = param._game_object.GetComponent<Draggable>();
			Assert.IsNotNull(draggable_component, $"{param._game_object.name} cannot find its Draggable component");
			MonoBehaviour.Destroy(draggable_component);
		});
	}

	public override void StateAction(SubstanceBoxParam param) {}

	public override SubstanceBoxState Transition(SubstanceBoxParam param) {
		if(_inserting) return new PositionPreparing_SubstanceBoxState();
		return this;
	}
}
