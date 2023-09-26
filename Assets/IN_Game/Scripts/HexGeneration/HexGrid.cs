using TMPro;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [SerializeField] int width = 6;
    [SerializeField] int height = 6;
    public int Width { get => width; private set => width = value; }
    public int Height { get => height; private set => height = value; }

    public HexCell cellPrefab;

    [SerializeField] Color defaultColor = Color.white;

    public Color DefautColor
    {
        get => defaultColor;
        private set => defaultColor = value;
    }


    HexCell[] cells;


    [SerializeField] TextMeshProUGUI cellLabelPrefab;
    Canvas gridCanvas;


    HexMesh hexMesh;


    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        cells =  new HexCell[height * width];

        for (int z = 0, cellIndex = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, cellIndex++);
            }
        }
    }


    private void Start()
    {
        hexMesh.Triangulate(cells);
    }

    void CreateCell(int x, int z, int cellIndex)
    {
        Vector3 position;
        position.x = (x + z * .5f - z/2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[cellIndex] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.cordinate = HexCordinates.FromOffsetCordinate(x, z);
        cell.color = defaultColor;


        if(x > 0)
        {
            cell.SetNeighbour(HexDirection.W, cells[cellIndex - 1]);
        }

        if(z > 0)
        {
            if((z&1) == 0)
            {
                cell.SetNeighbour(HexDirection.SE, cells[cellIndex - width]);
                if(x > 0)
                {
                    cell.SetNeighbour(HexDirection.SW, cells[cellIndex - width-1]);
                }
            }
            else
            {
                cell.SetNeighbour(HexDirection.SW, cells[cellIndex - width]);
                if(x < width - 1)
                {
                    cell.SetNeighbour(HexDirection.SE, cells[cellIndex - width + 1]);
                }
            }
        }

        TextMeshProUGUI label = Instantiate(cellLabelPrefab);

        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x,position.z);

        label.text = cell.cordinate.ToStringOnSepararteLines();

        cell.uiRect = label.rectTransform;
    }

    

    public HexCell GetCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);

        HexCordinates cordinates = HexCordinates.FromPosition(position);
        //Debug.Log("touched at " + cordinates.ToString());

        int index = cordinates.X + cordinates.Z * width + cordinates.Z /2;
        return cells[index];
    }

    public void Refresh()
    {
        hexMesh.Triangulate(cells);
    }

}