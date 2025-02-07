using UnityEngine;

public class Initial_MainMenuState : MainMenuState {
	public override void StateAction(MainMenuParameter param) {
		Debug.Log("Stato iniziale");
	}

	public override MainMenuState Transition(MainMenuParameter param) {
		if(Input.GetMouseButtonDown(0))  // TODO controllare se è nel posto giusto
			return new Darkening_MainMenuState();

		return this;
	}
}
