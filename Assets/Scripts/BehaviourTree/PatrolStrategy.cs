using UnityEngine;
using UnityEngine.AI;

internal class PatrolStrategy : IStrategy
{
    private NavMeshAgent agent;
    private Transform[] waypoints;
    private readonly GhostBehaviourTree ghostBehaviourTree;
    private readonly IFailConditions condtions;
    private int m_indexInPatrol;

    public PatrolStrategy(NavMeshAgent m_agent, Transform[] m_waypoints, GhostBehaviourTree ghostBehaviourTree)
    {
        agent = m_agent;
        waypoints = m_waypoints;
        this.ghostBehaviourTree = ghostBehaviourTree;
    }

    public Node.STATUS Process(float deltaTime)
    {
        Debug.Log("PatrolStrategy");
        ghostBehaviourTree.stamina += Time.deltaTime;
        ghostBehaviourTree.stamina = Mathf.Min(5, ghostBehaviourTree.stamina);
        agent.SetDestination(waypoints[m_indexInPatrol].position);
        agent.isStopped = false;
        if (Vector3.SqrMagnitude(agent.transform.position - waypoints[m_indexInPatrol].position) < 1f)
        {
            m_indexInPatrol++;
            if (m_indexInPatrol == waypoints.Length) 
            {
                m_indexInPatrol = 0;
                return Node.STATUS.SUCCESS;
            }

        }
        return Node.STATUS.RUNNING;

    }

    public void Reset()
    {
        m_indexInPatrol = 0;
    }
        
}