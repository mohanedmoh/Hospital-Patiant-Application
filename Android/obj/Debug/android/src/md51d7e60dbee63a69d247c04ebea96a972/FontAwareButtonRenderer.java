package md51d7e60dbee63a69d247c04ebea96a972;


public class FontAwareButtonRenderer
	extends md51558244f76c53b6aeda52c8a337f2c37.ButtonRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("NControl.Controls.Droid.FontAwareButtonRenderer, NControl.Controls.Droid", FontAwareButtonRenderer.class, __md_methods);
	}


	public FontAwareButtonRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == FontAwareButtonRenderer.class)
			mono.android.TypeManager.Activate ("NControl.Controls.Droid.FontAwareButtonRenderer, NControl.Controls.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public FontAwareButtonRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == FontAwareButtonRenderer.class)
			mono.android.TypeManager.Activate ("NControl.Controls.Droid.FontAwareButtonRenderer, NControl.Controls.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public FontAwareButtonRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == FontAwareButtonRenderer.class)
			mono.android.TypeManager.Activate ("NControl.Controls.Droid.FontAwareButtonRenderer, NControl.Controls.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
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
