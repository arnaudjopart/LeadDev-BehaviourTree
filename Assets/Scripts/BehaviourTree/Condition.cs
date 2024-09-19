using System;

internal class Condition : IStrategy
{
    private Func<bool> value;

    public Condition(Func<bool> value)
    {
        this.value = value;
    }

    public Node.STATUS Process(float deltaTime)
    {
        return value() ? Node.STATUS.SUCCESS : Node.STATUS.FAIL;
    }

    public void Reset()
    {
       
    }
}