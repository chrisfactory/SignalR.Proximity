import * as signalR from "@microsoft/signalr";

export class ProximityConnection {
    private _connection: signalR.HubConnection;

    constructor(connection: signalR.HubConnection) {
        this._connection = connection;
    }

    /**
     * Starts the connection.
     */
    async start(): Promise<void> {
        await this._connection.start();
    }

    /**
     * Stops the connection.
     */
    async stop(): Promise<void> {
        await this._connection.stop();
    }

    /**
     * Creates a proxy for sending notifications to the server.
     * The proxy will intercept method calls and invoke them on the SignalR hub.
     * @param signatureMap Optional map of method names to their .NET signatures
     * @param scope Optional scope definition (defaults to Notify.All)
     * @returns A proxy of type TContract
     */
    createNotifier<TContract extends object>(signatureMap?: Record<string, string>, scope?: ScopeDefinition): TContract {
        const connection = this._connection;
        const finalScope = scope || { Request: "Notify.All" };

        return new Proxy({} as TContract, {
            get: (target, prop) => {
                if (typeof prop === "string") {
                    return (...args: any[]) => {
                        const methodKey = signatureMap?.[prop] || prop;
                        const request = {
                            Scope: finalScope,
                            Argument: methodKey,
                            Arguments: null
                        };
                        return connection.invoke("Interact", request, args);
                    };
                }
                return undefined;
            }
        });
    }

    /**
     * Attaches a client implementation to receive notifications from the server.
     * The implementation's methods will be registered as handlers for SignalR messages.
     * @param instance An object implementing TContract
     * @param signatureMap Optional map of method names to their .NET signatures
     */
    attachClient<TContract extends object>(instance: TContract, signatureMap?: Record<string, string>): void {
        const proto = Object.getPrototypeOf(instance);
        const methodNames = Object.getOwnPropertyNames(proto)
            .filter(prop => prop !== "constructor" && typeof (instance as any)[prop] === "function");

        // Also check if it's a plain object
        const ownProps = Object.getOwnPropertyNames(instance)
            .filter(prop => typeof (instance as any)[prop] === "function");

        const allMethods = new Set([...methodNames, ...ownProps]);

        for (const method of allMethods) {
            const handler = (instance as any)[method].bind(instance);
            const methodKey = signatureMap?.[method] || method;
            this._connection.on(methodKey, handler);
        }
    }

    /**
     * Access to the underlying HubConnection.
     */
    get hubConnection(): signalR.HubConnection {
        return this._connection;
    }
}

export interface ScopeDefinition {
    Request: string;
    Argument?: string | null;
    Arguments?: string[] | null;
}
