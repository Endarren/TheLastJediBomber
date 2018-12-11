using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingField : MonoBehaviour
{
	public AnimationCurve speedCurve;
	public float maxSpeed;
	public Vector3 direction = Vector3.down;

	private void OnTriggerStay(Collider other)
	{
		
	}
}
