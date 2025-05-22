using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ImageRotation3D.Graphics
{
    class Picture
    {
        public static Image Image;
        private static System.Drawing.Graphics graphics;

        public static Image BackImage;
        public static Models.OverlayImage OverlayImage;

        public static Size BackImageSize = new Size(0, 0);
        public static Size OverlayImageSize = new Size(0, 0);

        private static object backImageLock = new object();
        public static object overlayImageLock = new object();

        public static bool[] ExtensionHover = new bool[4]; //north, east, south, west
        public static bool[] ExtensionHold = new bool[4]; //north, east, south, west

        public static void SetupPicture(Size size) {
            Image = new Bitmap(size.Width, size.Height);
            graphics = System.Drawing.Graphics.FromImage(Image);
        }

        public static void UpdatePicture() {
            //Back image
            lock (backImageLock) {
                if (BackImage != null)
                {
                    graphics.DrawImage(BackImage, new PointF(0, 0));
                }
            }

            //Overlay image
            lock (overlayImageLock) {
                if (OverlayImage != null)
                {
                    Image image = resizeImage(OverlayImage.Image, OverlayImage.Size.Width, OverlayImage.Size.Height);

                    graphics.DrawImage(
                            image,
                            OverlayImage.Location
                    );

                    image.Dispose();
                }
            }

            if (OverlayImage != null) {
                Point[,] extensionPoints = {
                    {OverlayImage.Location, new Point(OverlayImage.Location.X + OverlayImageSize.Width, OverlayImage.Location.Y)},
                    {new Point(OverlayImage.Location.X + OverlayImageSize.Width, OverlayImage.Location.Y), new Point(OverlayImage.Location.X + OverlayImageSize.Width, OverlayImage.Location.Y + OverlayImageSize.Height)},
                    {new Point(OverlayImage.Location.X, OverlayImage.Location.Y + OverlayImageSize.Height), new Point(OverlayImage.Location.X + OverlayImageSize.Width, OverlayImage.Location.Y + OverlayImageSize.Height)},
                    {OverlayImage.Location, new Point(OverlayImage.Location.X, OverlayImage.Location.Y + OverlayImageSize.Height)}
                };

                //Extension hover
                for (int extension = 0; extension < ExtensionHover.Length; extension++) {
                    if (ExtensionHover[extension]) {
                        graphics.DrawLine(new Pen(Color.Yellow, 3), extensionPoints[extension, 0], extensionPoints[extension, 1]);
                    }
                }

                //Extension hold
                for (int extension = 0; extension < ExtensionHold.Length; extension++){
                    if (ExtensionHold[extension])
                    {
                        graphics.DrawLine(new Pen(Color.Orange, 3), extensionPoints[extension, 0], extensionPoints[extension, 1]);
                    }
                }
            }
        }

        public static Image resizeImage(Image image, int newWidth, int newHeight)
        {
            return new Bitmap(image, new Size(newWidth, newHeight));
        }
    }
}
