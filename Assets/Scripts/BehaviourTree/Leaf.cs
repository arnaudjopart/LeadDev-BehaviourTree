using UnityEngine;

public class Leaf : Node
{
    private IStrategy m_stategy;

    public Leaf(string _name, IStrategy _strategy) : base(_name)
    {
        m_stategy = _strategy;
    }

    public override STATUS Process(float _deltaTime)
    {
        return m_stategy.Process(_deltaTime);
    }

    public override void Reset()
    {
        base.Reset();
        m_stategy.Reset();
    }
}

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