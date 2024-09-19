using System;

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
