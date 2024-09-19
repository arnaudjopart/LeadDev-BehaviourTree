using UnityEngine;
using UnityEngine.AI;

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
