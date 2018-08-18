using System.Collections;
using UnityEditor;
using UnityEngine;
using CA_Terrain;

[CustomEditor(typeof(TerrainBuilder), true)]
public class InspectorButton_Terrain : Editor {

	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        TerrainBuilder myScript = (TerrainBuilder)target;
        if(GUILayout.Button("Build Object"))
        {
            myScript.BuildTerrain();
        }
    }
}
