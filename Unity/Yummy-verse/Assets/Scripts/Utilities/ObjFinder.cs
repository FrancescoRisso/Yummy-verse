using UnityEngine;

namespace Utilities {

	public class ObjFinder {
		public static T FindByName<T>(string name)
			where T : Object {
			foreach(T comp in MonoBehaviour.FindObjectsByType<T>(FindObjectsSortMode.None))
				if(comp.name == name) return comp;
			return null;
		}
	}

}
