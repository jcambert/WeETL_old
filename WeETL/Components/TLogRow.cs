using System;
using System.Collections.Generic;
using System.Text;

namespace WeETL
{
    public class TLogRow<TSchema>:ETLComponent<TSchema> ,IDisposable
        where TSchema : class, new()
    {
        private bool _showHeader;
        private bool _headerShown;
        private IDisposable _disposable;
        private bool disposedValue;
        private ETLComponent<TSchema> _input;

        public TLogRow()
        {

        }
        public TLogRow<TSchema> ShowHeader(bool show)
        {
            this._showHeader = show;
            return this;
        }
        /* public override void SetInput(ETLComponent<TSchema> component)
         {
             if (_disposable != null || !HasInput) return;
             _input = component;
            _disposable = component.OnOuput.Subscribe(
                row => {
                    ShowHeader();
                    Console.WriteLine($"{row}"); 

                }, 
                ex => Console.Error.WriteLine(ex.Message), 
                () => Console.WriteLine("Completed"));
         }*/
        protected override void InternalOnRow(int index,TSchema row)
        {
            base.InternalOnRow(index,row);
            ShowHeader();
            Console.WriteLine($"{index.ToString().PadRight(3)} | {ElapsedTime.TotalSeconds} |{row}");
        }
        protected override void InternalOnException(Exception e)
        {
            base.InternalOnException(e);
            Console.Error.WriteLine(e.Message);
        }
        protected override void InternalOnCompleted()
        {
            base.InternalOnCompleted();
            
        }
        public void RemoveInput()
        {
            _disposable?.Dispose();
            _disposable = null;
            _input = null;
        }

        protected virtual void ShowHeader()
        {
            if (!_showHeader || _headerShown) return;
            _headerShown = true;
            Console.WriteLine("HEADER");
        }
        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                    _disposable?.Dispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~TLogRow()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
