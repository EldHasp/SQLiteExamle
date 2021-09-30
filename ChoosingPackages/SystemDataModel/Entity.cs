using System;
using System.ComponentModel.DataAnnotations;

namespace SystemDataModel
{
    public abstract class Entity<TIdDto, TEntity> where TIdDto : IdDto where TEntity : Entity<TIdDto, TEntity>, new()
    {
        [Key]
        public int? Id { get; set; }
        public void CopyFrom(TIdDto idDto)
        {
            if (idDto == null)
                throw new ArgumentNullException(nameof(idDto));
            if (idDto.Id != null && idDto.Id.Value != Id)
                throw new ArgumentException(nameof(idDto), "У DTO или не должно быть Id, или он должен быть таким же.");
            OverrideCopyFrom(idDto);
        }
        protected abstract void OverrideCopyFrom(TIdDto idDto);

        public bool ValuesEquals(TIdDto idDto)
            => idDto != null &&
               idDto.Id == Id &&
               OverrideEquals(idDto);
        protected abstract bool OverrideEquals(TIdDto idDto);

        public abstract TIdDto Create();
        public TEntity Create(TIdDto idDto)
        {
            if (idDto == null)
                throw new ArgumentNullException(nameof(idDto));
            if (idDto.Id != null)
                throw new ArgumentException(nameof(idDto), "У DTO не должно быть Id.");

            TEntity entity = new TEntity();
            entity.CopyFrom(idDto);

            return entity;
        }
    }
}
