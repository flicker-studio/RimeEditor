public interface IWritable<T> : IGameData
{
    void WriteData(T data);
}
