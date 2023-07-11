using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Nip.Models.Interfaces
{
    public class DateTimeGeneratorek : ValueGenerator<DateTime>
    {
        public override bool GeneratesTemporaryValues => false;
        private DateTime UtcDate()
        {
            return DateTime.UtcNow;
        }
        public override DateTime Next(EntityEntry entry)
        {
            entry.State = EntityState.Modified;
            return UtcDate();
        }
        protected override object NextValue(EntityEntry entry)
        {
            return UtcDate();
        }
    }
}
