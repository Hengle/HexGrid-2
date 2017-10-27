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
        position.x = x * 10f;
        position.y = 0f;
        position.z = z * 10f;

        HexCell cell = _cells[index] = Instantiate(_cellPrefab);
        cell.name = "Cell " + index;
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        CreateLabel(position.x, position.z, index);
    }

    private void CreateLabel(float posX, float posZ, int index)
    {
        Text label = Instantiate(_cellLabel);
        label.name = "Cell-Label " + index;
        label.rectTransform.SetParent(_canvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(posX, posZ);
        label.text = posX + "\n" + posZ;
    }
}