using UnityEngine;

namespace Utilities {


	public class Angles {
		static public float ClampAngle(float angle) {
			return ClampAngle(angle, 0, 360);
		}

		static public float ClampAngle(float angle, float min, float max) {
			if(angle < -360) angle += 360;
			if(angle > 360) angle -= 360;

			return Mathf.Clamp(angle, min, max);
		}
	}
}
