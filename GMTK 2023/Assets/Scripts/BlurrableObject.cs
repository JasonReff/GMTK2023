using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlurrableObject : MonoBehaviour
{
    [SerializeField] private int _focusLayer = 1;
    private SpriteRenderer _spriteRenderer;

    public int FocusLayer { get => _focusLayer; }

    public static event Action<BlurrableObject> OnLayerChanged;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeLayer(int layer)
    {
        _focusLayer = layer;
        OnLayerChanged?.Invoke(this);
    }

    public void SetBlur(Material material)
    {
        _spriteRenderer.material = material;
    }
}