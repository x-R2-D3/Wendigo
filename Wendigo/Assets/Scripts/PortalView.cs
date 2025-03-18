using UnityEngine;
using UnityEngine.XR;

public class PortalView : MonoBehaviour
{
    public PortalView otherPortal;
    public Camera portalView;
    public Shader portalShader;
    public Camera xrCamera; // Assign the XR Rig camera in the Inspector

    [SerializeField] private MeshRenderer portalMesh;
    private Material portalMaterial;

    private void Start()
    {
        if (xrCamera == null)
        {
            Debug.LogError("XR Camera is not assigned! Please assign it in the Inspector.");
            return;
        }

        // Ensure XR-supported render texture
        otherPortal.portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24)
        {
            dimension = UnityEngine.Rendering.TextureDimension.Tex2DArray,
            volumeDepth = 2,
            antiAliasing = 2,
            vrUsage = VRTextureUsage.TwoEyes
        };

        // Assign portal material
        portalMaterial = new Material(portalShader);
        portalMaterial.mainTexture = otherPortal.portalView.targetTexture;
        portalMesh.material = portalMaterial;
    }

    private void Update()
    {
        if (xrCamera == null) return;

        // Adjust the portal view position based on the VR headset’s perspective
        Matrix4x4 cameraToWorld = xrCamera.cameraToWorldMatrix;
        Matrix4x4 portalLocalToWorld = xrCamera.cameraToWorldMatrix;
        //Matrix4x4 portalLocalToWorld = otherPortal.transform.localToWorldMatrix;
        Matrix4x4 worldToPortal = transform.worldToLocalMatrix;

        Matrix4x4 portalCameraMatrix = portalLocalToWorld * worldToPortal * cameraToWorld;
        portalView.worldToCameraMatrix = portalCameraMatrix;

        // Ensure correct perspective projection for stereo rendering
        portalView.projectionMatrix = xrCamera.projectionMatrix;

        // Ensure portal renders correctly per frame
        portalView.Render();
    }
}

