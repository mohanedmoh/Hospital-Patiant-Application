using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model
{
    public interface imagePicker
    {
        void BringUpCameraAsync();
        void BringUpGallery();
    }
}
