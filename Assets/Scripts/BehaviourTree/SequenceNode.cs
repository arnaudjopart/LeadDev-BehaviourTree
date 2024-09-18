namespace ajc.BehaviourTree
{
    public class SequenceNode : CompositeNode
    {

        private int m_index;
    
        public override STATUS Process(float _deltaTime)
        {
            var result = Node.STATUS.PENDING;

            while(result == STATUS.PENDING)
            {
                var childProgressStatus = m_children[m_index].Process(_deltaTime);
                switch (childProgressStatus)
                {
                    case STATUS.Failure:
                        m_index = 0;
                        result = STATUS.Failure;
                        break;
                    case STATUS.Success:
                        m_index += 1;
                        if (m_index >= m_children.Count)
                        {
                            m_index = 0;
                            result = STATUS.Success;
                        }
                        break;
                    default:
                        result = STATUS.Running;
                        break;
                }
            }
            return result;
        }

        public SequenceNode(string _name) : base(_name)
        {
        }
    }

    public class AcrossSequenceNode : CompositeNode
    {

        private int m_index;

        public override STATUS Process(float _deltaTime)
        {
            foreach(var child in m_children)
            {
                var childProgressStatus = child.Process(_deltaTime);
                switch (childProgressStatus)
                {
                    case STATUS.Failure:
                        m_index = 0;
                        return STATUS.Failure;
                    case STATUS.Success:
                        {
                            m_index += 1;
                            if (m_index < m_children.Count) continue;
                            m_index = 0;
                            return STATUS.Success;
                        }
                    default:
                        m_index = 0;
                        return STATUS.Running;
                }
            }
            
            return STATUS.Running;
            
        }

        public AcrossSequenceNode(string _name) : base(_name)
        {
        }
    }
}