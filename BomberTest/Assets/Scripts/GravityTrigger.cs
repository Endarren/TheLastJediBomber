using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
	public bool isActive = false;
	bool wasActive;
	List<Rigidbody> inTrigger = new List<Rigidbody>();

	private void Awake()
	{
		wasActive = isActive;
	}
	public void SetIsActive(bool b)
	{
		isActive = b;
	}
	private void Update()
	{
		if (wasActive != isActive)
		{
			for (int i = 0; i < inTrigger.Count; i++)
			{
				if (inTrigger[i] != null)
				{
					inTrigger [i].useGravity = isActive;
				}
			}
			wasActive = isActive;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		Rigidbody rb = other.GetComponent<Rigidbody>();
		if (rb != null)
		{
			if(!inTrigger.Contains(rb))
				inTrigger.Add(rb);
			if (isActive)
			{
				rb.useGravity = true;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		Rigidbody rb = other.GetComponent<Rigidbody>();
		if (rb != null)
		{
			inTrigger.Remove(rb);
			if (isActive)
			{
				rb.useGravity = false;
			}
		}
	}
}
