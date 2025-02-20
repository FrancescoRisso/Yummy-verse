using UnityEngine;

public class Initial_MainMenuState : MainMenuState {
	private bool _hovering;
	private bool _clicked = false;

	public override void StateAction(MainMenuParameter param) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(ray, out RaycastHit hit, 10))
			_hovering = hit.transform.name == param._start_game_button.name;
		else
			_hovering = false;

		if(Input.GetMouseButtonDown(0) && _hovering) _clicked = true;

		if(_clicked)
			param._libro.material = param._libro_selected;
		else if(_hovering)
			param._libro.material = param._libro_hover;
		else
			param._libro.material = param._libro_normal;
	}

	public override MainMenuState Transition(MainMenuParameter param) {
		if(_clicked) return new Darkening_MainMenuState();
		return this;
	}
}
