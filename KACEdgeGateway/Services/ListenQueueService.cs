using EdgeGateway.Infrastructure.System;
using EdgeGateway.Services.ManageDataBase.Business;
using EdgeGateway.Services.ManageDataBase.Concrete.GestioneAnagrafica;
using KACGatewayContextLibrary.Domain.Entity;
using KQueue.RabbitMQ;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGateway.Services
{
    public class ListenQueueService
    {
        public class MessaggioQueue
        {
            public string message { get; set; }
            public DateTime sent { get; set; }
        }

        private readonly AnagraficaBusiness _tabellaBusiness = new AnagraficaBusiness();
        public void Execute()
        {
            try
            {
                var statusSystem = StatusSystem.GetInstance();

                var listener = RabbitMQListener.GetInstrance(statusSystem.Settings.HostNameQueueListener, statusSystem.Settings.PortQueueListener);
                listener.Listen("KAGUpdateGatewayDB", (queue, message) =>
                {
                    var a = message;
                });

                listener.Listen("KAGUpdateAnagrafica", (queue, message) =>
                {
                    var playload = JsonConvert.DeserializeObject<MessaggioQueue>((string)message);
                    var entity = JsonConvert.DeserializeObject<Anagrafica>((string)playload.message);

                    var command = new SaveAnagraficaCommand(entity);

                    var handler = AnagraficaCommandHandlerFactory.Build(command, _tabellaBusiness);
                    var response = handler.Execute();
                });

                listener.Listen("prova", (queue, message) =>
                {
                    var playload = JsonConvert.DeserializeObject<MessaggioQueue>((string)message);
                    var entity = JsonConvert.DeserializeObject<Anagrafica>((string)playload.message);

                    var command = new SaveAnagraficaCommand(entity);

                    var handler = AnagraficaCommandHandlerFactory.Build(command, _tabellaBusiness);
                    var response = handler.Execute();
                });

            }


            catch (Exception ex)
            {

            }
        }
    }
}
