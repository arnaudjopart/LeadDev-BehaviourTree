using System.Collections.Generic;

public abstract class CompositeNode : Node
{
    public List<Node> m_children;
    protected CompositeNode(string _name) : base(_name)
    {
        m_children = new List<Node>();
            }

    public void AddToChildren(Node _child)
    {
        m_children.Add(_child);
    }
}
