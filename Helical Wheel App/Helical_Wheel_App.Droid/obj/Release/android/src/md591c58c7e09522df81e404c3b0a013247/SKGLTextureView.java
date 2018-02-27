package md591c58c7e09522df81e404c3b0a013247;


public class SKGLTextureView
	extends md591c58c7e09522df81e404c3b0a013247.GLTextureView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("SkiaSharp.Views.Android.SKGLTextureView, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", SKGLTextureView.class, __md_methods);
	}


	public SKGLTextureView (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == SKGLTextureView.class)
			mono.android.TypeManager.Activate ("SkiaSharp.Views.Android.SKGLTextureView, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public SKGLTextureView (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == SKGLTextureView.class)
			mono.android.TypeManager.Activate ("SkiaSharp.Views.Android.SKGLTextureView, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public SKGLTextureView (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == SKGLTextureView.class)
			mono.android.TypeManager.Activate ("SkiaSharp.Views.Android.SKGLTextureView, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
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
