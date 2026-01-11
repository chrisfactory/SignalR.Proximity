import * as signalR from "@microsoft/signalr";
import { ProximityConnection } from "./client";

export interface IProximityBuilder {
    withUrl(url: string): IProximityBuilder;
    withBaseUrl(url: string): IProximityBuilder;
    withPath(path: string): IProximityBuilder;
    withUserName(userName: string): IProximityBuilder;
    withAutomaticReconnect(retryDelays?: number[]): IProximityBuilder;
    configureConnection(callback: (options: signalR.IHttpConnectionOptions) => void): IProximityBuilder;
    build(): ProximityConnection;
}

export class ProximityBuilder implements IProximityBuilder {
    private _url: string = "";
    private _baseUrl: string = "";
    private _path: string = "";
    private _userName: string = "";
    private _retryDelays: number[] | undefined;
    private _connectionOptions: signalR.IHttpConnectionOptions = {};

    withUrl(url: string): IProximityBuilder {
        this._url = url;
        return this;
    }

    withBaseUrl(url: string): IProximityBuilder {
        this._baseUrl = url;
        return this;
    }

    withPath(path: string): IProximityBuilder {
        this._path = path;
        return this;
    }

    withUserName(userName: string): IProximityBuilder {
        this._userName = userName;
        return this;
    }

    withAutomaticReconnect(retryDelays?: number[]): IProximityBuilder {
        this._retryDelays = retryDelays;
        return this;
    }

    configureConnection(callback: (options: signalR.IHttpConnectionOptions) => void): IProximityBuilder {
        callback(this._connectionOptions);
        return this;
    }

    build(): ProximityConnection {
        let finalUrl = this._url;

        // If no full URL provided, construct it from Base + Path
        if (!finalUrl && this._baseUrl) {
            finalUrl = this._baseUrl;
            if (!finalUrl.endsWith("/")) finalUrl += "/";
            if (this._path.startsWith("/")) finalUrl += this._path.substring(1);
            else finalUrl += this._path;
        }

        if (!finalUrl) {
            throw new Error("Url is required (use withUrl or withBaseUrl + withPath)");
        }

        // Handle UserName
        if (this._userName) {
            // Add to Headers (for LongPolling/SSE)
            if (!this._connectionOptions.headers) {
                this._connectionOptions.headers = {};
            }
            // @ts-ignore: headers can be string dictionary or other types, assuming dictionary here for simplicity or standard signalR usage
            (this._connectionOptions.headers as any)["username"] = this._userName;

            // Add to Query String (for WebSockets)
            const separator = finalUrl.includes("?") ? "&" : "?";
            finalUrl += `${separator}username=${encodeURIComponent(this._userName)}`;
        }

        let builder = new signalR.HubConnectionBuilder()
            .withUrl(finalUrl, this._connectionOptions);

        if (this._retryDelays) {
            builder = builder.withAutomaticReconnect(this._retryDelays);
        } else {
            builder = builder.withAutomaticReconnect();
        }

        const hubConnection = builder.build();
        return new ProximityConnection(hubConnection);
    }
}
