

using System.ComponentModel.DataAnnotations.Schema;

namespace MT.E_Sourcing.Order.Domain.Entities.Base
{
    public abstract class Entity : IEntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int id { get; protected set; }

        public Entity Clone ()
        {
            return (Entity)this.MemberwiseClone();
        }
    }
}
