using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (explosion != null)
		{
			GameObject go = Instantiate(explosion);
			go.transform.position = transform.position;
		}
		Destroy(gameObject);
	}
}
