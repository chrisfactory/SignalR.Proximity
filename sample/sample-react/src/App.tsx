import { useEffect, useState } from 'react'
import './App.css'
import { ProximityBuilder, ProximityConnection } from '@signalr-proximity/client'
import type { ScopeDefinition } from '@signalr-proximity/client'
import type { ISchoolContract } from './contracts.ISchoolContract'
import { schoolContractSignatures, schoolContractPath } from './contracts.ISchoolContract'
import type { IToastNotificationsContract, ToasterRequest } from './contracts.IToastNotificationsContract'
import { toastNotificationsContractSignatures, toastNotificationsContractPath } from './contracts.IToastNotificationsContract'

// --- Data ---
const ALL_USERS = [
  "Professor", "Berlin", "Denver", "Helsinki", "Moscou", "Nairobi", "Oslo", "Rio", "Tokio"
];

const ALL_GROUPS = ["group1", "group2"];

const CURRENT_USER = "Professor";

// --- Interfaces for Log ---
interface LogEntry {
  time: string;
  message: string;
  type: 'school' | 'toast' | 'info';
}

function App() {
  const [schoolConnection, setSchoolConnection] = useState<ProximityConnection | null>(null);
  const [toastConnection, setToastConnection] = useState<ProximityConnection | null>(null);

  // UI State
  const [message, setMessage] = useState("Hello from React");
  const [logs, setLogs] = useState<LogEntry[]>([]);
  const [selectedUsers, setSelectedUsers] = useState<string[]>([]);
  const [selectedGroups, setSelectedGroups] = useState<string[]>([]);

  const addLog = (msg: string, type: 'school' | 'toast' | 'info') => {
    setLogs(prev => [{ time: new Date().toLocaleTimeString(), message: msg, type }, ...prev]);
  };

  // --- Handlers (defined inside component to access addLog) ---
  class SchoolHandler implements ISchoolContract {
    Send(message: string, from: string): void {
      console.log("SCHOOL MSG", message, from);
      addLog(`[SCHOOL] From ${from}: ${message}`, 'school');
    }
  }

  class ToastNotificationsHandler implements IToastNotificationsContract {
    ShowInformation(request: ToasterRequest): void { addLog(`[INFO] ${request.Title}: ${request.Message}`, 'toast'); }
    ShowSuccess(request: ToasterRequest): void { addLog(`[SUCCESS] ${request.Title}: ${request.Message}`, 'toast'); }
    ShowWarning(request: ToasterRequest): void { addLog(`[WARNING] ${request.Title}: ${request.Message}`, 'toast'); }
    ShowError(request: ToasterRequest): void { addLog(`[ERROR] ${request.Title}: ${request.Message}`, 'toast'); }
  }

  const baseUrl = "https://localhost:5011";

  useEffect(() => {
    let mounted = true;

    const schoolCnx = new ProximityBuilder()
      .withBaseUrl(baseUrl)
      .withPath(schoolContractPath)
      .withUserName(CURRENT_USER)
      .withAutomaticReconnect()
      .build();

    const toastCnx = new ProximityBuilder()
      .withBaseUrl(baseUrl)
      .withPath(toastNotificationsContractPath)
      .withUserName(CURRENT_USER)
      .withAutomaticReconnect()
      .build();

    const connect = async () => {
      try {
        // Attach handlers
        schoolCnx.attachClient(new SchoolHandler(), schoolContractSignatures);
        toastCnx.attachClient(new ToastNotificationsHandler(), toastNotificationsContractSignatures);

        await schoolCnx.start();
        if (mounted) {
          setSchoolConnection(schoolCnx);
          addLog("Connected to School Hub", 'info');
        }

        await toastCnx.start();
        if (mounted) {
          setToastConnection(toastCnx);
          addLog("Connected to Toast Hub", 'info');
        }

      } catch (e) {
        console.error("Connection failed", e);
        addLog(`Connection failed: ${e}`, 'info');
      }
    };

    connect();

    return () => {
      mounted = false;
      schoolCnx.stop();
      toastCnx.stop();
    };
  }, []);

  // --- Actions ---
  const send = async (scope: ScopeDefinition, suffixDesc: string) => {
    if (!schoolConnection) return;
    try {
      const proxy = schoolConnection.createNotifier<ISchoolContract>(schoolContractSignatures, scope);
      await proxy.Send(`${message} (${suffixDesc})`, CURRENT_USER);
      addLog(`Sent to ${suffixDesc}`, 'info');
    } catch (e) {
      console.error("Send failed", e);
      addLog(`Send failed: ${e}`, 'info');
    }
  };

  const sendToAll = () => send({ Request: "Notify.All" }, "All");
  const sendToOthers = () => send({ Request: "Notify.Others" }, "Others");

  const sendToUsers = () => {
    if (selectedUsers.length === 0) return alert("Select at least one user");
    send({ Request: "Notify.Users", Arguments: selectedUsers }, `Users [${selectedUsers.join(', ')}]`);
  };

  const sendToGroups = () => {
    if (selectedGroups.length === 0) return alert("Select at least one group");
    send({ Request: "Notify.Groups", Arguments: selectedGroups }, `Groups [${selectedGroups.join(', ')}]`);
  };

  // --- Toggles ---
  const toggleUser = (user: string) => {
    setSelectedUsers(prev => prev.includes(user) ? prev.filter(u => u !== user) : [...prev, user]);
  };
  const toggleGroup = (group: string) => {
    setSelectedGroups(prev => prev.includes(group) ? prev.filter(g => g !== group) : [...prev, group]);
  };

  return (
    <div className="App" style={{ display: 'flex', gap: '20px', padding: '20px', textAlign: 'left', minHeight: '100vh', boxSizing: 'border-box' }}>
      <div style={{ flex: 1, display: 'flex', flexDirection: 'column', gap: '20px' }}>
        <h1>Proximity React Client ({CURRENT_USER})</h1>

        <div className="card">
          <h3>Message</h3>
          <input
            type="text"
            value={message}
            onChange={(e) => setMessage(e.target.value)}
            style={{ width: '100%', padding: '8px', boxSizing: 'border-box' }}
          />
        </div>

        <div className="card" style={{ display: 'flex', gap: '10px' }}>
          <button onClick={sendToAll} disabled={!schoolConnection}>Send To All</button>
          <button onClick={sendToOthers} disabled={!schoolConnection}>Send To Others</button>
        </div>

        <div className="card">
          <h3>Target Users</h3>
          <div style={{ display: 'flex', flexWrap: 'wrap', gap: '10px' }}>
            {ALL_USERS.map(u => (
              <label key={u} style={{ cursor: 'pointer', display: 'flex', alignItems: 'center', gap: '5px' }}>
                <input
                  type="checkbox"
                  checked={selectedUsers.includes(u)}
                  onChange={() => toggleUser(u)}
                /> {u}
              </label>
            ))}
          </div>
          <button onClick={sendToUsers} disabled={!schoolConnection || selectedUsers.length === 0} style={{ marginTop: '10px' }}>
            Send to Selected Users
          </button>
        </div>

        <div className="card">
          <h3>Target Groups</h3>
          <div style={{ display: 'flex', flexWrap: 'wrap', gap: '10px' }}>
            {ALL_GROUPS.map(g => (
              <label key={g} style={{ cursor: 'pointer', display: 'flex', alignItems: 'center', gap: '5px' }}>
                <input
                  type="checkbox"
                  checked={selectedGroups.includes(g)}
                  onChange={() => toggleGroup(g)}
                /> {g}
              </label>
            ))}
          </div>
          <button onClick={sendToGroups} disabled={!schoolConnection || selectedGroups.length === 0} style={{ marginTop: '10px' }}>
            Send to Selected Groups
          </button>
        </div>

        <div className="card">
          <p>Status School: {schoolConnection ? "✅ Connected" : "❌ Disconnected"}</p>
          <p>Status Toast: {toastConnection ? "✅ Connected" : "❌ Disconnected"}</p>
        </div>
      </div>

      <div style={{ flex: 1, borderLeft: '1px solid #ccc', paddingLeft: '20px', display: 'flex', flexDirection: 'column' }}>
        <h3>Logs</h3>
        <div style={{
          flex: 1,
          overflowY: 'auto',
          background: '#f0f0f0',
          padding: '10px',
          borderRadius: '4px',
          fontFamily: 'monospace',
          fontSize: '0.9em',
          maxHeight: '90vh'
        }}>
          {logs.map((l, i) => (
            <div key={i} style={{ marginBottom: '4px', color: l.type === 'school' ? 'blue' : l.type === 'toast' ? 'green' : 'gray' }}>
              [{l.time}] {l.message}
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}

export default App
