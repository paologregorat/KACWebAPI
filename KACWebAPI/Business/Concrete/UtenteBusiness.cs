using KVMWebAPI.Domain.Command.Commands;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAPI_CQRS.Domain.Infrastructure.Authorization;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using KVMWebAPI.Infrastructure.Utility.RequestFilters;
using static KVMWebAPI.Controllers.WebControllerBase;
using KACWebAPI.Business.Abstract;
using KACCloudContextLibrary.Domain;
using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KACCloudContextLibrary.Domain.Entity;
using KACWebAPI.Domain.Command.Commands.Utente;
using KACWebAPI.Domain.Query.Queries.Utente.Concrete;
using KACWebAPI.Infrastructure.Utility;
using KACWebAPI.Domain.Query.Queries.Ruolo.Concrete;

namespace KACWebAPI.Business.Concrete
{
    public class UtenteBusiness : GenericBusiness<Utente, IUtenteSerializer, UtenteDTO>, IUtenteBusiness
    {
        private RuoloSerializer _serializer;
        public UtenteBusiness(KACCloudContext context, IUtenteSerializer serializer) : base(context, serializer, new string[] {"Ruolo"})
        {
        }

        #region metodi custom
        public CommandResponse Save(SaveUtenteCommand command)
        {
            var response = new CommandResponse()
            {
                Success = false
            };


            Utente toUpdate = Context.Utenti.FirstOrDefault(e => e.ID == command.Utente.ID);
            //sono in update di un utente (non ho passato la password)
            if (toUpdate == default)
            {
                //assegnpo l'attuale password
                command.Utente.Password = CryptoUtils.Encrypt(command.Utente.UserName);
                command.Utente.CreationDate = DateTime.Now;
                command.Utente.LastEditDate = DateTime.Now;
                Context.Utenti.Add(command.Utente);
            }
            else
            {
                //assegno l'attuale password
                command.Utente.Password = toUpdate.Password;
                command.Utente.CreationDate = toUpdate.CreationDate;
                command.Utente.SetUpdated();
                Context.Entry(toUpdate).CurrentValues.SetValues(command.Utente);
            }

            Context.SaveChanges();
            response.ID = command.Utente.ID;
            response.Success = true;
            response.Message = "Entity salvata.";

            return response;
        }

        public Utente GetUtente(string username, string password)
        {
            var entity = Context.Utenti.Include("Ruolo").FirstOrDefault(c => c.UserName == username && c.Password == CryptoUtils.Encrypt(password));
            return entity;
        }

        public CommandResponse CreteToken(CreateTokenCommand command)
        {
            var response = new CommandResponse()
            {
                Success = false
            };

            var utente = GetUtente(command.UserName, command.Password);
            if (utente == default)
            {
                response.Success = false;
                response.Message = "Operatore non trovato.";
                return response;
            }

            var token = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create("grgpla74a26g284d"))
                .AddSubject(utente.Cognome)
                .AddIssuer("Fiver.Security.Bearer")
                .AddAudience("Fiver.Security.Bearer")
                .AddClaim("ID", utente.ID.ToString())
                .AddExpiry(1440)
                .Build();


            response.ID = utente.ID;
            response.Success = true;
            response.Message = token.Value;

            return response;
        }

        public bool IsUserEnabled(Guid? userID, int abilitazione)
        {
            var user = Context.Utenti.FirstOrDefault(u => u.ID == userID);
            if (user != default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CommandResponse CambiaPassword(CambiaPasswordCommand command)
        {
            var response = new CommandResponse()
            {
                Success = false
            };

            var utente = Context.Utenti.FirstOrDefault(u => u.ID == command.CambioPassword.UtenteID && u.Password == CryptoUtils.Encrypt(command.CambioPassword.PasswordVecchia));
            if (utente == default)
            {
                response.Success = false;
                response.Message = "Utente non trovato o password errata.";
                return response;
            }

            utente.Password = CryptoUtils.Encrypt(command.CambioPassword.PasswordNuova);
            Context.Entry(utente).CurrentValues.SetValues(utente);
            Context.SaveChanges();

            response.ID = utente.ID;
            response.Success = true;
            response.Message = "Entity salvata.";

            return response;
        }

        public CommandResponse AdministratorCambiaPassword(AdministratorCambiaPasswordCommand command)
        {
            var response = new CommandResponse()
            {
                Success = false
            };

            var utente = Context.Utenti.FirstOrDefault(u => u.ID == command.CambioPassword.UtenteID);
            if (utente == default)
            {
                response.Success = false;
                response.Message = "Utente non trovato o password errata.";
                return response;
            }

            utente.Password = CryptoUtils.Encrypt(command.CambioPassword.NuovaPassword);
            Context.Entry(utente).CurrentValues.SetValues(utente);
            Context.SaveChanges();

            response.ID = utente.ID;
            response.Success = true;
            response.Message = "Entity salvata.";

            return response;
        }

        #endregion
    }

}