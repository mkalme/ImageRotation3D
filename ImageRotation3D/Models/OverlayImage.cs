using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageRotation3D.Models
{
    class OverlayImage
    {
        public Image Image { get; set; }
        public Size Size { get; set; }
        public Point Location { get; set; }

        public OverlayImage(Image image, Size size, Point location) {
            this.Image = image;
            this.Size = size;
            this.Location = location;
        }
    }
}
