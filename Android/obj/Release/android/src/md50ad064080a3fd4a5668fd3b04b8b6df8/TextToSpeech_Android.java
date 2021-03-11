package md50ad064080a3fd4a5668fd3b04b8b6df8;


public class TextToSpeech_Android
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.speech.tts.TextToSpeech.OnInitListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInit:(I)V:GetOnInit_IHandler:Android.Speech.Tts.TextToSpeech/IOnInitListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("TodoLocalized.TextToSpeech_Android, TodoLocalized", TextToSpeech_Android.class, __md_methods);
	}


	public TextToSpeech_Android ()
	{
		super ();
		if (getClass () == TextToSpeech_Android.class)
			mono.android.TypeManager.Activate ("TodoLocalized.TextToSpeech_Android, TodoLocalized", "", this, new java.lang.Object[] {  });
	}


	public void onInit (int p0)
	{
		n_onInit (p0);
	}

	private native void n_onInit (int p0);

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
