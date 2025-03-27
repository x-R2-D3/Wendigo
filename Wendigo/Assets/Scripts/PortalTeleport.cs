using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public PortalTeleport otherTeleport;

    public void OnTriggerStay(Collider other)
    {
        float zPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

        if (zPosition < 0)
            Teleport(other.transform);



    }

    private void Teleport(Transform objectToTeleport)
    {
        Vector3 localPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(objectToTeleport.position);
        localPosition = new Vector3(-localPosition.x, localPosition.y, localPosition.z);
        objectToTeleport.position = otherTeleport.transform.localToWorldMatrix.MultiplyPoint3x4(localPosition);

        Quaternion difference = otherTeleport.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        objectToTeleport.rotation = difference * objectToTeleport.rotation;
    }

}
