using UnityEngine;

public class TrapPlacer : MonoBehaviour
{
    [SerializeField] private TrapData _selectedTrap;
    [SerializeField] private int _money;


    private void OnEnable()
    {
        TrapButton.OnTrapSelected += OnTrapSelected;
    }

    private void OnDisable()
    {
        TrapButton.OnTrapSelected -= OnTrapSelected;
    }

    private void OnTrapSelected(TrapData data)
    {
        if (_money >= data.TrapCost)
        {
            _selectedTrap = data;
        }
    }

    private void OnTileSelected()
    {

    }

    private void PurchaseTrap(PlaceableTile tile)
    {

    }
}
