using DefaultNamespace;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    [SerializeField]
    private HexCell[] _neighbours;

    public HexCoordinates Coordinates;
    public Color CellColor;

    public HexCell GetNeighbour(HexDirection direction)
    {
        return _neighbours[(int) direction];
    }

    public void SetNeighbour(HexDirection direction, HexCell cell)
    {
        _neighbours[(int) direction] = cell;
        cell._neighbours[(int) direction.Opposite()] = this;
    }
}