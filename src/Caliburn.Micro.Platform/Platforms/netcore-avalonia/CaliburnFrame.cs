using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Caliburn.Micro;

public class CaliburnFrame : TransitioningContentControl, IStyleable
{
    private string defaultContent { get; }= "Default Content";

    /// <summary>
    /// Initializes a new instance of the <see cref="CaliburnFrame"/> class.
    /// </summary>
    public CaliburnFrame()
    {
        Content= defaultContent;
    }

    Type IStyleable.StyleKey => typeof(TransitioningContentControl);
}
