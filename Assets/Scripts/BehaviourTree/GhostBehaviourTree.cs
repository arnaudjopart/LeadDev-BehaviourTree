using System;
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
        chasingSequence.AddToChildren(new Leaf("Check if player is detected", new Condition(() => m_observer.m_IsPlayerInRange == true)));
        chasingSequence.AddToChildren(new Leaf("Chasing Player", new ChasingStrategy(m_observer.player, m_agent, this)));

        m_mainNode.AddToChildren(m_patrolSelector);
        m_mainNode.AddToChildren(chasingSequence);

    }

    private void Update()
    {
        Debug.Log("Update");
        m_mainNode.Process(Time.deltaTime);
    }
}

internal class TestFailCondition : IFailConditions
{
    private GhostBehaviourTree ghostBehaviourTree;

    public TestFailCondition(GhostBehaviourTree ghostBehaviourTree)
    {
        this.ghostBehaviourTree = ghostBehaviourTree;
    }

    public bool CheckConditions()
    {
        return ghostBehaviourTree.stamina < 0;
    }
}

internal class ChasingStrategy : IStrategy
{
    private Transform playerTransform;
    private NavMeshAgent agent;
    private readonly GhostBehaviourTree ghostBehaviourTree;

    public ChasingStrategy(Transform playerTransform, NavMeshAgent agent, GhostBehaviourTree ghostBehaviourTree)
    {
        this.playerTransform = playerTransform;
        this.agent = agent;
        this.ghostBehaviourTree = ghostBehaviourTree;
    }

    public Node.STATUS Process(float deltaTime)
    {

        ghostBehaviourTree.stamina -= deltaTime;
        if (ghostBehaviourTree.stamina < 0) return Node.STATUS.FAIL;
        agent.SetDestination(playerTransform.position);
        return Vector3.SqrMagnitude(playerTransform.position - agent.transform.position) < 1f ? Node.STATUS.SUCCESS : Node.STATUS.RUNNING;
    }

    public void Reset()
    {
        
    }
}

internal class AlwaysFalseNode : DecoratorNode
{
    private string v;

    public AlwaysFalseNode(string _name) : base(_name)
    {
    }

    public override STATUS Process(float _deltaTime)
    {

        if(m_child.Process(_deltaTime)== STATUS.RUNNING) return STATUS.RUNNING;
        return STATUS.FAIL;

    }
    

}

internal abstract class DecoratorNode : Node
{
    public Node m_child;
    public DecoratorNode(string _name) : base(_name)
    {
    }

    public void AddChild(Node child) => m_child = child;
}

internal class ActionStrategy : IStrategy
{
    private Action value;

    public ActionStrategy(Action value)
    {
        this.value = value;
    }

    public Node.STATUS Process(float deltaTime)
    {
        value();
        return Node.STATUS.SUCCESS;
    }

    public void Reset()
    {
        
    }
}

internal class WaitForSecondsStrategy : IStrategy
{
    private float m_duration;
    private float m_timer;

    public WaitForSecondsStrategy(float _duration)
    {
        this.m_duration = _duration;
        m_timer = m_duration;
    }

    public Node.STATUS Process(float deltaTime)
    {
        m_timer -= deltaTime;
        if (m_timer < 0)
        {
            return Node.STATUS.SUCCESS;
        }
        return Node.STATUS.RUNNING;
    }

    public void Reset()
    {
        m_timer = m_duration;
    }
}