namespace Caliburn.Micro.HelloScreens.Framework.Controls {
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// This class was borrowed from the SL4 JetPack Theme.
    /// </summary>
    public class TiledBackground : UserControl {
        public static readonly DependencyProperty SourceUriProperty = DependencyProperty.Register(
            "SourceUri",
            typeof(Uri),
            typeof(TiledBackground),
            new PropertyMetadata(null, OnSourceUriChanged)
            );

        BitmapImage bitmap;
        int lastHeight;
        int lastWidth;
        WriteableBitmap sourceBitmap;
        readonly Image tiledImage = new Image();

        public TiledBackground() {
            tiledImage.Stretch = Stretch.None;
            Content = tiledImage;
            SizeChanged += TiledBackgroundSizeChanged;
        }

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Uri SourceUri {
            get { return (Uri)GetValue(SourceUriProperty); }
            set { SetValue(SourceUriProperty, value); }
        }

        void TiledBackgroundSizeChanged(object sender, SizeChangedEventArgs e) {
            UpdateTiledImage();
        }

        void UpdateTiledImage() {
            if(sourceBitmap == null)
                return;

            var width = (int)Math.Ceiling(ActualWidth);
            var height = (int)Math.Ceiling(ActualHeight);

            // only regenerate the image if the width/height has grown
            if(width < lastWidth && height < lastHeight)
                return;
            lastWidth = width;
            lastHeight = height;

            var final = new WriteableBitmap(width, height);

            for(var x = 0; x < final.PixelWidth; x++) {
                for(var y = 0; y < final.PixelHeight; y++) {
                    var tiledX = (x % sourceBitmap.PixelWidth);
                    var tiledY = (y % sourceBitmap.PixelHeight);
                    final.Pixels[y * final.PixelWidth + x] = sourceBitmap.Pixels[tiledY * sourceBitmap.PixelWidth + tiledX];
                }
            }

            tiledImage.Source = final;
        }

        static void OnSourceUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((TiledBackground)d).OnSourceUriChanged(e);
        }

        protected virtual void OnSourceUriChanged(DependencyPropertyChangedEventArgs e) {
            bitmap = new BitmapImage(e.NewValue as Uri) { CreateOptions = BitmapCreateOptions.None };
            bitmap.ImageOpened += BitmapImageOpened;
        }

        void BitmapImageOpened(object sender, RoutedEventArgs e) {
            sourceBitmap = new WriteableBitmap(bitmap);
            UpdateTiledImage();
        }
    }
}