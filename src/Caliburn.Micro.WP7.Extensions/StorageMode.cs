namespace Caliburn.Micro {
    using System;

    [Flags]
    public enum StorageMode {
        Automatic = 0,
        Temporary = 2,
        Permanent = 4,
        Any = Temporary | Permanent
    }
}