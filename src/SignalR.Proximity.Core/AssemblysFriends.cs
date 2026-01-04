
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("SignalR.Proximity")]
[assembly: InternalsVisibleTo("SignalR.Proximity.Hosting")]


//Pour retrouver la public key:
//Dans le prompt de VS
// 1) sn.exe -p key.snk myFile.PublicKey 
// 2) sn.exe -tp myFile.PublicKey 
//Ex d'output :
//Utilitaire Strong Name Microsoft (R) .NET Framework Version 4.0.30319.0
//Copyright(c) Microsoft Corporation.Tous droits réservés.
//
//
//Clé publique (algorithme de hachage : sha1) :
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx...
//
//Le jeton de clé publique est zzzzzzz..
//3) remove file =>  myFile.PublicKey 