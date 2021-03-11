using System;
using System.Collections.Generic;
using System.Text;

namespace TodoLocalized.Database
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
