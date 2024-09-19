public class SequenceNode : CompositeNode
{
    private int m_index;
    public SequenceNode(string _name) : base(_name)
    {
    }

    public override STATUS Process(float _deltaTime)
    {
        for(var i = m_index; i< m_children.Count;i++)
        {
            var childStatus = m_children[m_index].Process(_deltaTime);
            switch (childStatus)
            {
                case STATUS.FAIL:
                    m_index = 0;
                    return STATUS.FAIL;
                case STATUS.SUCCESS:
                    m_index++;
                    break;
                case STATUS.RUNNING:
                    return STATUS.RUNNING;
            }
        }

        for(var i =0; i< m_children.Count; i++)
        {
            m_children[i].Reset();
        }
        m_index = 0;
        return STATUS.SUCCESS;

    }
}
