
using System;

// ==========================================
// FIRST PROJECT: Point3D
// ==========================================

class Point3D : IComparable, ICloneable
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    // Constructors with chaining
    public Point3D() : this(0, 0, 0)
    {
    }

    public Point3D(int x, int y) : this(x, y, 0)
    {
    }

    public Point3D(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    // Override ToString
    public override string ToString()
    {
        return $"Point Coordinates: ({X}, {Y}, {Z})";
    }

    // Compare points based on X, then Y, then Z
    public int CompareTo(object obj)
    {
        if (obj == null)
            return 1;

        if (obj is not Point3D other)
            throw new ArgumentException("Object must be Point3D");

        int result = X.CompareTo(other.X);

        if (result == 0)
            result = Y.CompareTo(other.Y);

        if (result == 0)
            result = Z.CompareTo(other.Z);

        return result;
    }

    // Implement ICloneable
    public object Clone()
    {
        return new Point3D(X, Y, Z);
    }

    // Override Equals
    public override bool Equals(object obj)
    {
        if (obj is not Point3D other)
            return false;

        return X == other.X &&
               Y == other.Y &&
               Z == other.Z;
    }

    // Override GetHashCode
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    // Overload == operator
    public static bool operator ==(Point3D p1, Point3D p2)
    {
        if (ReferenceEquals(p1, p2))
            return true;

        if (p1 is null || p2 is null)
            return false;

        return p1.Equals(p2);
    }

    public static bool operator !=(Point3D p1, Point3D p2)
    {
        return !(p1 == p2);
    }
}


// ==========================================
// SECOND PROJECT: Maths
// ==========================================

class Maths
{
    public static int Add(int a, int b)
    {
        return a + b;
    }

    public static int Subtract(int a, int b)
    {
        return a - b;
    }

    public static int Multiply(int a, int b)
    {
        return a * b;
    }

    public static double Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException();

        return (double)a / b;
    }
}


// ==========================================
// THIRD PROJECT: Duration
// ==========================================

class Duration
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }

    // Calculate total seconds
    private int TotalSeconds
    {
        get
        {
            return Hours * 3600 + Minutes * 60 + Seconds;
        }
    }

    // Constructors
    public Duration() : this(0)
    {
    }

    public Duration(int hours, int minutes, int seconds)
    {
        int total = hours * 3600 + minutes * 60 + seconds;

        SetDuration(total);
    }

    public Duration(int totalSeconds)
    {
        SetDuration(totalSeconds);
    }

    // Convert total seconds into Hours, Minutes, Seconds
    private void SetDuration(int totalSeconds)
    {
        if (totalSeconds < 0)
            throw new ArgumentException("Duration cannot be negative");

        Hours = totalSeconds / 3600;
        Minutes = (totalSeconds % 3600) / 60;
        Seconds = totalSeconds % 60;
    }

    // Override ToString
    public override string ToString()
    {
        if (Hours > 0)
        {
            return $"Hours: {Hours}, Minutes: {Minutes}, Seconds: {Seconds}";
        }

        return $"Minutes: {Minutes}, Seconds: {Seconds}";
    }

    // Override Equals
    public override bool Equals(object obj)
    {
        if (obj is not Duration other)
            return false;

        return TotalSeconds == other.TotalSeconds;
    }

    // Override GetHashCode
    public override int GetHashCode()
    {
        return TotalSeconds.GetHashCode();
    }

    // ======================================
    // Operator Overloading
    // ======================================

    // Duration + Duration
    public static Duration operator +(Duration d1, Duration d2)
    {
        return new Duration(d1.TotalSeconds + d2.TotalSeconds);
    }

    // Duration + int
    public static Duration operator +(Duration d, int seconds)
    {
        return new Duration(d.TotalSeconds + seconds);
    }

    // int + Duration
    public static Duration operator +(int seconds, Duration d)
    {
        return new Duration(seconds + d.TotalSeconds);
    }

    // Duration - Duration
    public static Duration operator -(Duration d1, Duration d2)
    {
        return new Duration(d1.TotalSeconds - d2.TotalSeconds);
    }

    // Increment one minute
    public static Duration operator ++(Duration d)
    {
        return new Duration(d.TotalSeconds + 60);
    }

    // Decrement one minute
    public static Duration operator --(Duration d)
    {
        return new Duration(d.TotalSeconds - 60);
    }

    // Greater than
    public static bool operator >(Duration d1, Duration d2)
    {
        return d1.TotalSeconds > d2.TotalSeconds;
    }

    // Less than
    public static bool operator <(Duration d1, Duration d2)
    {
        return d1.TotalSeconds < d2.TotalSeconds;
    }

    // Greater than or equal
    public static bool operator >=(Duration d1, Duration d2)
    {
        return d1.TotalSeconds >= d2.TotalSeconds;
    }

    // Less than or equal
    public static bool operator <=(Duration d1, Duration d2)
    {
        return d1.TotalSeconds <= d2.TotalSeconds;
    }

    // Equal
    public static bool operator ==(Duration d1, Duration d2)
    {
        if (ReferenceEquals(d1, d2))
            return true;

        if (d1 is null || d2 is null)
            return false;

        return d1.Equals(d2);
    }

    // Not equal
    public static bool operator !=(Duration d1, Duration d2)
    {
        return !(d1 == d2);
    }

    // Allow if (D1)
    public static implicit operator bool(Duration d)
    {
        return d != null && d.TotalSeconds > 0;
    }

    // Convert Duration to DateTime
    public static explicit operator DateTime(Duration d)
    {
        return DateTime.Today.AddSeconds(d.TotalSeconds);
    }
}


