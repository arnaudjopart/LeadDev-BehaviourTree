internal abstract class DecoratorNode : Node
{
    public Node m_child;
    public DecoratorNode(string _name) : base(_name)
    {
    }

    public void AddChild(Node child) => m_child = child;
}
