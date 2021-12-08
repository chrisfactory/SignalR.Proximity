using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public static class HubConnectionExtensons
    {
        public static async Task<bool> StartWithRetryAsync(this HubConnection connection, IRetryPolicy policy, CancellationToken token)
        {
            RetryContext context = new RetryContext();
            while (true)
            {
                try
                {
                    if (await connection.InnerStartAsync(token))
                        return true;
                }
                catch when (token.IsCancellationRequested)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    context.RetryReason = ex;
                    var durration = policy.NextRetryDelay(context);
                    context.PreviousRetryCount++;
                    if (durration.HasValue)
                        await Task.Delay(durration.Value);
                    else return false;
                }
            }
        }
        private static async Task<bool> InnerStartAsync(this HubConnection connection, CancellationToken token)
        {
            try
            {
                if (connection.State == HubConnectionState.Disconnected)
                    await connection.StartAsync(token);

            }
            catch (Exception ex)
            {
                if (connection.State != HubConnectionState.Connected)//Situation de compétition sur StartAsync
                {
                    throw ex;
                }
            }
            return connection.State == HubConnectionState.Connected;
        }
    }
}
