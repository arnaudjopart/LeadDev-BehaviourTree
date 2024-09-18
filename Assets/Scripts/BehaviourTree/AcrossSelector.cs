namespace ajc.BehaviourTree
{
    public class AcrossSelector : CompositeNode
    {
        public AcrossSelector(string _name) : base(_name)
        {
        }

        public override STATUS Process(float _deltaTime)
        {
            foreach (Node _node in m_children)
            {
                switch (_node.Process(_deltaTime))
                {
                    case STATUS.Success:
                        return STATUS.Success;
                    case STATUS.Running: return STATUS.Running;
                }
            }
            return STATUS.Failure;
        }
    }
}