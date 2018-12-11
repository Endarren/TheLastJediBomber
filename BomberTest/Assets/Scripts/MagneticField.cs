using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
	public float speed = 2;
	public Vector3 direction;
	public ForceMode forceMode;
	public bool isActive = false;
	public void SetIsActive(bool b)
	{
		isActive = b;
	}
	private void OnTriggerStay(Collider other)
	{
		if (!isActive)
			return;
		Rigidbody rb = other.GetComponent<Rigidbody>();
		if(rb != null)
		{
			rb.AddForce(direction * speed, forceMode);
		}
	}
}
