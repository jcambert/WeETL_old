using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observables.Dxf.Tables;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf.Collections
{
    public interface ITextStyles : ITableObject<TextStyle>,IDxfObject
    {
        IObservable<TextStyle> OnAdd { get; }

    }
    /// <summary>
    /// Represents a collection of text styles.
    /// </summary>
    public sealed class TextStyles :
        TableObjects<TextStyle>, ITextStyles
    {
        private ISubject<TextStyle> _onAdd = new Subject<TextStyle>();
        public override string CodeName => DxfTableCode.Style;

        public IObservable<TextStyle> OnAdd => _onAdd.AsObservable();
        #region private variables
        private IDisposable _onNameChanged;

        #endregion
        #region constructor

        public TextStyles() : base() { }



        #endregion

        #region override methods

        /// <summary>
        /// Adds a text style to the list.
        /// </summary>
        /// <param name="style"><see cref="TextStyle">TextStyle</see> to add to the list.</param>
        /// <param name="assignHandle">Specifies if a handle needs to be generated for the text style parameter.</param>
        /// <returns>
        /// If a text style already exists with the same name as the instance that is being added the method returns the existing text style,
        /// if not it will return the new text style.
        /// </returns>
        internal override TextStyle Add(TextStyle style, bool assignHandle)
        {
            Check.NotNull(style, nameof(style));
            TextStyle add;
            if (this.list.TryGetValue(style.Name, out add))
                return add;

           // if (assignHandle || string.IsNullOrEmpty(style.Handle))
           //     this.Document.NumHandles = style.AssignHandle(this.Document.NumHandles);

            this.list.Add(style.Name, style);
            this.references.Add(style.Name, new List<DxfObject>());

            // style.Owner = this.Handle;
            _onAdd.OnNext(style);
            _onNameChanged = style.OnNamedChanged.Subscribe(e =>
            {
                if (this.Contains(e.NewValue))
                    throw new ArgumentException("There is already another text style with the same name.");

                this.list.Remove(e.Sender.Name);
                this.list.Add(e.NewValue, (TextStyle)e.Sender);

                List<DxfObject> refs = this.references[e.Sender.Name];
                this.references.Remove(e.Sender.Name);
                this.references.Add(e.NewValue, refs);
            });


            //this.GetOwner<DxfDocument>().AddedObjects.Add(style.Handle, style);

            return style;
        }

        /// <summary>
        /// Removes a text style.
        /// </summary>
        /// <param name="name"><see cref="TextStyle">TextStyle</see> name to remove from the document.</param>
        /// <returns>True if the text style has been successfully removed, or false otherwise.</returns>
        /// <remarks>Reserved text styles or any other referenced by objects cannot be removed.</remarks>
        public override bool Remove(string name)
        {
            return this.Remove(this[name]);
        }

        /// <summary>
        /// Removes a text style.
        /// </summary>
        /// <param name="item"><see cref="TextStyle">TextStyle</see> to remove from the document.</param>
        /// <returns>True if the text style has been successfully removed, or false otherwise.</returns>
        /// <remarks>Reserved text styles or any other referenced by objects cannot be removed.</remarks>
        public override bool Remove(TextStyle item)
        {
            if (item == null)
                return false;

            if (!this.Contains(item))
                return false;

            if (item.IsReserved)
                return false;

            if (this.references[item.Name].Count != 0)
                return false;

            this.GetOwner<DxfDocument>().AddedObjects.Remove(item.Handle);
            this.references.Remove(item.Name);
            this.list.Remove(item.Name);

            item.Handle = null;
            item.Owner = null;

            _onNameChanged?.Dispose();
            _onNameChanged = null;

            return true;
        }

        #endregion


    }
}
