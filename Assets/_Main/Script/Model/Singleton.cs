using System;

public abstract class Singleton<T> : IDisposable where T :class, new()
{
    private static T _instance;
    
    public static T I => _instance ??= new T();

    ~Singleton()
    {
        Dispose();
    }

    public virtual void Dispose()
    {
        _instance = null;
    }
}
