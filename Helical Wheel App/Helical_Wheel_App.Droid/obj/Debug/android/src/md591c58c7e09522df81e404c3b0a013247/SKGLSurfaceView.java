package md591c58c7e09522df81e404c3b0a013247;


public class SKGLSurfaceView
	extends android.opengl.GLSurfaceView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("SkiaSharp.Views.Android.SKGLSurfaceView, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", SKGLSurfaceView.class, __md_methods);
	}


	public SKGLSurfaceView (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == SKGLSurfaceView.class)
			mono.android.TypeManager.Activate ("SkiaSharp.Views.Android.SKGLSurfaceView, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public SKGLSurfaceView (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == SKGLSurfaceView.class)
			mono.android.TypeManager.Activate ("SkiaSharp.Views.Android.SKGLSurfaceView, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
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
