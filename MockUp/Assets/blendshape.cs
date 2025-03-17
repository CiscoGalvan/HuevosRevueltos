using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blendshape : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh mesh;
    int blendShapeCount;
    int currentBlendShape = 0;
    float blendWeight = 0f;
    [SerializeField]
    float speed = 50f; // Velocidad de la transici�n

    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        mesh = skinnedMeshRenderer.sharedMesh;
        blendShapeCount = mesh.blendShapeCount;
    }

    void Update()
    {
        if (blendShapeCount == 0) return;

        // Reducir el peso del blendshape anterior
        int previousBlendShape = (currentBlendShape - 1 + blendShapeCount) % blendShapeCount;
        float previousWeight = skinnedMeshRenderer.GetBlendShapeWeight(previousBlendShape);
        skinnedMeshRenderer.SetBlendShapeWeight(previousBlendShape, Mathf.Max(0, previousWeight - speed * Time.deltaTime));

        // Aumentar el peso del blendshape actual
        float currentWeight = skinnedMeshRenderer.GetBlendShapeWeight(currentBlendShape);
        skinnedMeshRenderer.SetBlendShapeWeight(currentBlendShape, Mathf.Min(100, currentWeight + speed * Time.deltaTime));

        // Cuando el blendshape actual alcance 100, cambiar al siguiente
        if (currentWeight >= 100)
        {
            currentBlendShape = (currentBlendShape + 1) % blendShapeCount;
        }
    }
}
