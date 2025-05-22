using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace ImageRotation3D.UserInput
{
    class Events
    {
        //Overlay Cursor
        public static void MouseMoveOverlay(MouseEventArgs e) {
            Cursor.Current = Cursors.SizeAll;
        }
        public static void MouseEnterOverlay(MouseEventArgs e) {
            Cursor.Current = Cursors.SizeAll;
        }
        public static void MouseExitOverlay(MouseEventArgs e) {
            Cursor.Current = Cursors.Arrow;
        }

        //Overlay Drag
        public static void MouseDragOverlay(MouseEventArgs e) {
            Point newLocation = new Point(0, 0);

            newLocation.X = (e.X - EventHandlers.pointOnClick.X);
            newLocation.Y = (e.Y - EventHandlers.pointOnClick.Y);

            //Update
            EventHandlers.OverlayImageLocation = new Point(
                EventHandlers.overlayLocationOnClick.X + newLocation.X,
                EventHandlers.overlayLocationOnClick.Y + newLocation.Y
            );

            EventHandlers.ExtensionPoints = EventHandlers.GetExtensionPoints();

            //Graphics
            Graphics.Picture.OverlayImage.Location = new Point(
                (int)((double)EventHandlers.OverlayImageLocation.X * (1.0 / EventHandlers.Ratio)),
                (int)((double)EventHandlers.OverlayImageLocation.Y * (1.0 / EventHandlers.Ratio))
            );
        }

        //Extension
        public static void MouseMoveExtension(int index) {
            Cursor.Current = Cursors.Hand;
        }
        public static void MouseEnterExtension(int index) {
            Graphics.Picture.ExtensionHover[index] = true;

            Cursor.Current = Cursors.Hand;
        }
        public static void MouseExitExtension(int index){
            Graphics.Picture.ExtensionHover[index] = false;

            Cursor.Current = Cursors.Arrow;
        }

        //Extension Drag
        public static void MouseDragExtension(int index, MouseEventArgs e) {
            Graphics.Picture.ExtensionHold[index] = true;

            Point newLocation = new Point(0, 0);

            newLocation.X = (e.X - EventHandlers.pointOnClick.X);
            newLocation.Y = (e.Y - EventHandlers.pointOnClick.Y);

            Thread.Sleep(20);

            if (index == 2) {
                lock (Graphics.Picture.overlayImageLock) {
                    int newWidth = EventHandlers.overlaySizeOnClick.Width;
                    int newHeight = EventHandlers.overlaySizeOnClick.Height + newLocation.Y;

                    Image image = Graphics.Picture.resizeImage(Graphics.Picture.OverlayImage.Image, newWidth, newHeight);

                    Graphics.Picture.OverlayImage.Image = new Bitmap(image);
                    Graphics.Picture.OverlayImage.Size = new Size(newWidth, newHeight);
                    Graphics.Picture.OverlayImageSize = new Size(newWidth, newHeight);

                    EventHandlers.OverlayImageSize = EventHandlers.GetOverlaySizeOnImage();
                    EventHandlers.ExtensionPoints = EventHandlers.GetExtensionPoints();

                    image.Dispose();
                }
            }

            //if (index == 0 || index == 2) {
            //    Debug.WriteLine(newLocation.Y);
            //} else if (index == 1 || index == 3) {
            //    Debug.WriteLine(newLocation.X);
            //}
        }
        public static void MouseEnterDragExtension(int index, MouseEventArgs e) {
            Graphics.Picture.ExtensionHold[index] = true;
        }
        public static void MouseExitDragExtension(int index, MouseEventArgs e) {
            Graphics.Picture.ExtensionHold[index] = false;
        }
    }
}
