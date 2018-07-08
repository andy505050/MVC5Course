using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC5Course.Models
{
    public class ClientRepository : EFRepository<Client>, IClientRepository
    {
        public override IQueryable<Client> All()
        {
            return base.All().Where(x => !x.isDelete.Equals("Y"));
        }
        public Client Find(int id)
        {
            return this.All().FirstOrDefault(x => x.ClientId.Equals(id));
        }

        public IQueryable<Client> Search(string Keyword)
        {
            var data = this.All().OrderByDescending(x => x.ClientId).AsQueryable();
            if (!string.IsNullOrEmpty(Keyword))
                data = data.Where(x => x.FirstName.Contains(Keyword));

            return data;
        }

        public override void Delete(Client entity)
        {
            entity.isDelete = "Y";
        }
    }

    public interface IClientRepository : IRepository<Client>
    {

    }
}