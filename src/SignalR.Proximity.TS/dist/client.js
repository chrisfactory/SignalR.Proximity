export class ProximityConnection {
    constructor(connection) {
        this._connection = connection;
    }
    /**
     * Starts the connection.
     */
    async start() {
        await this._connection.start();
    }
    /**
     * Stops the connection.
     */
    async stop() {
        await this._connection.stop();
    }
    /**
     * Creates a proxy for sending notifications to the server.
     * The proxy will intercept method calls and invoke them on the SignalR hub.
     * @param signatureMap Optional map of method names to their .NET signatures
     * @param scope Optional scope definition (defaults to Notify.All)
     * @returns A proxy of type TContract
     */
    createNotifier(signatureMap, scope) {
        const connection = this._connection;
        const finalScope = scope || { Request: "Notify.All" };
        return new Proxy({}, {
            get: (target, prop) => {
                if (typeof prop === "string") {
                    return (...args) => {
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
    attachClient(instance, signatureMap) {
        const proto = Object.getPrototypeOf(instance);
        const methodNames = Object.getOwnPropertyNames(proto)
            .filter(prop => prop !== "constructor" && typeof instance[prop] === "function");
        // Also check if it's a plain object
        const ownProps = Object.getOwnPropertyNames(instance)
            .filter(prop => typeof instance[prop] === "function");
        const allMethods = new Set([...methodNames, ...ownProps]);
        for (const method of allMethods) {
            const handler = instance[method].bind(instance);
            const methodKey = signatureMap?.[method] || method;
            this._connection.on(methodKey, handler);
        }
    }
    /**
     * Access to the underlying HubConnection.
     */
    get hubConnection() {
        return this._connection;
    }
}
