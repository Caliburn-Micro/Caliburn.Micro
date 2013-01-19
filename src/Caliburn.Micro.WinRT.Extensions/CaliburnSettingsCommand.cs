using System;
using System.Collections.Generic;

namespace Caliburn.Micro
{
    internal class CaliburnSettingsCommand
    {
        private readonly object _id;
        private readonly string _label;
        private readonly Type _viewModelType;
        private readonly IDictionary<string, object> _viewSettings;

        public CaliburnSettingsCommand(object id, string label, Type viewModelType, IDictionary<string, object> viewSettings)
        {
            _id = id;
            _label = label;
            _viewModelType = viewModelType;
            _viewSettings = viewSettings;
        }

        public Type ViewModelType
        {
            get { return _viewModelType; }
        }

        public object Id
        {
            get { return _id; }
        }

        public string Label
        {
            get { return _label; }
        }

        public IDictionary<string, object> ViewSettings
        {
            get { return _viewSettings; }
        }
    }
}
