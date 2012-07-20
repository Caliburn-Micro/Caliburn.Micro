namespace Caliburn.Micro {
	using System;

	/// <summary>
	/// WindowManager extensions
	/// </summary>
	public static class WindowManagerExtensions {
		/// <summary>
		///   Shows a modal dialog for the specified model, using vibrate and audio feedback
		/// </summary>
		/// <param name = "windowManager">The WindowManager instance.</param>
		/// <param name = "rootModel">The root model.</param>
		/// <param name = "context">The context.</param>
		/// <param name="wavOpeningSound">If not null, use the specified .wav as opening sound</param>
		/// <param name="vibrate">If true, use a vibration feedback on dialog opening</param>
		public static void ShowDialogWithFeedback(this IWindowManager windowManager, object rootModel, object context = null, Uri wavOpeningSound= null, bool vibrate = true) {
			if (wavOpeningSound != null) {
				IoC.Get<ISoundEffectPlayer>().Play(wavOpeningSound);
			}

			if (vibrate) {
				IoC.Get<IVibrateController>().Start(TimeSpan.FromMilliseconds(200));
			}

			windowManager.ShowDialog(rootModel, context);
		}
	}
}