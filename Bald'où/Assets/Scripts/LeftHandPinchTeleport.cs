
using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using UnityEngine.InputSystem;

using Unity.Mathematics; // si tu l'as ; sinon remplace float3 par Vector3

public class LeftHandPinchTeleport : MonoBehaviour
{
    [Header("Références")]
    public XRHandSubsystem handSubsystem;            // Option : sera auto-renseigné si null
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor leftHandRay;              // Ray interactor pour viser (Left Hand)
    public LineRenderer rayVisual;                   // Optionnel: visuel du ray
    public UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationProvider teleportProvider;   // Sur le XR Origin
    public Transform xrOrigin;                       // XR Origin transform (pour fallback)

    [Header("Réglages Pinch")]
    [Tooltip("Distance (m) entre index tip et pouce tip considérée comme pinch.")]
    public float pinchEnterDistance = 0.025f;
    [Tooltip("Hysteresis: distance (m) pour considérer fin du pinch.")]
    public float pinchExitDistance = 0.035f;
    [Tooltip("Délai minimum entre deux téléportations (s).")]
    public float teleportCooldown = 0.35f;

    [Header("Ray")]
    [Tooltip("Distance max du ray de visée.")]
    public float maxRayDistance = 10f;

    [SerializeField] private InputActionReference m_PinchAction;
    
    private bool isPinching = false;
    private float lastTeleportTime = -999f;

    private GameManager gameManager;

    void Awake()
    {
        if (!teleportProvider) teleportProvider = FindFirstObjectByType<UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationProvider>();
        if (!xrOrigin) xrOrigin = (teleportProvider ? teleportProvider.system.xrOrigin.CameraFloorOffsetObject.transform.parent : null);
        if (!leftHandRay) leftHandRay = FindFirstObjectByType<UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor>();
    }


    private void Start()
    {
        gameManager = GameManager.Instance;
        m_PinchAction.action.started += PinchActionStarted;
        m_PinchAction.action.canceled += PinchActionCanceled;
    }

    private void PinchActionStarted(InputAction.CallbackContext callbackContext)
    {
        SetRayActive(true);
    }
    
    private void PinchActionCanceled(InputAction.CallbackContext callbackContext)
    {
        if (!gameManager.isZoomedIn)
        {
            xrOrigin.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        gameManager.isZoomedIn = true;
        TryTeleportFromRay();
    }

    private void SetRayActive(bool active)
    {
        if (leftHandRay) leftHandRay.enabled = active;
        if (rayVisual) rayVisual.enabled = active;
    }

    private void TryTeleportFromRay()
    {
        if (leftHandRay == null || teleportProvider == null)
        {
            return;
        }

        // Cast depuis le XRRayInteractor pour récupérer un hit XRI (priorise TeleportationArea/Anchor)
        if (leftHandRay.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            var dest = hit.point;

            // Vérifie si la cible est une surface de téléportation
            var interactable = hit.collider.GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.BaseTeleportationInteractable>();
            if (interactable != null && interactable.isActiveAndEnabled)
            {
                var request = new UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportRequest()
                {
                    destinationPosition = dest,
                    matchOrientation = UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.MatchOrientation.WorldSpaceUp
                };
                teleportProvider.QueueTeleportRequest(request);
                lastTeleportTime = Time.time;
            }
        }

        // Désactive le ray après tentative (style “hold-to-aim, release-to-teleport”)
        SetRayActive(false);
    }
}
