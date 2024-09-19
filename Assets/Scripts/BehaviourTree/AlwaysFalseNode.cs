internal class AlwaysFalseNode : DecoratorNode
{
    private string v;

    public AlwaysFalseNode(string _name) : base(_name)
    {
    }

    public override STATUS Process(float _deltaTime)
    {

        if(m_child.Process(_deltaTime)== STATUS.RUNNING) return STATUS.RUNNING;
        return STATUS.FAIL;

    }
    

}
