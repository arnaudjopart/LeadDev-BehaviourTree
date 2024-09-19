public interface IStrategy
{
    Node.STATUS Process(float deltaTime);
    void Reset();
}