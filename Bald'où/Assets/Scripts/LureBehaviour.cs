using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LureBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    private int destPoint = 0;
    private NavMeshAgent agent;

    private GameManager gameManager;
    private XRGrabInteractable xri;
    
    void Start ()
    {
        gameManager = GameManager.Instance;
        xri = GetComponent<XRGrabInteractable>();
        xri.firstSelectEntered.AddListener(OnSelected);
        
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint() {
        if (targets.Length == 0)
            return;

        agent.destination = targets[destPoint].position;

        destPoint = (destPoint + 1) % targets.Length;
    }


    void Update ()
    {
        if (!agent.enabled || !agent.isOnNavMesh) return;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    void OnSelected(SelectEnterEventArgs args)
    {
        Debug.Log("OnSelected");
        gameManager.LureGrabbed();
    }
}
