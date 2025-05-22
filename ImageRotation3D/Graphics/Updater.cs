using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace ImageRotation3D.Graphics
{
    class Updater
    {
        private static PictureBox pictureBox;
        private static readonly object pictureBoxLock = new object();

        private static BackgroundWorker backgroundWorker;

        public static void SetupUpdater(PictureBox pictureBoxToUpdate) {
            pictureBox = pictureBoxToUpdate;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_UpdateGraphics);
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.RunWorkerAsync();
        }

        static void backgroundWorker_UpdateGraphics(object sender, ProgressChangedEventArgs e)
        {
            lock (pictureBoxLock) {
                pictureBox.Image = Picture.Image;
            }
        }
        static void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int c = 0;
            while (true)
            {
                Thread.Sleep(50);

                Picture.UpdatePicture();
                backgroundWorker.ReportProgress(c++);
            }
        }
    }
}
