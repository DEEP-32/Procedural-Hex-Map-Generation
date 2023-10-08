using UnityEngine;
using static HexMetrics;

public class HexCell : MonoBehaviour
{
    int elevation;
    public int Elevation
    {
        get
        {
            return elevation;
        }
        set
        {
            elevation = value;
            Vector3 position = transform.localPosition;
            position.y = value * HexMetrics.elevationStep;
            transform.localPosition = position;

            Vector3 uiPosition = uiRect.localPosition;
            uiPosition.z = elevation * -HexMetrics.elevationStep;
            uiRect.localPosition = uiPosition;
        }
    }

    public HexCordinates cordinate;
    public Color color;
    public RectTransform uiRect;

    [SerializeField]
    HexCell[] neighbours;

    public HexCell GetNeighbour(HexDirection direction)
    {
        return neighbours[(int)direction];
    }

    public void SetNeighbour(HexDirection direction, HexCell cell)
    {
        neighbours[(int)direction] = cell;
        cell.neighbours[(int)direction.Opposite()] = this;
    }

    public HexEdgeType GetEdgeType(HexDirection dir)
    {
        return HexMetrics.GetEdgeType(
              elevation, neighbours[(int)dir].elevation
            );
    }

}
