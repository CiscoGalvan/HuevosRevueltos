using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(SpriteBillboard))]
public class InitializeEmergentObject3D : MonoBehaviour
{
    Animator animator;
    BoxCollider boxCollider;
    MeshRenderer MeshRenderer;
    MeshFilter MeshFilter;
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
        MeshRenderer = GetComponent<MeshRenderer>();
        MeshRenderer.enabled = false;
        MeshFilter = GetComponent<MeshFilter>();
        animator.runtimeAnimatorController =  Resources.Load<RuntimeAnimatorController>("InitializeEmergentObjectAnimator");
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("InitializeEmergentObjectAnimation");
        boxCollider = GetComponent<BoxCollider>();
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    public void ActivateObject()
    {
        boxCollider.enabled = true;
        MeshRenderer.enabled = true;
    }
}
