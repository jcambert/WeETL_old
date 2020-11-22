using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WeETL.Core
{
    public interface IETLCoreComponent 
    {
        public string Name { get; }
    }
    public abstract class ETLCoreComponent :ETLWatchable<ETLCoreComponent>, IETLCoreComponent
    {
        private bool _enabled = true;
        private bool _passthrue = false;


        private readonly ISubject<string> _onPropertyChanged = new Subject<string>();
        

        public IObservable<string> OnPropertyChanged => _onPropertyChanged.AsObservable();
        public ISubject<string> PropertyChangedHandler => _onPropertyChanged;

        
        /// <summary>
        /// Enable or not a component
        /// if FALSE it break the pipe, otherwise it continues
        /// </summary>
        public bool Enabled { get => _enabled; set { this.SetPropertyValueAndNotify(d => d.Enabled, ref _enabled, value, PropertyChangedHandler); } }

        /// <summary>
        /// Patssthrue or not a component 
        /// if TRUE, the component pass consecutively from input, to the mapper, and finally send output,
        /// it does not process before/after transformation, where are generally used to interacting
        /// </summary>
        public bool Passthrue { get=>_passthrue; set { this.SetPropertyValueAndNotify(d => d.Passthrue, ref _passthrue, value, PropertyChangedHandler); } }

        public string Name { get; internal set; }
    }
}
