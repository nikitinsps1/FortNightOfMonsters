using System.Collections;
using UnityEngine;

namespace Assets
{
    public class EnableGPUInstancing : MonoBehaviour
    {
        private void Awake()
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            SkinnedMeshRenderer meshRenderer = GetComponent<SkinnedMeshRenderer>();
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}