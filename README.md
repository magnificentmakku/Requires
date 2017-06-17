# Overview

This is a simple proof of concept for a Contract / Requires API that grabs the correct parameter names from argument expressions passed into it and allows for fluent method chaining.

Typical null guard:

```csharp
public void Method(object theObject)
{
    if (str == null)
        throw new ArgumentNullException("theObject");
    
    // ...
}
```

Utilizing this API:

```csharp
public void Method(object theObject)
{
    Requires.NotNull(() => theObject);

    // ...
}
```

Multiple requirements may be chained in a fluent way:

```csharp
public void Method(int x, int y, int z)
{
    Requires.Require(() => x).GreaterThan(() => y).AndLessThan(() => z);
}
```