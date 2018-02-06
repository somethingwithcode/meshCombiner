using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class MeshCombiner : MonoBehaviour
{
    private MeshFilter _myMeshFilter;
    private MeshRenderer _myMeshRenderer;
    private BoxCollider _myBoxCollider;

    public void Build()
    {

        Quaternion myRotation = Quaternion.identity;
        Vector3 myPosition = Vector3.zero;

        MeshFilter[] meshFilter = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combineInstance = new CombineInstance[meshFilter.Length];

        Material[] myMats = GetComponentInChildren<MeshRenderer>().sharedMaterials;

        for (int i = 0; i < meshFilter.Length; i++)
        {
            combineInstance[i].subMeshIndex = 0;
            combineInstance[i].mesh = meshFilter[i].sharedMesh;
            combineInstance[i].transform = meshFilter[i].transform.localToWorldMatrix;
            

        }

        Mesh newMesh = new Mesh() { name = "CombinedMesh" };
        newMesh.CombineMeshes(combineInstance);

        _myMeshFilter = gameObject.AddComponent<MeshFilter>();

        _myMeshFilter.sharedMesh = newMesh;

        _myMeshRenderer = gameObject.AddComponent<MeshRenderer>();

        _myBoxCollider = gameObject.AddComponent<BoxCollider>();

        foreach (var mat in myMats)
        {
            _myMeshRenderer.material = mat;
        }

        transform.rotation = myRotation;
        transform.position = myPosition;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        gameObject.isStatic = true;
    }

    public void ResetMesh()
    {
        _myBoxCollider = GetComponent<BoxCollider>();
        _myMeshFilter = GetComponent<MeshFilter>();
        _myMeshRenderer = GetComponent<MeshRenderer>();

        DestroyImmediate(_myBoxCollider);
        DestroyImmediate(_myMeshFilter);
        DestroyImmediate(_myMeshRenderer);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

    }
}
