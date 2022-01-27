using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float smoothSpeed = 0.125f;
	public float Distance;

    void Update()
    {
		Distance = Vector3.Distance(this.transform.position, target.transform.position);

		Distance = 3;
		Vector3 pos = (transform.position - target.transform.position).normalized * Distance + target.transform.position;

		transform.position = new Vector3(pos.x, 1, pos.z);

		Vector3 desiredPosition = target.position;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		transform.LookAt(target);
	}
}