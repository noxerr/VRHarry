using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DivideMesh : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Mesh m = GetComponent<SkinnedMeshRenderer>().sharedMesh.GetSubmesh(6);
        GameObject capa = new GameObject("capa");
        SkinnedMeshRenderer skMesh = capa.AddComponent<SkinnedMeshRenderer>();
        skMesh.sharedMesh = m;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
