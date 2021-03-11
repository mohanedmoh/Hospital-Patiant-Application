package md51d7e60dbee63a69d247c04ebea96a972;


public class RoundCornerViewRenderer
	extends md5fda5119d0c3a6fef277d09a77f95a7ae.NControlViewRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_draw:(Landroid/graphics/Canvas;)V:GetDraw_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("NControl.Controls.Droid.RoundCornerViewRenderer, NControl.Controls.Droid", RoundCornerViewRenderer.class, __md_methods);
	}


	public RoundCornerViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == RoundCornerViewRenderer.class)
			mono.android.TypeManager.Activate ("NControl.Controls.Droid.RoundCornerViewRenderer, NControl.Controls.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public RoundCornerViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == RoundCornerViewRenderer.class)
			mono.android.TypeManager.Activate ("NControl.Controls.Droid.RoundCornerViewRenderer, NControl.Controls.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public RoundCornerViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == RoundCornerViewRenderer.class)
			mono.android.TypeManager.Activate ("NControl.Controls.Droid.RoundCornerViewRenderer, NControl.Controls.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public void draw (android.graphics.Canvas p0)
	{
		n_draw (p0);
	}

	private native void n_draw (android.graphics.Canvas p0);

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
