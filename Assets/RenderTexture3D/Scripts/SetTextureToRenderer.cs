using UnityEngine;
using System.Collections;
using sugi.cc;

public class SetTextureToRenderer : MonoBehaviour
{
    public string propName = "_Tex";
    public void SetTexture(Texture tex)
    {
        var r = GetComponent<Renderer>();
        r.SetTexture(propName, tex);
    }
}
