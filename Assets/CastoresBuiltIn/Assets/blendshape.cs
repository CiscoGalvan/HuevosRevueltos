using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blendshape : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh mesh;
    int blendahape;
    int playindex = 0;
    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();  
        mesh = skinnedMeshRenderer.sharedMesh;
        blendahape = mesh.blendShapeCount;

    }

    // Update is called once per frame
    void Update()
    {
        if (playindex > 0) skinnedMeshRenderer.SetBlendShapeWeight(playindex - 1, 0f);
        else if(playindex==0) skinnedMeshRenderer.SetBlendShapeWeight(blendahape-1, 0f);
        skinnedMeshRenderer.SetBlendShapeWeight(playindex, 100f);
        playindex++;
        if (playindex > blendahape - 1) playindex = 0;
    }
}
