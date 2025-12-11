using UnityEngine;
using UnityEngine.AI;

public class BaldBehaviour : MonoBehaviour
{
    [SerializeField] public Transform target;
    private RangeAttribute xRange = new (-15f, 15f);
    private RangeAttribute zRange = new (-15f, 15f);
    private NavMeshAgent agent;

    void Start () {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        Vector3 newPos = new Vector3(Random.Range(xRange.min, xRange.max), 0, Random.Range(zRange.min, zRange.max));
        target.position = newPos;
        
        agent.destination = target.position;
    }


    void Update () {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
