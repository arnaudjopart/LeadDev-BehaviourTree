namespace ajc.BehaviourTree
{
    public class Selector : CompositeNode
    {
        private int _index;

        public Selector(string _name) : base(_name)
        {
        }

        public override STATUS Process(float _deltaTime)
        {
            var status = m_children[_index].Process(_deltaTime);
            switch (status)
            {
                case STATUS.Running:
                    return STATUS.Running;
                case STATUS.Success:
                    _index = 0;
                    return STATUS.Success;
                case STATUS.Failure:
                    _index++;
                    if (_index >= m_children.Count)
                    {
                        _index = 0;
                        return STATUS.Failure;
                    }
                    break;
            }
            return STATUS.Running;
        }
    }
}