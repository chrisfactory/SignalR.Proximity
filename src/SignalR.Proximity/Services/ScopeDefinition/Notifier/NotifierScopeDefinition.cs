using System.Collections.Generic;
namespace SignalR.Proximity
{
    public class NotifierScopeDefinition : ScopeDefinitionBase
    {
        protected const string TargetAll = "Notify.All";
        protected const string TargetAllExcept = "Notify.All.Except";
        protected const string TargetGroup = "Notify.Group";
        protected const string TargetGroups = "Notify.Groups";
        protected const string TargetGroupExcept = "Notify.Group.Except";
        protected const string TargetClient = "Notify.Client";
        protected const string TargetClients = "Notify.Clients";
        protected const string TargetUser = "Notify.User";
        protected const string TargetUsers = "Notify.Users";
        protected const string TargetOthers = "Notify.Others";
        protected const string TargetOthersInGroup = "Notify.Others.In.Group";

        public NotifierScopeDefinition() : base() { }
        private NotifierScopeDefinition(string request) : base(request, null, null) { }
        private NotifierScopeDefinition(string request, IReadOnlyList<string> arguments) : base(request, arguments, null) { }
        private NotifierScopeDefinition(string request, string argument) : base(request, null, argument) { }
        private NotifierScopeDefinition(string request, IReadOnlyList<string> arguments, string argument) : base(request, arguments, argument) { }

        public static NotifierScopeDefinition All() => new NotifierScopeDefinition(TargetAll);
        public static NotifierScopeDefinition AllExcept(string[] excludedConnectionIds) => new NotifierScopeDefinition(TargetAllExcept, excludedConnectionIds);
        public static NotifierScopeDefinition Group(string group) => new NotifierScopeDefinition(TargetGroup, group);
        public static NotifierScopeDefinition Groups(string[] groups) => new NotifierScopeDefinition(TargetGroups, groups);
        public static NotifierScopeDefinition GroupExcept(string group, string[] excludedConnectionIds) => new NotifierScopeDefinition(TargetGroupExcept, excludedConnectionIds, group);
        public static NotifierScopeDefinition Client(string connectionId) => new NotifierScopeDefinition(TargetClient, connectionId);
        public static NotifierScopeDefinition Clients(string[] connectionIds) => new NotifierScopeDefinition(TargetClients, connectionIds);
        public static NotifierScopeDefinition User(string userId) => new NotifierScopeDefinition(TargetUser, userId);
        public static NotifierScopeDefinition Users(string[] userIds) => new NotifierScopeDefinition(TargetUsers, userIds);
        public static NotifierScopeDefinition Others() => new NotifierScopeDefinition(TargetOthers);
        public static NotifierScopeDefinition OthersInGroup(string group) => new NotifierScopeDefinition(TargetOthersInGroup, group);

    }
}
