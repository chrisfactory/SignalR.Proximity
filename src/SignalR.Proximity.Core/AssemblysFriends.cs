
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("SignalR.Proximity, PublicKey=002400000480000094000000060200000024000052534131000400000100010041f0c9eb13810b3ed9cbb7bbc3b6b39007283747f1f5824fe438519d5e3e04bad00d63599d24f3f08a45d31537bfe14f695cfdaeee019b442c73a8c7443b1ae7cc5b41164c15e2d3eaa3206a1547a52cfc5c61d96f3fd85a30aeec763557c5c78b7260654510b36c90a073d3f4a6ee637305a76b5c12d4a51b2329513042bcc4")]
[assembly: InternalsVisibleTo("SignalR.Proximity.Hosting, PublicKey=002400000480000094000000060200000024000052534131000400000100010041f0c9eb13810b3ed9cbb7bbc3b6b39007283747f1f5824fe438519d5e3e04bad00d63599d24f3f08a45d31537bfe14f695cfdaeee019b442c73a8c7443b1ae7cc5b41164c15e2d3eaa3206a1547a52cfc5c61d96f3fd85a30aeec763557c5c78b7260654510b36c90a073d3f4a6ee637305a76b5c12d4a51b2329513042bcc4")]


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