using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using _AIAgent;
using XNodeEditor;

[CustomNodeGraphEditor(typeof(AIAgentChart))]
public class AIChartEditor : NodeGraphEditor {
	private bool isExecuteOnce = false;

	public override void OnGUI() {
		// Keep repainting the GUI of the active NodeEditorWindow
		NodeEditorWindow.current.Repaint();

		if (!isExecuteOnce) {
			AIAgentChart chart = (AIAgentChart) target;
			chart.SaveEvent -= SaveRecord;
			chart.SaveEvent += SaveRecord;
			isExecuteOnce = true;
		}
	}

	private void SaveRecord() {
		AIAgentChart chart = (AIAgentChart) target;
		EditorUtility.SetDirty(target);
		EditorUtility.SetDirty(chart.agentNode);

		AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
		AssetDatabase.SaveAssets();
	}

}
