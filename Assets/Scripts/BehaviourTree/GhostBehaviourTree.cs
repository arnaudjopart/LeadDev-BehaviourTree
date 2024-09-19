using UnityEngine;
using UnityEngine.AI;

public class GhostBehaviourTree : MonoBehaviour
{
    public Transform[] m_waypoints;
    public GameObject m_testGameObject;
    public float stamina;
    private SequenceNode m_mainNode;
    CompositeNode m_patrolSelector;
    [SerializeField] private Observer m_observer;
    [SerializeField] private NavMeshAgent m_agent;

    private void Start()
    {
        m_mainNode = new SequenceNode("Main Node");

        //Patrolling Logic
        m_patrolSelector = new CheckAllSelectorNode("Patrol Selector");

        var patrolSequence = new AlwaysFalseNode("End Of Patrol returns Fail");
        patrolSequence.AddChild(new Leaf("Patrol", new PatrolStrategy(m_agent, m_waypoints, this)));

        m_patrolSelector.AddToChildren(new Leaf("Check if player is detected", new Condition(() => m_observer.m_IsPlayerInRange == true)));
        m_patrolSelector.AddToChildren(patrolSequence);

        //Chasing Logic
        var chasingSequence = new SequenceNode("Chasing Player");
        chasingSequence.AddToChildren(new Leaf("Chasing Player", new ChasingStrategy(m_observer.player, m_agent, this)));

        m_mainNode.AddToChildren(m_patrolSelector);
        m_mainNode.AddToChildren(chasingSequence);

    }

    private void Update()
    {
        m_mainNode.Process(Time.deltaTime);
    }
}
