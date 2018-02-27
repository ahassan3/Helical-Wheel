package md591c58c7e09522df81e404c3b0a013247;


public class SKGLTextureView_InternalRenderer
	extends md591c58c7e09522df81e404c3b0a013247.SKGLTextureViewRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("SkiaSharp.Views.Android.SKGLTextureView+InternalRenderer, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", SKGLTextureView_InternalRenderer.class, __md_methods);
	}


	public SKGLTextureView_InternalRenderer () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SKGLTextureView_InternalRenderer.class)
			mono.android.TypeManager.Activate ("SkiaSharp.Views.Android.SKGLTextureView+InternalRenderer, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
