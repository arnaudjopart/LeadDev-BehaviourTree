using System;

public abstract class Node
{
    private readonly string m_name;

    public enum STATUS
    {
        FAIL,
        SUCCESS,
        RUNNING
    }
    public Node(string _name)
    {
        m_name = _name;
    }

    public abstract STATUS Process(float _deltaTime);

    public virtual void Reset()
    {
        
    }
}

public interface IStrategy
{
    Node.STATUS Process(float deltaTime);
    void Reset();
}