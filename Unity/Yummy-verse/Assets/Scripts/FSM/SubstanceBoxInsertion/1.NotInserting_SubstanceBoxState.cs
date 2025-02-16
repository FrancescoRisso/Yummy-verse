using UnityEngine;
using UnityEngine.Assertions;

public class NotInserting_SubstanceBoxState : SubstanceBoxState {
    private bool _inserting = false;
    private bool _wrongInsertion = false;
    private Vector3 _initialPosition;
    private Vector3 _repelDirection;
    private float _repelForce = 0.5f;
    private float _repelDuration = 0.3f;
    private float _elapsedTime = 0f;

    public override void PrepareBeforeAction(SubstanceBoxParam param) {
    param._addInsertIntoBoxListener.Invoke((SubstanceBoxLights box) => {
        // 🔹 Controlla se la forma della sostanza è accettata dalla scatola
        if (!box.IsShapeAccepted(param._shape)) {  
            // ❌ Forma sbagliata: respinge la sostanza
            _wrongInsertion = true;
            _elapsedTime = 0f;
            _repelDirection = (param._game_object.transform.position - box.transform.position).normalized;
            return;  // Esce dalla funzione, la sostanza non viene inserita
        }

        // ✅ Forma corretta: la sostanza viene accettata e inserita nella scatola
        _inserting = true;
        param._game_object.transform.SetParent(box.GetTransform());

        Draggable draggable_component = param._game_object.GetComponent<Draggable>();
        Assert.IsNotNull(draggable_component, $"{param._game_object.name} cannot find its Draggable component");
        MonoBehaviour.Destroy(draggable_component);
    });
}


    public override void StateAction(SubstanceBoxParam param) {
    if (_wrongInsertion) {
        _elapsedTime += Time.deltaTime;
        float t = _elapsedTime / _repelDuration;

        // Movimento di respinta
        param._game_object.transform.position += _repelDirection * _repelForce * Time.deltaTime;

        // Aggiungiamo un leggero effetto di vibrazione
        Vector3 shake = new Vector3(Mathf.Sin(Time.time * 50) * 0.01f, 0, 0);
        param._game_object.transform.position += shake;

        // Dopo il tempo di respinta, riportiamo la sostanza alla posizione iniziale
        if (_elapsedTime >= _repelDuration) {
            param._game_object.transform.position = Vector3.Lerp(param._game_object.transform.position, _initialPosition, Time.deltaTime * 5);
        }
    }
}


    public override SubstanceBoxState Transition(SubstanceBoxParam param) {
        if (_wrongInsertion && _elapsedTime >= _repelDuration + 0.2f) return this; // Mantiene lo stato finché la sostanza torna in posizione
        if (_inserting) return new PositionPreparing_SubstanceBoxState();
        return this;
    }
}

/*using UnityEngine;
using UnityEngine.Assertions;

public class NotInserting_SubstanceBoxState : SubstanceBoxState {
	private bool _inserting = false;

	public override void PrepareBeforeAction(SubstanceBoxParam param) {
		param._addInsertIntoBoxListener.Invoke((SubstanceBoxLights box) => {
			_inserting = true;
			param._game_object.transform.SetParent(box.GetTransform());

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
}*/
