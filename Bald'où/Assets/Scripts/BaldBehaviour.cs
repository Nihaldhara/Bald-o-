using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BaldBehaviour : MonoBehaviour
{
    [SerializeField] public Transform target;
    
    //TODO: Changer les ranges pour s'adapter au mouvement du plateau
    private RangeAttribute xRange = new(-15f, 15f);
    private RangeAttribute zRange = new(-15f, 15f);
    private NavMeshAgent agent;

    private GameManager gameManager;
    private XRGrabInteractable xri;

    [SerializeField] private int countdown = 3;

    void Start()
    {
        gameManager = GameManager.Instance;
        xri = GetComponent<XRGrabInteractable>();
        xri.firstSelectEntered.AddListener(OnSelected);
        xri.lastSelectExited.AddListener(OnReleased);

        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        Vector3 newPos = new Vector3(Random.Range(xRange.min, xRange.max), 6, Random.Range(zRange.min, zRange.max));
        target.position = newPos;

        agent.destination = target.position;
    }


    void Update()
    {
        if (!agent.enabled) return;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    void OnSelected(SelectEnterEventArgs args)
    {
        StartCoroutine(Countdown());
    }
    
    void OnReleased(SelectExitEventArgs args)
    {
        StopCoroutine(Countdown());
        countdown = 3;
    }

    IEnumerator Countdown()
    {
        while (countdown > 0)
        {
            yield return new WaitForSeconds(1);
            countdown--;
        }
        FinishLevel();
    }

    //Le joueur choppe le chauve
    //On lance un countdown de 3 secondes :
    //si le joueur lâche le chauve, le countdown est reset et la partie reprend
    //si le countdown se termine avant que le joueur lâche, on passe au niveau suivant
    
    void FinishLevel()
    {
        gameManager.BaldieGrabbed();
        gameObject.SetActive(false);
    }
}