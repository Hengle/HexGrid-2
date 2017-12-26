using DefaultNamespace;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    [SerializeField]
    private int _width = 6;

    [SerializeField]
    private int _height = 6;

    [SerializeField]
    private HexCell _cellPrefab;

    [SerializeField]
    private Text _cellLabel;

    [SerializeField]
    private Color _defaultColor = Color.white;

    private HexMesh _hexMesh;
    private Canvas _canvas;
    private HexCell[] _cells;

    public void Awake()
    {
        _hexMesh = GetComponentInChildren<HexMesh>();
        _canvas = GetComponentInChildren<Canvas>();
        _cells = new HexCell[_height * _width];

        for (int z = 0, i = 0; z < _height; z++)
        {
            for (int x = 0; x < _width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    private void Start()
    {
        _hexMesh.Triangulate(_cells);
    }

    private void CreateCell(int x, int z, int index)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * HexMetrics.HorizontalDistance;
        position.y = 0f;
        position.z = z * HexMetrics.VerticalDistance;

        HexCell cell = _cells[index] = Instantiate(_cellPrefab);
        cell.name = "Cell " + index;
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.Coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.CellColor = _defaultColor;
        ConnectNeighbours(cell, x, z, index);
        CreateLabel(cell, index);
    }

    private void ConnectNeighbours(HexCell cell, int x, int z, int index)
    {
        if (x > 0)
        {
            cell.SetNeighbour(HexDirection.W, _cells[index - 1]);
        }
        if (z > 0)
        {
            // this bitwise AND operation determines if z is an even number
            if ((z & 1) == 0)
            {
                cell.SetNeighbour(HexDirection.SE, _cells[index - _width]);
                if (x > 0)
                {
                    cell.SetNeighbour(HexDirection.SW, _cells[index - _width - 1]);
                }
            }
            else
            {
                cell.SetNeighbour(HexDirection.SW, _cells[index - _width]);
                if (x < _width - 1)
                {
                    cell.SetNeighbour(HexDirection.SE, _cells[index - _width + 1]);
                }
            }
        }

    }

    private void CreateLabel(HexCell cell, int index)
    {
        Text label = Instantiate(_cellLabel);
        label.name = "Cell-Label " + index;
        label.rectTransform.SetParent(_canvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(cell.transform.localPosition.x, cell.transform.localPosition.z);
        label.text = cell.Coordinates.ToStringOnSeparateLines();
    }

    public void ColorCell(Vector3 position, Color color)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * _width + coordinates.Z / 2;
        HexCell cell = _cells[index];
        cell.CellColor = color;
        _hexMesh.Triangulate(_cells);
    }
}