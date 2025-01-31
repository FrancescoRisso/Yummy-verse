using UnityEngine;

namespace Utilities {


	public class Children {
		public static Component FindChild(GameObject gameObject, string child_name) {
			foreach(Component comp in gameObject.GetComponentsInChildren<Component>(true))
				if(comp.gameObject.name == child_name) return comp;
			return null;
		}
	}
}
