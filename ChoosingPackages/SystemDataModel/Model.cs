using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemDataModel
{
    public class Model<TIdDto, TEntity> where TIdDto : IdDto where TEntity : Entity<TIdDto, TEntity>, new()
    {
        public string Source { get; }

        public Model(string source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Task.Run(() => { using var db = new ContactsDB(Source); });
        }

        public IReadOnlyList<TIdDto> GetAll()
        {
            using var db = new ContactsDB(Source);
            return db
                .Set<TEntity>()
                .ToList()
                .Select(en => en.Create())
                .ToList()
                .AsReadOnly();
        }
        public TIdDto Get(int id)
        {
            using var db = new ContactsDB(Source);
            TEntity en = db
                .Set<TEntity>()
                .Find(id);
            if (en == null)
                return null;
            else
                return en.Create();
        }

        private static readonly TEntity factory = new TEntity();
        public TIdDto Add(TIdDto idDto)
        {
            TEntity en = factory.Create(idDto);
            using var db = new ContactsDB(Source);
            db
               .Set<TEntity>()
               .Add(en);
            db.SaveChanges();

            return en.Create();
        }

        public void Change(TIdDto oldDto, TIdDto newDto)
        {
            if (oldDto?.Id == null)
                throw new ArgumentNullException(nameof(oldDto), "Cтарое значение и его Id не могут быть равны null.");

            using var db = new ContactsDB(Source);
            var en = db
               .Set<TEntity>()
               .Find(oldDto.Id);
            if (en == null)
                throw new ArgumentException("Записи с таким Id нет.", nameof(oldDto));
            if (!en.ValuesEquals(oldDto))
                throw new ArgumentException("Запись с таким Id содержит другие данные.", nameof(oldDto));
            en.CopyFrom(newDto);
            db.SaveChanges();
        }
        public void Remove(TIdDto idDto)
        {
            if (idDto?.Id == null)
                throw new ArgumentNullException(nameof(idDto), "Значение и его Id не могут быть равны null.");

            using var db = new ContactsDB(Source);
            var en = db
               .Set<TEntity>()
               .Find(idDto.Id);
            if (en == null)
                throw new ArgumentException("Записи с таким Id нет.", nameof(idDto));
            if (!en.ValuesEquals(idDto))
                throw new ArgumentException("Запись с таким Id содержит другие данные.", nameof(idDto));
            db.Set<TEntity>().Remove(en);
            db.SaveChanges();
        }

    }
}
