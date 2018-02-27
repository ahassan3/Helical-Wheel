package md591c58c7e09522df81e404c3b0a013247;


public class GLTextureView_LogWriter
	extends java.io.Writer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_close:()V:GetCloseHandler\n" +
			"n_flush:()V:GetFlushHandler\n" +
			"n_write:([CII)V:GetWrite_arrayCIIHandler\n" +
			"";
		mono.android.Runtime.register ("SkiaSharp.Views.Android.GLTextureView+LogWriter, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", GLTextureView_LogWriter.class, __md_methods);
	}


	public GLTextureView_LogWriter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GLTextureView_LogWriter.class)
			mono.android.TypeManager.Activate ("SkiaSharp.Views.Android.GLTextureView+LogWriter, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", "", this, new java.lang.Object[] {  });
	}


	public GLTextureView_LogWriter (java.lang.Object p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == GLTextureView_LogWriter.class)
			mono.android.TypeManager.Activate ("SkiaSharp.Views.Android.GLTextureView+LogWriter, SkiaSharp.Views.Android, Version=1.60.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", "Java.Lang.Object, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void close ()
	{
		n_close ();
	}

	private native void n_close ();


	public void flush ()
	{
		n_flush ();
	}

	private native void n_flush ();


	public void write (char[] p0, int p1, int p2)
	{
		n_write (p0, p1, p2);
	}

	private native void n_write (char[] p0, int p1, int p2);

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
