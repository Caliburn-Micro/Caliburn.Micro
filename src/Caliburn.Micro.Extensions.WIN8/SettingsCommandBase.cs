using System;

namespace Caliburn.Micro
{
    public abstract class SettingsCommandBase
    {
        private readonly string label;

        protected SettingsCommandBase(string label)
        {
            this.label = label;
        }

        public string Label
        {
            get
            {
                return label;
            }
        }

        public abstract void OnSelected();
    }
}
