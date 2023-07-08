using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurManager : MonoBehaviour
{
    [SerializeField] private int _focusLayer = 2;
    [SerializeField] private Material _focus, _blur1, _blur2;
    [SerializeField] private BoxCollider2D _boxCollider;

    private void OnEnable()
    {
        BlurrableObject.OnLayerChanged += OnObjectLayerChanged;
    }

    private void OnDisable()
    {
        BlurrableObject.OnLayerChanged -= OnObjectLayerChanged;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ChangeFocus(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeFocus(2);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeFocus(3);
        }
    }

    public void ChangeFocus(int layer)
    {
        _focusLayer = layer;
        List<BlurrableObject> blurrables = new List<BlurrableObject>();
        List<Collider2D> results = new List<Collider2D>();
        _boxCollider.OverlapCollider(new ContactFilter2D().NoFilter(), results);
        foreach (var result in results)
            if (result.TryGetComponent(out BlurrableObject blurrable))
                blurrables.Add(blurrable);
        foreach (var blurrable in blurrables)
        {
            SetBlur(blurrable);
        }
    }

    private void OnObjectLayerChanged(BlurrableObject blurrable)
    {
        SetBlur(blurrable);
    }

    private void SetBlur(BlurrableObject blurrable)
    {
        int difference = Mathf.Abs(_focusLayer - blurrable.FocusLayer);
        if (difference == 0)
        {
            blurrable.SetBlur(_focus);
        }
        else if (difference == 1)
        {
            blurrable.SetBlur(_blur1);
        }
        else
        {
            blurrable.SetBlur(_blur2);
        }
    }

    private void ClearBlur(BlurrableObject blurrable)
    {
        blurrable.SetBlur(_focus);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent(out BlurrableObject blurrable))
        {
            SetBlur(blurrable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BlurrableObject blurrable))
        {
            ClearBlur(blurrable);
        }
    }
}
