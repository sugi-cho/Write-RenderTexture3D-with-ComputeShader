using UnityEngine;
using System.Collections;
using sugi.cc;

public class RenderTexture3D : MonoBehaviour
{
	public int size = 256;
	public RenderTextureFormat format = RenderTextureFormat.ARGBFloat;
	public ComputeShader compute;
	public TextureEvent onCreateTexture;
	public int repeat = 4;
	public float deltaPos = 0.01f;
	public string kernelName = "CSMain";

	[SerializeField]
	RenderTexture rt3d;

	// Use this for initialization
	void Start ()
	{
		rt3d = new RenderTexture (size, size, 0, format);
		rt3d.isVolume = true;
		rt3d.volumeDepth = size;
		rt3d.enableRandomWrite = true;
		rt3d.wrapMode = TextureWrapMode.Repeat;
		rt3d.Create ();

		onCreateTexture.Invoke (rt3d);
	}

	void OnDestroy ()
	{
		if (rt3d == null)
			return;
		rt3d.Release ();
		rt3d = null;
	}

	// Update is called once per frame
	void Update ()
	{
		var kernel = compute.FindKernel (kernelName);
		compute.SetVector ("_TexelSize", new Vector2 (1f / size, size));
		compute.SetInt ("_Repeat", repeat);
		compute.SetFloat ("_DPos", deltaPos);
		compute.SetTexture (kernel, "_Rt3d", rt3d);
		compute.Dispatch (kernel, size / 8, size / 8, size / 8);
	}

}
