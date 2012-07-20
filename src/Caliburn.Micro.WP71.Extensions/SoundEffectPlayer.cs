namespace Caliburn.Micro {
    using System;
    using System.Windows;
    using System.Windows.Threading;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;

    /// <summary>
    ///   Service allowing to play a .wav sound effect
    /// </summary>
    public interface ISoundEffectPlayer {
        /// <summary>
        ///   Plays a sound effect
        /// </summary>
        /// <param name="wavResource"> The uri of the resource containing the .wav file </param>
        void Play(Uri wavResource);
    }

    /// <summary>
    ///   Default <see cref="ISoundEffectPlayer" /> implementation, using Xna Framework. The sound effect is played without interrupting the music playback on the phone (which is required for the app certification in the WP7 Marketplace. Also note that using the Xna Framework in a WP7 Silverlight app is explicitly allowed for this very purpose.
    /// </summary>
    public class XnaSoundEffectPlayer : ISoundEffectPlayer {
        static XNAFrameworkDispatcherUpdater updater = new XNAFrameworkDispatcherUpdater();

        /// <summary>
        ///   Plays a sound effect
        /// </summary>
        /// <param name="wavResource"> The uri of the resource containing the .wav file </param>
        public void Play(Uri wavResource) {
            var res = Application.GetResourceStream(wavResource);
            using(var stream = res.Stream) {
                var effect = SoundEffect.FromStream(stream);
                effect.Play();
            }
        }

        class XNAFrameworkDispatcherUpdater {
            readonly DispatcherTimer timer;

            public XNAFrameworkDispatcherUpdater() {
                timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
                timer.Tick += OnTick;
                FrameworkDispatcher.Update();
            }

            void OnTick(object sender, EventArgs e) {
                FrameworkDispatcher.Update();
            }
        }
    }
}