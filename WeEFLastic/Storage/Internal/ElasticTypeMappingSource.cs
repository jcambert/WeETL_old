using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;

namespace WeEFLastic.Storage.Internal
{
    public class ElasticTypeMappingSource : TypeMappingSource
    {
        public ElasticTypeMappingSource([NotNull] TypeMappingSourceDependencies dependencies) : base(dependencies)
        {
        }
    }
}
