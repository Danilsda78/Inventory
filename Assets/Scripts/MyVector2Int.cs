using System;

[Serializable]
public struct MyVector2Int
{
    public int x;
    public int y;

    public MyVector2Int(int X, int Y)
    {
        x = X;
        y = Y;
    }

    public static MyVector2Int operator -(MyVector2Int v)
    {
        return new MyVector2Int(-v.x, -v.y);
    }

    public static MyVector2Int operator +(MyVector2Int a, MyVector2Int b)
    {
        return new MyVector2Int(a.x + b.x, a.y + b.y);
    }

    public static MyVector2Int operator -(MyVector2Int a, MyVector2Int b)
    {
        return new MyVector2Int(a.x - b.x, a.y - b.y);
    }

    public static MyVector2Int operator *(MyVector2Int a, MyVector2Int b)
    {
        return new MyVector2Int(a.x * b.x, a.y * b.y);
    }

    public static MyVector2Int operator *(int a, MyVector2Int b)
    {
        return new MyVector2Int(a * b.x, a * b.y);
    }

    public static MyVector2Int operator *(MyVector2Int a, int b)
    {
        return new MyVector2Int(a.x * b, a.y * b);
    }

    public static MyVector2Int operator /(MyVector2Int a, int b)
    {
        return new MyVector2Int(a.x / b, a.y / b);
    }

    public static bool operator ==(MyVector2Int lhs, MyVector2Int rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }
    public static bool operator !=(MyVector2Int lhs, MyVector2Int rhs)
    {
        return !(lhs == rhs);
    }

    public override bool Equals(object obj)
    {
        return obj is MyVector2Int @int &&
               x == @int.x &&
               y == @int.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }
}
