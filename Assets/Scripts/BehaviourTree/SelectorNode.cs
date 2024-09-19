public class SelectorNode : CompositeNode
{
    private int m_index;
    public SelectorNode(string _name) : base(_name)
    {
    }

    public override STATUS Process(float _deltaTime)
    {

        var childStatus = m_children[m_index].Process(_deltaTime);
        
        switch (childStatus)
        {
            case STATUS.FAIL:
                m_index++;
                if(m_index >= m_children.Count)
                {
                    m_index = 0;
                    return STATUS.FAIL;
                }
                break;
            case STATUS.SUCCESS:
                m_index = 0;
                return STATUS.SUCCESS;
        }
        return STATUS.RUNNING;
     }
}


public class CheckAllSelectorNode : CompositeNode
{
    private int m_index;
    public CheckAllSelectorNode(string _name) : base(_name)
    {
    }

    public override STATUS Process(float _deltaTime)
    {
        for(int i = 0; i < m_children.Count; i++)
        {
            var childStatus = m_children[i].Process(_deltaTime);

            switch (childStatus)
            {
                case STATUS.RUNNING:
                    return STATUS.RUNNING;
                case STATUS.SUCCESS:
                    return STATUS.SUCCESS;
            }
            
        }
        return STATUS.FAIL;

    }
}