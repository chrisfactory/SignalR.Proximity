
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("SignalR.Proximity.Client, PublicKey=00240000048000009400000006020000002400005253413100040000010001009926991860ebf623e167c030204de56923186f33a4b5546d4406e25ff98f1d48478124ed39c3e52cd1058846fab8a4174bc05e775d29ee00b295fd91f85992121da71fa689a2bfba68f26fc4be46d13a5f047f998f715ad687b922b97f40bb83a08aa92eda6900e1ed44ac16e5ad0468f77a2d3307c45f609c63047e8190e4ec")]
[assembly: InternalsVisibleTo("SignalR.Proximity.Notifier, PublicKey=00240000048000009400000006020000002400005253413100040000010001009926991860ebf623e167c030204de56923186f33a4b5546d4406e25ff98f1d48478124ed39c3e52cd1058846fab8a4174bc05e775d29ee00b295fd91f85992121da71fa689a2bfba68f26fc4be46d13a5f047f998f715ad687b922b97f40bb83a08aa92eda6900e1ed44ac16e5ad0468f77a2d3307c45f609c63047e8190e4ec")]


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