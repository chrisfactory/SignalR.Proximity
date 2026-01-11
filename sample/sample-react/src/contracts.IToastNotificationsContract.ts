export interface ToasterRequest {
    Title: string;
    Message: string;
    FromUser: string;
}

export interface IToastNotificationsContract {
    ShowInformation(request: ToasterRequest): void;
    ShowSuccess(request: ToasterRequest): void;
    ShowWarning(request: ToasterRequest): void;
    ShowError(request: ToasterRequest): void;
}

export const toastSignatures = {
    ShowInformation: "Void ShowInformation(Sample.SignalR.Proximity.Toaster.ToasterRequest)",
    ShowSuccess: "Void ShowSuccess(Sample.SignalR.Proximity.Toaster.ToasterRequest)",
    ShowWarning: "Void ShowWarning(Sample.SignalR.Proximity.Toaster.ToasterRequest)",
    ShowError: "Void ShowError(Sample.SignalR.Proximity.Toaster.ToasterRequest)"
};

export const toastPath = "sample.signalr.proximity.toaster.itoastnotificationscontract";
