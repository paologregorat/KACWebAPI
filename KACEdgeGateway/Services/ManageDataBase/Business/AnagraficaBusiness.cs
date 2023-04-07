using EdgeGateway.Services.ManageDataBase.Concrete.GestioneAnagrafica;
using KACEdgeGateway.Services.ManageDataBase.Abstract;
using KACGatewayContextLibrary.Domain;
using KACGatewayContextLibrary.Domain.Entity;
using KVMWebAPI.Domain.Command.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGateway.Services.ManageDataBase.Business
{
    public class AnagraficaBusiness : GenericBusiness<Anagrafica>
    {
        public CommandResponse Save(Anagrafica item)
        {
            var toUpdate = Context.Anagrafiche.FirstOrDefault(a => a.ID == item.ID || (a.Nome == item.Nome && a.Cognome == item.Cognome));
            if (toUpdate == null)
            {
                return Insert(item);
            } else
            {
                item.ID = toUpdate.ID;
                return Update(item);
            }
  
        }
    }
}
