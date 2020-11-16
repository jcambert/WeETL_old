using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WeEFLastic.Infrastructure.Internal
{
    public sealed class ElasticOptionsExtension : IDbContextOptionsExtension
    {
        public DbContextOptionsExtensionInfo Info => throw new NotImplementedException();

        public void ApplyServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        public void Validate(IDbContextOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
