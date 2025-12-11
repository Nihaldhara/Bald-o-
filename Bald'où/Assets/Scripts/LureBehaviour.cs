using UnityEngine;
using UnityEngine.AI;

public class LureBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    private int destPoint = 0;
    private NavMeshAgent agent;

    void Start () {
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


    void Update () {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
