using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public float health = 100f;

	public GameObject deadFX;

	public void TakeDamage(float damageAmount) {
		health -= damageAmount;

		print ("received DAMAGE");

		if (health <= 0) {
			Instantiate (deadFX, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

} // class






































