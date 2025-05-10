using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mutuelleApi.models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime ModifieLe { get; set; } = DateTime.Now;
        public int? ModifiePar { get; set; }
    }
}