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