using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observables.Dxf.Entities;

namespace WeETL.Observables.Dxf.IO
{
    [DxfSection(DxfEntityCode.EntitiesSection)]
    public partial class EntitySectionWriter : SectionWriter
    {
        public EntitySectionWriter(IServiceProvider serviceProvider, ILogger<SectionWriter> logger) : base(serviceProvider, logger)
        {
        }

        

   /*     protected override void InternalWrite()
        {
            foreach (EntityObject entity in Document.Entities)
            {
                if(entity is Circle)
                {
                    Circle circle = entity as Circle;
                    TextWriter.Write(OtherUtilities.ToDxfFormat(circle));
                    
                }
            }
        }*/
    }
}
