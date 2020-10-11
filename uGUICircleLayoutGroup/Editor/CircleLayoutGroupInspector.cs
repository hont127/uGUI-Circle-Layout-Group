using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Hont
{
    [CustomEditor(typeof(CircleLayoutGroup))]
    public class CircleLayoutGroupInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var lookAtToPivotProp = serializedObject.FindProperty("lookAtToPivot");
            var offsetProp = serializedObject.FindProperty("offset");
            var spacingProp = serializedObject.FindProperty("spacing");
            var radiusProp = serializedObject.FindProperty("radius");
            var modeProp = serializedObject.FindProperty("mode");
            var clockwiseProp = serializedObject.FindProperty("clockwise");

            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                EditorGUILayout.PropertyField(lookAtToPivotProp);
                EditorGUILayout.PropertyField(offsetProp);

                if ((CircleLayoutGroup.EMode)modeProp.enumValueIndex == CircleLayoutGroup.EMode.FixedStep)
                    EditorGUILayout.PropertyField(spacingProp);

                EditorGUILayout.PropertyField(radiusProp);
                EditorGUILayout.PropertyField(modeProp);
                EditorGUILayout.PropertyField(clockwiseProp);

                if (changeCheck.changed)
                {
                    (target as CircleLayoutGroup).ManualUpdateLayout();

                    serializedObject.ApplyModifiedProperties();
                }
            }
        }
    }
}
