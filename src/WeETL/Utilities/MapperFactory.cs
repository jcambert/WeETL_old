using AutoMapper;
using System;
using System.Collections.Generic;

namespace WeETL.Utilities
{
    public class MapperFactory
    {
        Dictionary<(Type, Type), Action<IMappingExpression<object,object>>> configurations = new Dictionary<(Type, Type), Action<IMappingExpression<object,object>>>();
        public virtual MapperFactory AddMapper<TIn, TOut>(Action<IMappingExpression<TIn, TOut>> cfg = null)
        {
            configurations[(typeof(TIn), typeof(TOut))] = cfg as Action<IMappingExpression<object, object>>;
            return this;
            /*var config = new MapperConfiguration(c =>
            {
                var s = c.CreateMap<TIn, TOut>();
                cfg.Invoke(s);
            });
            
            return  config.CreateMapper();*/
        }

        public IMapper Build()
        {
            var config = new MapperConfiguration(c =>
            {

                foreach (var item in configurations.Keys)
                {
                    var s = c.CreateMap(item.Item1,item.Item2);
                    var cfg = configurations[(item.Item1,item.Item2)];
                    
                    (cfg as Action<IMappingExpression<object,object>>) ?.Invoke((IMappingExpression<object, object>)s);
                }
            });
            return config.CreateMapper();
        }
    }
}
