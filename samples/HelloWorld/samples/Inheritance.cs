class Base<T>
{
    public void WriteClassName() => Console.WriteLine(typeof(T).Name);
}

class Foo : Base<Foo>
{
}

class Bar
{
}
