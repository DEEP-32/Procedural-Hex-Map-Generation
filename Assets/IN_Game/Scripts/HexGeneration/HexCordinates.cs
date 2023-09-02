using UnityEngine;

[System.Serializable]
public struct HexCordinates
{

    [SerializeField]
    private int x, z;
    public int X
    {
        get
        {
            return x;
        }
        private set
        {
            x = value;
        }
    }

    public int Z
    {
        get
        {
            return z;
        }
        private set
        {
            z = value;
        }
    }

    public int Y
    {
        get
        {
            return -X -Z;
        }
    }

    public HexCordinates(int x,int z)
    {
        this.x = x;
        this.z = z;
    }

    public static HexCordinates FromOffsetCordinate(int x,int z)
    {
        return new HexCordinates(x - z/2,z);
    }

    public static HexCordinates FromPosition(Vector3 position)
    {
        float x = position.x / (HexMetrics.innerRadius * 2f);
        float y = -x;

        float offset = position.z / (HexMetrics.outerRadius * 3f);
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x - y - iZ);

            if (dX > dY && dX > dZ)
            {
                iX = -iY - iZ;
            }
            else if (dZ > dY)
            {
                iZ = -iX - iY;
            }
        }

        return new HexCordinates(iX, iZ);
    }

    public override string ToString()
    {
        return $"({X} ,{Y} , {Z})";
    }

    public string ToStringOnSepararteLines()
    {
        return $"{X}\n{Y}\n{Z}";
    }
}