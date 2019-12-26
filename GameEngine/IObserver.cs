namespace GameEngine
{
    public interface IObserver<T>
    {
        void Notify(T notification);
    }
}
