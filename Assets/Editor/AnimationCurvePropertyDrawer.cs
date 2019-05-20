using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AnimationCurve))]
public class AnimationCurvePropertyDrawer: PropertyDrawer {
	static AnimationCurve buffer;

	public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label) {
		Event e = Event.current;

		if(e.type == EventType.MouseDown && e.button == 1 && rect.Contains(e.mousePosition)){
			GenericMenu context = new GenericMenu();

			context.AddItem(new GUIContent ("Copy"), false, () => {buffer = property.animationCurveValue;});
			context.AddItem(new GUIContent ("Paste"), false, () => {
				if(buffer == null) return;

				property.animationCurveValue = new AnimationCurve(buffer.keys);
				property.animationCurveValue.preWrapMode = buffer.preWrapMode;
				property.animationCurveValue.postWrapMode = buffer.postWrapMode;

				property.serializedObject.ApplyModifiedProperties();
			});

			context.ShowAsContext();
			e.Use();
		}

		EditorGUI.PropertyField(rect, property);
	}
}
