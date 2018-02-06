using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(MeshCombiner))]
public class MeshCombinerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MeshCombiner myScript = (MeshCombiner)target;

        if (GUILayout.Button("Build Object"))
        {
            myScript.Build();
        }

        if(GUILayout.Button("Reset Combined Mesh"))
        {
            myScript.ResetMesh();
        }
    }

}