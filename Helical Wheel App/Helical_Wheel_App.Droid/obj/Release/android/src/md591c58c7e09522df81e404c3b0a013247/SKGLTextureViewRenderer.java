package md591c58c7e09522df81e404c3b0a013247;


public abstract class SKGLTextureViewRenderer
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("SkiaSharp.Views.Android.SKGLTextureViewRenderer, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", SKGLTextureViewRenderer.class, __md_methods);
	}


	public SKGLTextureViewRenderer () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SKGLTextureViewRenderer.class)
			mono.android.TypeManager.Activate ("SkiaSharp.Views.Android.SKGLTextureViewRenderer, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", "", this, new java.lang.Object[] {  });
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
