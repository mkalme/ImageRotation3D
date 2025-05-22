using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace ImageRotation3D.UserInput
{
    class EventHandlers
    {
        public static PictureBox pictureBox;
        public static readonly object pictureBoxLock = new object();

        public static bool mouseHold = false;
        public static bool hoverOnMouseClick = false;
        public static Point pointOnClick = new Point();
        public static Point overlayLocationOnClick = new Point();

        public static int extensionDragOnClick = -1;

        public static Size overlaySizeOnClick = new Size();

        public static Size ImageSize = new Size();
        public static Point ImageLocation = new Point();

        public static double Ratio = 0;

        public static Size OverlayImageSize = new Size();
        public static Point OverlayImageLocation = new Point();

        public static Point[,] ExtensionPoints = new Point[4,2];

        //Setup
        public static void SetupEventHandlers(PictureBox pictureBox1) {
            pictureBox = pictureBox1;

            UpdateImagesProperties();

            pictureBox.MouseMove += MouseMove;
            pictureBox.MouseDown += MouseDown;
            pictureBox.MouseUp += MouseUp;
        }
        public static void UpdateImagesProperties() {
            ImageSize = GetImageSizeOnPictureBox();
            ImageLocation = GetImageLocationOnPictureBox();

            Ratio = GetRatio();

            OverlayImageSize = GetOverlaySizeOnImage();
            OverlayImageLocation = GetOverlayLocationOnImage();

            ExtensionPoints = GetExtensionPoints();
        }

        //Events
        private static int prevStateEnterExit = 0; //0 = exit, 1 = enter
        private static int lastExtensionEnter = -1;
        private static void MouseMove(object sender, MouseEventArgs e) {
            if (Graphics.Picture.OverlayImage != null) {
                //Extension hover enter || exit
                int index = WhichExtensionMouseHovers();
                bool mouseHover = IfMouseHoversOverlay();

                //Mouse move extension
                if (index > -1) {
                    Events.MouseMoveExtension(index);
                }

                if (lastExtensionEnter != index && lastExtensionEnter > -1)
                {//Exit
                    Events.MouseExitExtension(lastExtensionEnter);

                    lastExtensionEnter = -1;
                }
                if (lastExtensionEnter != index && index > -1)
                {//Exter
                    Events.MouseEnterExtension(index);

                    lastExtensionEnter = index;
                }

                //Extension Drag
                if (mouseHold && extensionDragOnClick > -1) {
                    Events.MouseDragExtension(extensionDragOnClick, e);
                }



                //Mouse move overlay
                if (mouseHover && extensionDragOnClick == -1) {
                    Events.MouseMoveOverlay(e);
                }

                //Mouse enter || exit overlay
                if (prevStateEnterExit == 0 && mouseHover && extensionDragOnClick == -1)
                {
                    Events.MouseEnterOverlay(e);

                    prevStateEnterExit = 1;
                }
                else if(prevStateEnterExit == 1 && mouseHover == false && extensionDragOnClick == -1)
                {
                    Events.MouseExitOverlay(e);

                    prevStateEnterExit = 0;
                }

                //Drag
                if (mouseHold && hoverOnMouseClick && extensionDragOnClick == -1)
                {
                    Events.MouseDragOverlay(e);
                }
            }
        }
        private static void MouseDown(object sender, MouseEventArgs e) {
            mouseHold = true;
            hoverOnMouseClick = IfMouseHoversOverlay();
            extensionDragOnClick = WhichExtensionMouseHovers();
            overlaySizeOnClick = Graphics.Picture.OverlayImageSize;

            pointOnClick = e.Location;

            if (IfMouseHoversOverlay()) {
                overlayLocationOnClick = OverlayImageLocation;
            }

            //Extension drag
            if (extensionDragOnClick > -1)
            {//Enter
                Events.MouseEnterDragExtension(extensionDragOnClick, e);
            }
        }
        private static void MouseUp(object sender, MouseEventArgs e)
        {
            mouseHold = false;
            hoverOnMouseClick = false;
            overlaySizeOnClick = new Size();

            if (extensionDragOnClick > -1)
            {//Exit
                Events.MouseExitDragExtension(extensionDragOnClick, e);
                extensionDragOnClick = -1;
            }
        }

        //Mouse
        private static bool IfMouseHoversOverlay() {
            Point point = GetMouseLocationOnOverlay();

            //If mouse hovers overlay
            if ((point.X > -1 && point.X < OverlayImageSize.Width) &&
                (point.Y > -1 && point.Y < OverlayImageSize.Height)) {

                return true;
            }
            else {
                return false;
            }
        }
        private static int WhichExtensionMouseHovers() {
            Point point = GetMouseLocationOnImage();

            int index = -1;

            for (int extension = 0; extension < ExtensionPoints.GetLength(0); extension++) {
                if ((point.X > ExtensionPoints[extension, 0].X - 5 && point.X < ExtensionPoints[extension, 1].X + 5) &&
                    (point.Y > ExtensionPoints[extension, 0].Y - 5 && point.Y < ExtensionPoints[extension, 1].Y + 5)) {

                    index = extension;

                    goto after_loop;
                }
            }
            after_loop:

            return index;
        }

        private static Point GetMouseLocationOnOverlay() {
            Point mousePoint = GetMouseLocationOnImage();

            Point mouseLocation = new Point(-1, -1);

            if (Graphics.Picture.OverlayImage != null) {
                mouseLocation.X = mousePoint.X - OverlayImageLocation.X;
                mouseLocation.Y = mousePoint.Y - OverlayImageLocation.Y;
            }

            return mouseLocation;
        }
        private static Point GetMouseLocationOnImage() {
            Point mousePoint = pictureBox.PointToClient(Cursor.Position);

            Point mouseLocation = new Point(0, 0);
            mouseLocation.X = mousePoint.X - ImageLocation.X;
            mouseLocation.Y = mousePoint.Y - ImageLocation.Y;

            return mouseLocation;
        }

        //Update
        private static Size GetImageSizeOnPictureBox() {
            Size size = new Size(0, 0);

            if (Graphics.Picture.Image != null) {
                if (((double)Graphics.Picture.Image.Height / (double)Graphics.Picture.Image.Width) >=
                     (double)(pictureBox.Height / (double)pictureBox.Width))
                { //If PictureBox height is the same as the image's height
                    int imageWidth = (int)(((double)pictureBox.Height / (double)Graphics.Picture.Image.Height) * (double)Graphics.Picture.Image.Width);

                    size = new Size(imageWidth, pictureBox.Height);
                }
                else
                {//If PictureBox width is the same as the image's width
                    int imageHeight = (int)(((double)pictureBox.Width / (double)Graphics.Picture.Image.Width) * (double)Graphics.Picture.Image.Height);

                    size = new Size(pictureBox.Width, imageHeight);
                }
            }

            return size;
        }
        private static Point GetImageLocationOnPictureBox()
        {
            Point location = new Point(0, 0);

            location.X = (pictureBox.Width - ImageSize.Width) / 2;
            location.Y = (pictureBox.Height - ImageSize.Height) / 2;

            return location;
        }

        private static double GetRatio()
        {
            double ratio = 0;

            if (Graphics.Picture.BackImage != null) {
                ratio = (double)ImageSize.Width / (double)Graphics.Picture.BackImageSize.Width;
            }

            return ratio;
        }

        public static Size GetOverlaySizeOnImage() {
            Size overlayImageSize = new Size();

            if (Graphics.Picture.OverlayImage != null) {

                overlayImageSize.Width = (int)((double)Graphics.Picture.OverlayImageSize.Width * Ratio);
                overlayImageSize.Height = (int)((double)Graphics.Picture.OverlayImageSize.Height * Ratio);
            }

            return overlayImageSize;
        }
        private static Point GetOverlayLocationOnImage() {
            Point overlayLocation = new Point();

            if (Graphics.Picture.OverlayImage != null) {
                overlayLocation.X = (int)((double)Graphics.Picture.OverlayImage.Location.X * Ratio);
                overlayLocation.Y = (int)((double)Graphics.Picture.OverlayImage.Location.Y * Ratio);
            }

            return overlayLocation;
        }

        public static Point[,] GetExtensionPoints() {
            Point[,] points = {
                    {OverlayImageLocation, new Point(OverlayImageLocation.X + OverlayImageSize.Width, OverlayImageLocation.Y)},
                    {new Point(OverlayImageLocation.X + OverlayImageSize.Width, OverlayImageLocation.Y), new Point(OverlayImageLocation.X + OverlayImageSize.Width, OverlayImageLocation.Y + OverlayImageSize.Height)},
                    {new Point(OverlayImageLocation.X, OverlayImageLocation.Y + OverlayImageSize.Height), new Point(OverlayImageLocation.X + OverlayImageSize.Width, OverlayImageLocation.Y + OverlayImageSize.Height)},
                    {OverlayImageLocation, new Point(OverlayImageLocation.X, OverlayImageLocation.Y + OverlayImageSize.Height)}
            };

            return points;
        }
    }
}
