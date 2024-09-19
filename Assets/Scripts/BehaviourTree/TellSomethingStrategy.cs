using UnityEngine;

internal class TellSomethingStrategy : IStrategy
{
    private string m_text;

    public TellSomethingStrategy(string v)
    {
        this.m_text = v;
    }

    public Node.STATUS Process(float deltaTime)
    {
        Debug.Log(m_text);
        return Node.STATUS.SUCCESS;
    }

    public void Reset()
    {
        
    }
}