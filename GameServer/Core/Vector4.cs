#region Assembly Bootstrapper, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// C:\Users\Momo\.nuget\packages\ragemp-bootstrapper\1.1.3\lib\netcoreapp3.1\Bootstrapper.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace GTANetworkAPI
{
    [StructLayout(LayoutKind.Sequential)]
    public class Vector4
    {
        private static readonly Random randInstance = new Random();

        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }

        [JsonProperty("z")]
        public float Z { get; set; }

        [JsonProperty("w")]
        public float W { get; set; }

        [JsonIgnore]
        public Vector4 Normalized
        {
            get
            {
                float num = Length();
                return new Vector4(X / num, Y / num, Z / num, W / num);
            }
        }

        public Vector4()
        {
            X = 0f;
            Y = 0f;
            Z = 0f;
            W = 0f;
        }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4(double x, double y, double z, double w)
        {
            X = (float)x;
            Y = (float)y;
            Z = (float)z;
            W = (float)w;
        }

        public Vector4(Vector3 position, float w)
        {
            X = (float)position.X;
            Y = (float)position.Y;
            Z = (float)position.Z;
            W = (float)w;
        }
        public IntPtr ToUnmanaged()
        {
            IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, intPtr, fDeleteOld: true);
            return intPtr;
        }

        public static bool operator ==(Vector4 left, Vector4 right)
        {
            if ((object)left == null && (object)right == null)
            {
                return true;
            }

            if ((object)left == null || (object)right == null)
            {
                return false;
            }

            if (left.X == right.X && left.Y == right.Y)
            {
                return left.Z == right.Z;
            }

            return false;
        }

        public static bool operator !=(Vector4 left, Vector4 right)
        {
            if ((object)left == null && (object)right == null)
            {
                return false;
            }

            if ((object)left == null || (object)right == null)
            {
                return true;
            }

            if (left.X == right.X && left.Y == right.Y)
            {
                return left.Z != right.Z;
            }

            return true;
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            if ((object)left == null || (object)right == null)
            {
                return new Vector4();
            }

            return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            if ((object)left == null || (object)right == null)
            {
                return new Vector4();
            }

            return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Vector4 operator *(Vector4 left, float right)
        {
            if ((object)left == null)
            {
                return new Vector4();
            }

            return new Vector4(left.X * right, left.Y * right, left.Z * right, left.W * right);
        }

        public static Vector4 operator /(Vector4 left, float right)
        {
            if ((object)left == null)
            {
                return new Vector4();
            }

            return new Vector4(left.X / right, left.Y / right, left.Z / right, left.W / right);
        }

        public static Vector4 Lerp(Vector4 start, Vector4 end, float n)
        {
            return new Vector4
            {
                X = start.X + (end.X - start.X) * n,
                Y = start.Y + (end.Y - start.Y) * n,
                Z = start.Z + (end.Z - start.Z) * n,
                W = start.W + (end.W - start.W) * n,
            };
        }

        public static float Distance(Vector4 a, Vector4 b)
        {
            return a.DistanceTo(b);
        }

        public static float DistanceSquared(Vector4 a, Vector4 b)
        {
            return a.DistanceToSquared(b);
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z} {W}";
        }

        public string ToStringComma()
        {
            return $"{X}, {Y}, {Z}, {W}";
        }

        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z + W * W;
        }

        public float Length()
        {
            return (float)Math.Sqrt(LengthSquared());
        }

        public void Normalize()
        {
            float num = Length();
            X /= num;
            Y /= num;
            Z /= num;
            W /= num;
        }

        public Vector4 Add(Vector4 right)
        {
            return this + right;
        }

        public Vector4 Subtract(Vector4 right)
        {
            return this - right;
        }

        public Vector4 Multiply(float right)
        {
            return this * right;
        }

        public Vector4 Divide(float right)
        {
            return this / right;
        }

        public Vector4 Copy()
        {
            return new Vector4(X, Y, Z, W);
        }

        public static Vector4 RandomXy()
        {
            Vector4 vector = new Vector4();
            double num = randInstance.NextDouble() * 2.0 * Math.PI;
            vector.X = (float)Math.Cos(num);
            vector.Y = (float)Math.Sin(num);
            vector.Normalize();
            return vector;
        }

        public Vector4 Around(float distance)
        {
            return this + RandomXy() * distance;
        }

        public float DistanceToSquared(Vector4 right)
        {
            if ((object)right == null)
            {
                return 0f;
            }

            float num = X - right.X;
            float num2 = Y - right.Y;
            float num3 = Z - right.Z;
            float num4 = W - right.W;
            return num * num + num2 * num2 + num3 * num3 + num4 * num4;
        }

        public float DistanceTo(Vector4 right)
        {
            if ((object)right == null)
            {
                return 0f;
            }

            return (float)Math.Sqrt(DistanceToSquared(right));
        }

        public float DistanceToSquared2D(Vector4 right)
        {
            if ((object)right == null)
            {
                return 0f;
            }

            float num = X - right.X;
            float num2 = Y - right.Y;
            return num * num + num2 * num2;
        }

        public float DistanceTo2D(Vector4 right)
        {
            if ((object)right == null)
            {
                return 0f;
            }

            return (float)Math.Sqrt(DistanceToSquared2D(right));
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

    }
}