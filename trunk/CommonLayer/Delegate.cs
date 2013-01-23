using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLayer
{
    /// <summary>
    /// An event for updating progress
    /// </summary>
    /// <param name="info">The info message.</param>
    /// <param name="percent">The new value</param>
    public delegate void UpdateProgressEvent(string info, int percent);
}
