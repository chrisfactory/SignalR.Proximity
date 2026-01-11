# @signalr-proximity/client

A strongly-typed SignalR client for the `SignalR.Proximity` ecosystem. This library enables seamless interoperability between TypeScript/React applications and .NET backends using the Proximity pattern (dynamic interface-based contracts).

## Features

*   **Strong Typing**: Use TypeScript interfaces to define your communication contracts.
*   **Dynamic Proxies**: Send messages using method calls on proxy objects (`proxy.Send(...)`), similar to the .NET client.
*   **Scope Support**: Target specific users, groups, or clients (`Notify.All`, `Notify.Others`, `Notify.Users`, etc.).
*   **.NET Interoperability**: Full support for mapping .NET method signatures (`Void Send(System.String)`), ensuring compatibility with existing WPF/Console clients.
*   **Reconnection**: Built-in support for automatic reconnection.

## Installation

```bash
npm install @signalr-proximity/client
```

## Usage

### 1. Define your Contracts

Create TypeScript interfaces that mirror your C# contracts.

```typescript
// Matches ISchoolContract.cs
export interface ISchoolContract {
    Send(message: string, from: string): void;
}
```

### 2. Configure the Connection

Use `ProximityBuilder` to setup the connection. You can use the fluent API to construct the endpoint URL and identifying information.

```typescript
import { ProximityBuilder } from '@signalr-proximity/client';
import { schoolPath } from './contracts.ISchoolContract';

const connection = new ProximityBuilder()
    .withBaseUrl("https://localhost:5011")    // The root URL of your server
    .withPath(schoolPath)                     // The specific hub path (often exported from your contract)
    .withUserName("JohnDoe")                  // Adds 'username' header and query string for auth
    .withAutomaticReconnect()
    .build();

await connection.start();
```

### 3. Usage: Sending Messages (Notifier)

To send messages, create a notifier proxy. 
**Crucial**: You must provide a "Signature Map" to match the .NET server's method keys.

```typescript
// Map method names to .NET signatures (ToString() of MethodInfo)
const schoolSignatures = {
    Send: "Void Send(System.String, System.String)"
};

// Create a proxy
const proxy = connection.createNotifier<ISchoolContract>(schoolSignatures);

// Send a message (broadcast to All by default)
await proxy.Send("Hello World", "John");
```

#### Targeting Specific Users/Groups (Scopes)

You can specify a scope when creating the notifier.

```typescript
import { ScopeDefinition } from '@signalr-proximity/client';

// Send to specific users
const userScope: ScopeDefinition = { 
    Request: "Notify.Users", 
    Arguments: ["UserA", "UserB"] 
};

const userProxy = connection.createNotifier<ISchoolContract>(schoolSignatures, userScope);
await userProxy.Send("Secret Message", "John");

// Other scopes: "Notify.All", "Notify.Others", "Notify.Groups", "Notify.Client"
```

### 4. Usage: Receiving Messages (Client)

Implement the contract interface and attach it to the connection.

```typescript
class SchoolHandler implements ISchoolContract {
    Send(message: string, from: string): void {
        console.log(`Received from ${from}: ${message}`);
    }
}

// Attach the handler
connection.attachClient(new SchoolHandler(), schoolSignatures);
```

## Why Signature Maps?

`SignalR.Proximity` uses the full string representation of the .NET method signature (e.g., `Void Send(System.String, System.String)`) as the event key. Since TypeScript interfaces don't exist at runtime, you must explicitly provide this mapping so the client knows which event string to specific for `Send`.
