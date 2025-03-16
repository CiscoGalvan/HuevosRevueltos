using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SpriteBillboard))]
public class InitializeEmergentObject2D : MonoBehaviour
{
    Animator animator;
    BoxCollider boxCollider;
    SpriteRenderer spriteRenderer;
    SpriteRenderer spriteRendererShadow;

    private void Reset()
    {
        if (transform.Find("WaterShadow") == null)
        {
            GameObject child = new GameObject("WaterShadow");
            spriteRendererShadow = child.AddComponent<SpriteRenderer>();
            animator = child.AddComponent<Animator>();
            child.AddComponent<SpriteBillboard>();
            child.AddComponent<Emerge>();
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
        }
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        animator.runtimeAnimatorController =  Resources.Load<RuntimeAnimatorController>("InitializeEmergentObjectAnimator");
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("InitializeEmergentObjectAnimation");
        boxCollider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ActivateObject()
    {
        spriteRenderer.enabled = true;
    }
}
