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

    private Canvas _canvas;
    private HexCell[] _cells;

    private void Awake()
    {
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

        CreateLabel(position, x, z, index);
    }

    private void CreateLabel(Vector3 position, int x, int z, int index)
    {
        Text label = Instantiate(_cellLabel);
        label.name = "Cell-Label " + index;
        label.rectTransform.SetParent(_canvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = x + "\n" + z;
    }
}