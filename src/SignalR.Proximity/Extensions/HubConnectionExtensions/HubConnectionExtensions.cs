﻿using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    internal static class HubConnectionExtensions
    {
        internal static async Task<bool> StartWithRetryAsync<TRetryPolicy>(this HubConnection connection, TRetryPolicy policy, CancellationToken token)
            where TRetryPolicy : IRetryPolicy
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
                        await Task.Delay(durration.Value, token);
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
            catch
            {
                if (connection.State != HubConnectionState.Connected)//Situation de compétition sur StartAsync
                {
                    throw;
                }
            }
            return connection.State == HubConnectionState.Connected;
        }
    }
}
