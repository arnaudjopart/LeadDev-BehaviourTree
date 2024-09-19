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
