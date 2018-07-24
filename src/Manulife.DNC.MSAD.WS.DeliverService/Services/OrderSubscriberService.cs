﻿using Dapper;
using DotNetCore.CAP;
using Manulife.DNC.MSAD.Common;
using Manulife.DNC.MSAD.WS.Events;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Manulife.DNC.MSAD.WS.DeliveryService.Services
{
    public class OrderSubscriberService : IOrderSubscriberService, ICapSubscribe
    {
        private readonly string _connStr;

        public OrderSubscriberService(string connStr)
        {
            _connStr = connStr;
        }

        [CapSubscribe(EventConstants.EVENT_NAME_CREATE_ORDER)]
        public async Task ConsumeOrderMessage(OrderMessage message)
        {
            await Console.Out.WriteLineAsync($"[DeliveryService] Received message : {JsonHelper.SerializeObject(message)}");
            await AddDeliveryRecordAsync(message);
        }

        private async Task<bool> AddDeliveryRecordAsync(OrderMessage order)
        {
            //throw new Exception("test"); // just for demo use
            using (var conn = new SqlConnection(_connStr))
            {
                string sqlCommand = @"INSERT INTO [dbo].[Deliveries] (DeliveryID, OrderID, ProductID, OrderUserID, CreatedTime)
                                                            VALUES (@DeliveryID, @OrderID, @ProductID, @OrderUserID, @CreatedTime)";

                int count = await conn.ExecuteAsync(sqlCommand, param: new
                {
                    DeliveryID = Guid.NewGuid().ToString(),
                    OrderID = order.ID,
                    OrderUserID = order.OrderUserID,
                    ProductID = order.ProductID,
                    CreatedTime = DateTime.Now
                });

                return count > 0;
            }
        }
    }
}
