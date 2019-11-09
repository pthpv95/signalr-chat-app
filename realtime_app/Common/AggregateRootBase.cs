using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace realtime_app.Common
{
    public class AggregateRootBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }

        public DateTime Created { get; protected set; }

        public DateTime Updated { get; protected set; }
        
    }
}