import * as signalR from "@microsoft/signalr";
export declare class ProximityConnection {
    private _connection;
    constructor(connection: signalR.HubConnection);
    /**
     * Starts the connection.
     */
    start(): Promise<void>;
    /**
     * Stops the connection.
     */
    stop(): Promise<void>;
    /**
     * Creates a proxy for sending notifications to the server.
     * The proxy will intercept method calls and invoke them on the SignalR hub.
     * @param signatureMap Optional map of method names to their .NET signatures
     * @param scope Optional scope definition (defaults to Notify.All)
     * @returns A proxy of type TContract
     */
    createNotifier<TContract extends object>(signatureMap?: Record<string, string>, scope?: ScopeDefinition): TContract;
    /**
     * Attaches a client implementation to receive notifications from the server.
     * The implementation's methods will be registered as handlers for SignalR messages.
     * @param instance An object implementing TContract
     * @param signatureMap Optional map of method names to their .NET signatures
     */
    attachClient<TContract extends object>(instance: TContract, signatureMap?: Record<string, string>): void;
    /**
     * Access to the underlying HubConnection.
     */
    get hubConnection(): signalR.HubConnection;
}
export interface ScopeDefinition {
    Request: string;
    Argument?: string | null;
    Arguments?: string[] | null;
}
