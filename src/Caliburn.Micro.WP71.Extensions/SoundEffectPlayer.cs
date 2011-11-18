namespace Caliburn.Micro
{
	using System;
	using System.Windows;
	using Microsoft.Xna.Framework.Audio;
	using System.Windows.Threading;
	using Microsoft.Xna.Framework;
	
	/// <summary>
	/// Service allowing to play a .wav sound effect
	/// </summary>
	public interface ISoundEffectPlayer
	{
		/// <summary>
		/// Plays a sound effect
		/// </summary>
		/// <param name="wavResource">The uri of the resource containing the .wav file</param>
		void Play(Uri wavResource);
	}

	/// <summary>
	/// Default <see cref="ISoundEffectPlayer"/> implementation, using Xna Framework.
	/// The sound effect is played without interrupting the music playback on the phone (which is required for the app certification
	/// in the WP7 Marketplace.
	/// Also note that using the Xna Framework in a WP7 Silverlight app is explicitly allowed for this very purpose.
	/// </summary>
	public class XnaSoundEffectPlayer: ISoundEffectPlayer {

		static XNAFrameworkDispatcherUpdater updater = new XNAFrameworkDispatcherUpdater();

		/// <summary>
		/// Plays a sound effect
		/// </summary>
		/// <param name="wavResource">The uri of the resource containing the .wav file</param>
		public void Play(Uri wavResource)
		{
			var res = Application.GetResourceStream(wavResource);
			using (var stream = res.Stream)
			{
				var effect = SoundEffect.FromStream(stream);
				effect.Play();
			}
		}


		class XNAFrameworkDispatcherUpdater
		{
			private DispatcherTimer timer;
			public XNAFrameworkDispatcherUpdater()
			{
				this.timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100)};
				this.timer.Tick += OnTick;
				FrameworkDispatcher.Update();
			}

			void OnTick(object sender, EventArgs e)
			{
				FrameworkDispatcher.Update();
			}

		}
	}


	
}
