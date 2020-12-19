using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observables.Dxf.Tables;

namespace WeETL.Observables.Dxf.Collections
{
    /// <summary>
    /// Represents a collection of application registries.
    /// </summary>
    public sealed class ApplicationRegistries :
        TableObjects<ApplicationRegistry>
    {
        #region private variables
        IDisposable _onNameChangedDisposable;

        public override string CodeName => DxfTableCode.ApplicationId;
        #endregion
        #region constructor

        internal ApplicationRegistries(DxfDocument document)
            : this(document, null)
        {
        }

        internal ApplicationRegistries(DxfDocument document, string handle)
            : base(document, handle)
        {
        }

        #endregion

        #region override methods

        /// <summary>
        /// Adds an application registry to the list.
        /// </summary>
        /// <param name="appReg"><see cref="ApplicationRegistry">ApplicationRegistry</see> to add to the list.</param>
        /// <param name="assignHandle">Checks if the appReg parameter requires a handle.</param>
        /// <returns>
        /// If a an application registry already exists with the same name as the instance that is being added the method returns the existing application registry,
        /// if not it will return the new application registry.
        /// </returns>
        internal override ApplicationRegistry Add(ApplicationRegistry appReg, bool assignHandle)
        {
            if (appReg == null)
                throw new ArgumentNullException(nameof(appReg));

            ApplicationRegistry add;
            if (this.list.TryGetValue(appReg.Name, out add))
                return add;

            if (assignHandle || string.IsNullOrEmpty(appReg.Handle))
                this.GetOwner<DxfDocument>().NumHandles = appReg.AssignHandle(this.GetOwner<DxfDocument>().NumHandles);

            this.list.Add(appReg.Name, appReg);
            this.references.Add(appReg.Name, new List<DxfObject>());

            appReg.Owner = this.Handle;
            _onNameChangedDisposable= appReg.OnNamedChanged.Subscribe(e => {
                if (this.Contains(e.NewValue))
                    throw new ArgumentException("There is already another application registry with the same name.");
                
                this.list.Remove(e.Sender.Name);
                this.list.Add(e.NewValue, (ApplicationRegistry)e.Sender);

                List<DxfObject> refs = this.references[e.Sender.Name];
                this.references.Remove(e.Sender.Name);
                this.references.Add(e.NewValue, refs);
            });
            //appReg.NameChanged += this.Item_NameChanged;

            this.GetOwner<DxfDocument>().AddedObjects.Add(appReg.Handle, appReg);

            return appReg;
        }

        /// <summary>
        /// Removes an application registry.
        /// </summary>
        /// <param name="name"><see cref="ApplicationRegistry">ApplicationRegistry</see> name to remove from the document.</param>
        /// <returns>True if the application registry has been successfully removed, or false otherwise.</returns>
        /// <remarks>Reserved application registries or any other referenced by objects cannot be removed.</remarks>
        public override bool Remove(string name)=> this.Remove(this[name]);
        

        /// <summary>
        /// Removes an application registry.
        /// </summary>
        /// <param name="item"><see cref="ApplicationRegistry">ApplicationRegistry</see> to remove from the document.</param>
        /// <returns>True if the application registry has been successfully removed, or false otherwise.</returns>
        /// <remarks>Reserved application registries or any other referenced by objects cannot be removed.</remarks>
        public override bool Remove(ApplicationRegistry item)
        {
            if (item == null || !this.Contains(item) || item.IsReserved || this.references[item.Name].Count != 0)
                return false;


            this.GetOwner<DxfDocument>().AddedObjects.Remove(item.Handle);
            this.references.Remove(item.Name);
            this.list.Remove(item.Name);

            item.Handle = null;
            item.Owner = null;
            _onNameChangedDisposable?.Dispose();
            _onNameChangedDisposable = null;
           // item.NameChanged -= this.Item_NameChanged;

            return true;
        }

        #endregion

       
    }
}
