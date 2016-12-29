using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePublic : MonoBehaviour {
    public Material[] Materiales;
    private float elapsed = 0;
    private Vector2 offset;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < Materiales.Length; i++)
        {
            Materiales[i].mainTextureOffset = new Vector2(0, 0);
        }
	}


    

	
	// Update is called once per frame
	void FixedUpdate () {
        if (elapsed > 0.4f)
        {
            elapsed = 0;
            for (int i = 0; i < Materiales.Length; i++)
            {
                offset = Materiales[i].mainTextureOffset;
                if (offset.x > 0.75f)
                {
                    offset.x = 0;
                    offset.y += (.5f - offset.y);
                }
                else offset.x += 0.25f;
                Materiales[i].mainTextureOffset = offset;
            }
        }
        elapsed += Time.deltaTime;
	}
}
