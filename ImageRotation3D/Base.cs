using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ImageRotation3D
{
    public partial class Base : Form
    {
        private static object overlayImageLock = new object();

        public Base()
        {
            InitializeComponent();
        }

        //Setup
        private void Base_Load(object sender, EventArgs e)
        {
            Graphics.Picture.SetupPicture(new Size(pictureBox1.Height, pictureBox1.Width));
            Graphics.Updater.SetupUpdater(pictureBox1);

            UserInput.EventHandlers.SetupEventHandlers(pictureBox1);
        }

        //New Image Buttons
        private void NewBackImage_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();

            string backImagePath = openFileDialog.FileName;

            Graphics.Picture.BackImage = Image.FromFile(backImagePath);
            Graphics.Picture.BackImageSize = Graphics.Picture.BackImage.Size;
            Graphics.Picture.SetupPicture(new Size(Graphics.Picture.BackImage.Width, Graphics.Picture.BackImage.Height));

            Graphics.Picture.OverlayImage = null;
            Graphics.Picture.OverlayImageSize = new Size(0, 0);

            UserInput.EventHandlers.UpdateImagesProperties();
        }
        private void NewOverlayImage_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();

            string overlayImagePath = openFileDialog.FileName;

            Image overlayImage = Image.FromFile(overlayImagePath);
            lock (Graphics.Picture.overlayImageLock) {
                overlayImage = Graphics.Picture.resizeImage(
                                            overlayImage,
                                            (int)((double)Graphics.Picture.BackImage.Width / 3.0),
                                            (int)(((double)overlayImage.Height / (double)overlayImage.Width) * ((double)Graphics.Picture.BackImage.Width / 3.0))
                );

                Point location = new Point(0, 0);
                if (Graphics.Picture.OverlayImage != null){
                    location = Graphics.Picture.OverlayImage.Location;
                }

                Graphics.Picture.OverlayImage = new Models.OverlayImage(overlayImage, new Size(overlayImage.Width, overlayImage.Height), location);
                Graphics.Picture.OverlayImageSize = Graphics.Picture.OverlayImage.Image.Size;
            }

            UserInput.EventHandlers.UpdateImagesProperties();
        }
    }
}
