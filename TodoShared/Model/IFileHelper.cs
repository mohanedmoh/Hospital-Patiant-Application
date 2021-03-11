using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Model
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
