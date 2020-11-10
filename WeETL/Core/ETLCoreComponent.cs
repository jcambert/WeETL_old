using System.ComponentModel;

namespace WeETL.Core
{
    public class ETLCoreComponent : INotifyPropertyChanged
    {
        private bool _enabled = true;
        private bool _passthrue = false;
        public event PropertyChangedEventHandler PropertyChanged;

        
        /// <summary>
        /// Enable or not a component
        /// if FALSE it break the pipe, otherwise it continues
        /// </summary>
        public bool Enabled { get => _enabled; set { this.SetPropertyValueAndNotify(d => d.Enabled, ref _enabled, value, PropertyChanged); } }

        /// <summary>
        /// Patssthrue or not a component 
        /// if TRUE, the component pass consecutively from input, to the mapper, and finally send output,
        /// it does not process before/after transformation, where are generally used to interacting
        /// </summary>
        public bool Passthrue { get=>_passthrue; set { this.SetPropertyValueAndNotify(d => d.Passthrue, ref _passthrue, value, PropertyChanged); } }
    }
}
