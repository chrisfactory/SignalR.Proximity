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
export declare class ProximityBuilder implements IProximityBuilder {
    private _url;
    private _baseUrl;
    private _path;
    private _userName;
    private _retryDelays;
    private _connectionOptions;
    withUrl(url: string): IProximityBuilder;
    withBaseUrl(url: string): IProximityBuilder;
    withPath(path: string): IProximityBuilder;
    withUserName(userName: string): IProximityBuilder;
    withAutomaticReconnect(retryDelays?: number[]): IProximityBuilder;
    configureConnection(callback: (options: signalR.IHttpConnectionOptions) => void): IProximityBuilder;
    build(): ProximityConnection;
}
