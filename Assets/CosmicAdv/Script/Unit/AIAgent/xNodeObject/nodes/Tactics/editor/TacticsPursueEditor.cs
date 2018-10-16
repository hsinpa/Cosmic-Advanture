using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;
using UnityEditor;

namespace _AIAgent
{
	[CustomNodeEditor(typeof(TacticsPursueNode))]
	public class TacticsPursueEditor : NodeEditor {

		public override void OnBodyGUI() {
			  TacticsPursueNode targ = target as TacticsPursueNode;

			NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("responseTime"));
			NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("positionType"));

			EditorGUILayout.LabelField("Pattern Type : ");
			TacticsPursueNode.Pattern newValue = (TacticsPursueNode.Pattern)EditorGUILayout.EnumPopup( targ.patternType );
			if( newValue != targ.patternType) {
				targ.patternType = newValue;
				PresetPatternLayout(targ.patternType, targ );
				// do stuff, call functions, etc.
			}

			NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("customPattern"));
			NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("node"));
		}


		private void PresetPatternLayout(TacticsPursueNode.Pattern p_pattern, TacticsPursueNode p_pursueNode) {
			switch(p_pattern) {
				//Vertical
				case TacticsPursueNode.Pattern.Cardinally: {
					p_pursueNode.customPattern = new Vector3[] {
						Vector3.right,
						Vector3.left,
						Vector3.forward,
						Vector3.back
					};
				}
				break;
				//Horizontal
				case TacticsPursueNode.Pattern.Diagonally: {
					p_pursueNode.customPattern = new Vector3[] {
						new Vector3(1,0,1),
						new Vector3(1,0,-1),
						new Vector3(-1,0,1),
						new Vector3(-1,0,-1)
					};
				}
				break;

				case TacticsPursueNode.Pattern.Custom: {
					p_pursueNode.customPattern = new Vector3[0];
				}
				break;
			}
		}

	}

}