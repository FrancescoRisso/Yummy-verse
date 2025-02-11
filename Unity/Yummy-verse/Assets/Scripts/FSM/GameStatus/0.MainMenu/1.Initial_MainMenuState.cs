using UnityEngine;

public class Initial_MainMenuState : MainMenuState {
	public override void StateAction(MainMenuParameter param) {}

	public override MainMenuState Transition(MainMenuParameter param) {
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out RaycastHit hit, 10)) {
				if(hit.transform.name == param._start_game_button.name) return new Darkening_MainMenuState();
			}
		}

		return this;
	}
}
