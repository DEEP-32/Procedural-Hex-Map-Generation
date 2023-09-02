using TMPro;
using UnityEngine;

public class HexGrid : MonoBehaviour
{

    public int width = 6;
    public int height = 6;

    public HexCell cellPrefab;


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

        TextMeshProUGUI label = Instantiate(cellLabelPrefab);

        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x,position.z);

        label.text = cell.cordinate.ToStringOnSepararteLines();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            TouchCell(hit.point);
        }
    }

    void TouchCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);

        HexCordinates cordinates = HexCordinates.FromPosition(position);
        Debug.Log("touched at " + cordinates.ToString());
    }

}