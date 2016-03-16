using UnityEngine;
using System.Collections;

public class unityAnimTestExplode : MonoBehaviour {

	public Transform explodePoint;
	public float explodePower = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Space)) {
			Explode ();
		}
	}

	void Explode () {
		Rigidbody[] bodies = gameObject.GetComponentsInChildren<Rigidbody> ();

		for (int i = 0; i < bodies.Length; i++) {
			
			float dist = Vector3.Distance (explodePoint.position, bodies [i].transform.position);
			float powerRatio = Mathf.InverseLerp (5, 0, dist);

			Vector3 dir = (bodies [i].transform.position - explodePoint.position).normalized;

			bodies [i].AddForce (dir * explodePower * powerRatio * Random.Range( 1.0f, 1.5f ));

		}
	}
}
