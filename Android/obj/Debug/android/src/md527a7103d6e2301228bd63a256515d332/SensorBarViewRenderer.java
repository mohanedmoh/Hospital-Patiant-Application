package md527a7103d6e2301228bd63a256515d332;


public class SensorBarViewRenderer
	extends md51558244f76c53b6aeda52c8a337f2c37.ViewRenderer_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("XLabs.Forms.Controls.SensorBarViewRenderer, XLabs.Forms.Droid", SensorBarViewRenderer.class, __md_methods);
	}


	public SensorBarViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == SensorBarViewRenderer.class)
			mono.android.TypeManager.Activate ("XLabs.Forms.Controls.SensorBarViewRenderer, XLabs.Forms.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public SensorBarViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == SensorBarViewRenderer.class)
			mono.android.TypeManager.Activate ("XLabs.Forms.Controls.SensorBarViewRenderer, XLabs.Forms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public SensorBarViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == SensorBarViewRenderer.class)
			mono.android.TypeManager.Activate ("XLabs.Forms.Controls.SensorBarViewRenderer, XLabs.Forms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
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
