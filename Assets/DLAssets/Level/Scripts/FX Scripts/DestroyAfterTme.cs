using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTme : MonoBehaviour {

	public float timer = 2f;

	void Start () {
		Destroy (gameObject, timer);	
	}
}
