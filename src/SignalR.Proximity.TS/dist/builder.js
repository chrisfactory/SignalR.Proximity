import * as signalR from "@microsoft/signalr";
import { ProximityConnection } from "./client";
export class ProximityBuilder {
    constructor() {
        this._url = "";
        this._baseUrl = "";
        this._path = "";
        this._userName = "";
        this._connectionOptions = {};
    }
    withUrl(url) {
        this._url = url;
        return this;
    }
    withBaseUrl(url) {
        this._baseUrl = url;
        return this;
    }
    withPath(path) {
        this._path = path;
        return this;
    }
    withUserName(userName) {
        this._userName = userName;
        return this;
    }
    withAutomaticReconnect(retryDelays) {
        this._retryDelays = retryDelays;
        return this;
    }
    configureConnection(callback) {
        callback(this._connectionOptions);
        return this;
    }
    build() {
        let finalUrl = this._url;
        // If no full URL provided, construct it from Base + Path
        if (!finalUrl && this._baseUrl) {
            finalUrl = this._baseUrl;
            if (!finalUrl.endsWith("/"))
                finalUrl += "/";
            if (this._path.startsWith("/"))
                finalUrl += this._path.substring(1);
            else
                finalUrl += this._path;
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
            this._connectionOptions.headers["username"] = this._userName;
            // Add to Query String (for WebSockets)
            const separator = finalUrl.includes("?") ? "&" : "?";
            finalUrl += `${separator}username=${encodeURIComponent(this._userName)}`;
        }
        let builder = new signalR.HubConnectionBuilder()
            .withUrl(finalUrl, this._connectionOptions);
        if (this._retryDelays) {
            builder = builder.withAutomaticReconnect(this._retryDelays);
        }
        else {
            builder = builder.withAutomaticReconnect();
        }
        const hubConnection = builder.build();
        return new ProximityConnection(hubConnection);
    }
}
