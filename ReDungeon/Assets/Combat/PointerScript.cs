using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite ActivePointer;
    [SerializeField]
    private Sprite InactivePointer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = InactivePointer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(bool enabled)
    {
        if (enabled)
            spriteRenderer.sprite = ActivePointer;
        else
        spriteRenderer.sprite = InactivePointer;
    }
}
