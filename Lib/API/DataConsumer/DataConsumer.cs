namespace Lib.API;

public interface IDataConsumer<in T>
{
    void Consume(T data);
}