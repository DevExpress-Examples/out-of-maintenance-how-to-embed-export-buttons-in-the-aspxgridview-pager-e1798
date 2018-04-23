using System;
using System.Collections.Generic;
using System.Text;

namespace Grid
{
    [Flags]
    public enum ExportType
    {
        none = 0,
        XLS = 1,
        PDF = 2,
        CSV = 4,
        RTF = 8
    }
}
