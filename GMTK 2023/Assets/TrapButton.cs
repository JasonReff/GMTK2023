using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrapButton : MonoBehaviour
{
    [SerializeField] private TrapData _trapData;
    [SerializeField] private Image _trapIcon;
    [SerializeField] private TextMeshProUGUI _trapName;

    public static event Action<TrapData> OnTrapSelected;

    private void OnEnable()
    {
        OnTrapSelected += TrapSelected;
    }

    private void OnDisable()
    {
        OnTrapSelected -= TrapSelected;
    }

    public void Initialize(TrapData data)
    {
        _trapData = data;
        _trapIcon.sprite = _trapData.TrapSprite;
        _trapName.text = $"{_trapData.TrapName} ({_trapData.TrapCost})";
    }

    public void SelectTrap()
    {
        
    }

    public void TrapSelected(TrapData trapData)
    {
        SetHighlight(trapData == _trapData);
    }

    public void SetHighlight(bool toggle)
    {

    }
}
