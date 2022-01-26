using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float smoothSpeed = 0.125f;
	public float Distance;

	void FixedUpdate()
	{
		Distance = Vector3.Distance(this.transform.position, target.transform.position);

		Distance = 5;
		transform.position = (transform.position - target.transform.position).normalized * Distance + target.transform.position;

		Vector3 desiredPosition = target.position;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		transform.LookAt(target);
	}
}