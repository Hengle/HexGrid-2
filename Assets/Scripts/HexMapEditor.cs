using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class HexMapEditor : MonoBehaviour
    {
        public Color[] Colors;

        public HexGrid HexGrid;

        private Color _activeColor;

        private void Awake()
        {
            SelectColor(0);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                HandleInput();    
            }
        }

        public void SelectColor(int index)
        {
            _activeColor = Colors[index];
        }

        private void HandleInput()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(inputRay, out hit))
            {
                HexGrid.ColorCell(hit.point, _activeColor);
            }
        }
    }
}