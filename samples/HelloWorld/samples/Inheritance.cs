class MyClass
{
}

class Parent<T>
{
    public void WriteClassName() => Console.WriteLine(typeof(T).Name);
}

class Child : Parent<MyClass>
{
}