// ==========================================
// MAIN PROGRAM
// ==========================================

class Program
{
    static void Main()
    {
        // ==================================
        // FIRST PROJECT
        // ==================================

        Console.WriteLine("===== FIRST PROJECT: Point3D =====");

        // Create Point using constructor
        Point3D P = new Point3D(10, 10, 10);

        Console.WriteLine(P.ToString());

        // Read first point
        Console.WriteLine("\nEnter coordinates for P1:");

        Point3D P1 = ReadPoint();

        // Read second point
        Console.WriteLine("\nEnter coordinates for P2:");

        Point3D P2 = ReadPoint();

        // Compare points using ==
        Console.WriteLine("\nAre P1 and P2 equal?");
        Console.WriteLine(P1 == P2);

        // Create array of points
        Point3D[] points =
        {
            new Point3D(5, 3, 1),
            new Point3D(2, 8, 4),
            new Point3D(2, 4, 6),
            new Point3D(1, 9, 2)
        };

        // Sort array
        Array.Sort(points);

        Console.WriteLine("\nSorted Points:");

        foreach (Point3D point in points)
        {
            Console.WriteLine(point);
        }

        // Clone object
        Point3D clonedPoint = (Point3D)P.Clone();

        Console.WriteLine("\nCloned Point:");
        Console.WriteLine(clonedPoint);


        // ==================================
        // SECOND PROJECT
        // ==================================

        Console.WriteLine("\n===== SECOND PROJECT: Maths =====");

        // Call static methods without creating object

        Console.WriteLine("Add: " + Maths.Add(10, 5));

        Console.WriteLine("Subtract: " + Maths.Subtract(10, 5));

        Console.WriteLine("Multiply: " + Maths.Multiply(10, 5));

        Console.WriteLine("Divide: " + Maths.Divide(10, 5));


        // ==================================
        // THIRD PROJECT
        // ==================================

        Console.WriteLine("\n===== THIRD PROJECT: Duration =====");

        // Constructors

        Duration D1 = new Duration(1, 10, 15);

        Console.WriteLine("D1: " + D1);

        Duration D2 = new Duration(7800);

        Console.WriteLine("D2: " + D2);

        Duration D3 = new Duration(666);

        Console.WriteLine("D3: " + D3);

        Duration D4 = new Duration(3600);

        Console.WriteLine("D4: " + D4);


        // Operator Overloading

        // D3 = D1 + D2
        D3 = D1 + D2;

        Console.WriteLine("\nD1 + D2 = " + D3);

        // D3 = D1 + 7800
        D3 = D1 + 7800;

        Console.WriteLine("D1 + 7800 = " + D3);

        // D3 = 666 + D3
        D3 = 666 + D3;

        Console.WriteLine("666 + D3 = " + D3);

        // D3 = ++D1 (Increase one minute)
        D3 = ++D1;

        Console.WriteLine("++D1 = " + D3);

        // D3 = --D2 (Decrease one minute)
        D3 = --D2;

        Console.WriteLine("--D2 = " + D3);

        // D1 = D1 - D2
        D1 = D1 - D2;

        Console.WriteLine("D1 - D2 = " + D1);

        // If (D1 > D2)
        if (D1 > D2)
        {
            Console.WriteLine("D1 is greater than D2");
        }

        // If (D1 <= D2)
        if (D1 <= D2)
        {
            Console.WriteLine("D1 is less than or equal to D2");
        }

        // If (D1)
        if (D1)
        {
            Console.WriteLine("D1 is not zero");
        }
        else
        {
            Console.WriteLine("D1 is zero");
        }

        // Convert Duration to DateTime
        DateTime Obj = (DateTime)D1;

        Console.WriteLine("DateTime: " + Obj);


        // Test Equals and GetHashCode
        Console.WriteLine("\nD1 Equals D2: " + D1.Equals(D2));

        Console.WriteLine("D1 HashCode: " + D1.GetHashCode());
    }


    // ==================================
    // Read Point from User
    // ==================================

    static Point3D ReadPoint()
    {
        int x, y, z;

        while (true)
        {
            Console.Write("Enter X: ");

            if (int.TryParse(Console.ReadLine(), out x))
                break;

            Console.WriteLine("Invalid input! Enter an integer.");
        }

        while (true)
        {
            Console.Write("Enter Y: ");

            if (int.TryParse(Console.ReadLine(), out y))
                break;

            Console.WriteLine("Invalid input! Enter an integer.");
        }

        while (true)
        {
            Console.Write("Enter Z: ");

            if (int.TryParse(Console.ReadLine(), out z))
                break;

            Console.WriteLine("Invalid input! Enter an integer.");
        }

        return new Point3D(x, y, z);
    }
}
