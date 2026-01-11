export interface ISchoolContract {
    Send(message: string, from: string): void;
}

export const schoolSignatures = {
    Send: "Void Send(System.String, System.String)"
};

export const schoolPath = "sample.signalr.proximity.toaster.ischoolcontract";
