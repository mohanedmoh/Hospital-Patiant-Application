using System.Collections.Generic;
using Android.Speech.Tts;
using Java.Lang;
using Xamarin.Forms;
using TodoLocalized;

[assembly: Dependency (typeof (TextToSpeech_Android))]

namespace TodoLocalized
{
	public class TextToSpeech_Android : Object, ITextToSpeech, TextToSpeech.IOnInitListener
	{
		TextToSpeech speaker;
		string toSpeak;

		public TextToSpeech_Android ()
		{
		}

		public void Speak (string text)
		{
#pragma warning disable CS0618 // Type or member is obsolete
            var c = Forms.Context;
#pragma warning restore CS0618 // Type or member is obsolete
            toSpeak = text;

			if (speaker == null) {
				speaker = new TextToSpeech (c, this);
			} else {
				var p = new Dictionary<string,string> ();
			//	speaker.Speak (toSpeak, QueueMode.Flush, p);
			}
        }

		#region IOnInitListener implementation
		public void OnInit (OperationResult status)
		{
			if (status.Equals (OperationResult.Success)) {
				System.Diagnostics.Debug.WriteLine ("spoke");
				var p = new Dictionary<string,string> ();
			//	speaker.Speak (toSpeak, QueueMode.Flush, p);
			} else {
				System.Diagnostics.Debug.WriteLine ("was quiet");
			}
		}
		#endregion
	}
}

