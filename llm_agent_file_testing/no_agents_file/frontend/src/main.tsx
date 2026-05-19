import React, { useEffect, useMemo, useState } from "react";
import { createRoot } from "react-dom/client";
import { Check, Download, Plus, RefreshCcw, Search, Send, Trash2, X } from "lucide-react";
import "./styles.css";

type Role = "SITE_ADMIN" | "MANAGER" | "EMPLOYEE";
type EntryType = "STANDARD" | "OVERTIME";
type ApprovalStatus = "PENDING" | "APPROVED" | "REJECTED" | "NOT_REQUIRED";

type Team = { id: number; name: string };
type Task = { id: number; name: string; paidTimeOff: boolean };
type ReferenceData = {
  user: { id: number; username: string; displayName: string; role: Role; team: string | null; standardDate: string };
  teams: Team[];
  tasks: Task[];
};
type EntryView = {
  id: number;
  workingDate: string;
  user: string;
  team: string;
  task: string;
  comments: string | null;
  hours: number;
  entryType: EntryType;
  approvalStatus: ApprovalStatus;
};
type ApprovalView = {
  id: number;
  requestType: "OVERTIME" | "DELETE_DAY";
  requester: string;
  workingDate: string;
  task: string | null;
  hours: number | null;
  status: ApprovalStatus;
};

type Line = { teamId: string; taskId: string; comments: string; hours: string };

async function api<T>(url: string, options: RequestInit = {}): Promise<T> {
  const response = await fetch(url, {
    ...options,
    headers: { "Content-Type": "application/json", ...(options.headers ?? {}) }
  });
  if (!response.ok) {
    let message = `${response.status} ${response.statusText}`;
    try {
      const body = await response.json();
      message = body.error ?? message;
    } catch {
      // keep HTTP message
    }
    throw new Error(message);
  }
  return response.json();
}

function App() {
  const [ref, setRef] = useState<ReferenceData | null>(null);
  const [entries, setEntries] = useState<EntryView[]>([]);
  const [managerEntries, setManagerEntries] = useState<EntryView[]>([]);
  const [approvals, setApprovals] = useState<ApprovalView[]>([]);
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");

  const load = async () => {
    setError("");
    const data = await api<ReferenceData>("/api/me");
    setRef(data);
    setEntries(await api<EntryView[]>("/api/entries"));
    if (data.user.role === "MANAGER" || data.user.role === "SITE_ADMIN") {
      setManagerEntries(await api<EntryView[]>("/api/manager/entries"));
      setApprovals(await api<ApprovalView[]>("/api/manager/approvals"));
    }
  };

  useEffect(() => {
    load().catch((err: Error) => setError(err.message));
  }, []);

  const notify = async (action: () => Promise<unknown>, success: string) => {
    setError("");
    setMessage("");
    try {
      await action();
      setMessage(success);
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : "Request failed");
    }
  };

  if (!ref) {
    return <main className="shell"><div className="status">Loading office time...</div>{error && <div className="alert">{error}</div>}</main>;
  }

  return (
    <main className="shell">
      <header className="topbar">
        <div>
          <h1>Office Time</h1>
          <p>{ref.user.displayName} · {ref.user.role.replace("_", " ")} · {ref.user.team ?? "No team"}</p>
        </div>
        <button className="iconButton" title="Refresh" onClick={() => load().catch((err: Error) => setError(err.message))}><RefreshCcw size={18} /></button>
      </header>

      {message && <div className="notice">{message}</div>}
      {error && <div className="alert">{error}</div>}

      <section className="grid">
        <EmployeePanel refData={ref} entries={entries} notify={notify} setEntries={setEntries} />
        {(ref.user.role === "MANAGER" || ref.user.role === "SITE_ADMIN") && (
          <ManagerPanel approvals={approvals} entries={managerEntries} notify={notify} />
        )}
      </section>
    </main>
  );
}

function EmployeePanel({ refData, entries, notify, setEntries }: {
  refData: ReferenceData;
  entries: EntryView[];
  notify: (action: () => Promise<unknown>, success: string) => Promise<void>;
  setEntries: (entries: EntryView[]) => void;
}) {
  const firstTeam = String(refData.teams[0]?.id ?? "");
  const firstTask = String(refData.tasks[0]?.id ?? "");
  const [lines, setLines] = useState<Line[]>([{ teamId: firstTeam, taskId: firstTask, comments: "", hours: "8.00" }]);
  const [overtime, setOvertime] = useState({ workingDate: refData.user.standardDate, teamId: firstTeam, taskId: firstTask, comments: "", hours: "1.00" });
  const [search, setSearch] = useState({ start: "", end: "" });
  const [deleteDate, setDeleteDate] = useState(refData.user.standardDate);
  const total = useMemo(() => lines.reduce((sum, line) => sum + Number(line.hours || 0), 0), [lines]);

  const updateLine = (index: number, patch: Partial<Line>) => {
    setLines(lines.map((line, current) => current === index ? { ...line, ...patch } : line));
  };

  return (
    <section className="panel">
      <h2>Employee Entry</h2>
      <div className="subhead">Standard date: {refData.user.standardDate}</div>
      <div className="lineList">
        {lines.map((line, index) => (
          <div className="entryLine" key={index}>
            <Select value={line.teamId} onChange={(teamId) => updateLine(index, { teamId })} items={refData.teams} />
            <TaskSelect value={line.taskId} onChange={(taskId) => updateLine(index, { taskId })} tasks={refData.tasks} />
            <input value={line.comments} onChange={(event) => updateLine(index, { comments: event.target.value })} placeholder="Comments" />
            <input className="hours" type="number" min="0.25" step="0.25" value={line.hours} onChange={(event) => updateLine(index, { hours: event.target.value })} />
          </div>
        ))}
      </div>
      <div className="toolbar">
        <button title="Add line" onClick={() => setLines([...lines, { teamId: firstTeam, taskId: firstTask, comments: "", hours: "0.25" }])}><Plus size={16} /> Add</button>
        <span className={total === 8 ? "total ok" : "total"}>{total.toFixed(2)} / 8.00</span>
        <button title="Submit standard hours" onClick={() => notify(() => api("/api/entries/standard", { method: "POST", body: JSON.stringify({ lines: lines.map(toPayload) }) }), "Standard hours submitted")}><Send size={16} /> Submit</button>
      </div>

      <h3>Overtime</h3>
      <div className="entryLine">
        <input type="date" max={refData.user.standardDate} value={overtime.workingDate} onChange={(event) => setOvertime({ ...overtime, workingDate: event.target.value })} />
        <Select value={overtime.teamId} onChange={(teamId) => setOvertime({ ...overtime, teamId })} items={refData.teams} />
        <TaskSelect value={overtime.taskId} onChange={(taskId) => setOvertime({ ...overtime, taskId })} tasks={refData.tasks} />
        <input value={overtime.comments} onChange={(event) => setOvertime({ ...overtime, comments: event.target.value })} placeholder="Comments" />
        <input className="hours" type="number" min="0.25" step="0.25" value={overtime.hours} onChange={(event) => setOvertime({ ...overtime, hours: event.target.value })} />
      </div>
      <div className="toolbar">
        <button title="Request overtime approval" onClick={() => notify(() => api("/api/entries/overtime", { method: "POST", body: JSON.stringify(toPayload(overtime)) }), "Overtime submitted for approval")}><Send size={16} /> Request</button>
      </div>

      <h3>History</h3>
      <div className="toolbar">
        <input type="date" value={search.start} onChange={(event) => setSearch({ ...search, start: event.target.value })} />
        <input type="date" value={search.end} onChange={(event) => setSearch({ ...search, end: event.target.value })} />
        <button title="Search entries" onClick={() => api<EntryView[]>(`/api/entries?start=${search.start}&end=${search.end}`).then(setEntries)}><Search size={16} /> Search</button>
      </div>
      <div className="toolbar">
        <input type="date" value={deleteDate} onChange={(event) => setDeleteDate(event.target.value)} />
        <button title="Request day deletion" onClick={() => notify(() => api("/api/entries/delete-requests", { method: "POST", body: JSON.stringify({ workingDate: deleteDate }) }), "Deletion requested")}><Trash2 size={16} /> Delete Request</button>
      </div>
      <EntryTable entries={entries} />
    </section>
  );
}

function ManagerPanel({ approvals, entries, notify }: {
  approvals: ApprovalView[];
  entries: EntryView[];
  notify: (action: () => Promise<unknown>, success: string) => Promise<void>;
}) {
  return (
    <section className="panel">
      <h2>Manager Review</h2>
      <div className="toolbar">
        <a className="buttonLink" href="/api/manager/entries.csv"><Download size={16} /> CSV</a>
      </div>
      <h3>Approvals</h3>
      <div className="approvalList">
        {approvals.map((approval) => (
          <div className="approval" key={approval.id}>
            <div>
              <strong>{approval.requester}</strong>
              <span>{approval.requestType.replace("_", " ")} · {approval.workingDate} · {approval.task ?? "Day deletion"} {approval.hours ? `· ${approval.hours}h` : ""}</span>
            </div>
            <button title="Approve" onClick={() => notify(() => api(`/api/manager/approvals/${approval.id}/approve`, { method: "POST" }), "Request approved")}><Check size={16} /></button>
            <button title="Reject" onClick={() => notify(() => api(`/api/manager/approvals/${approval.id}/reject`, { method: "POST" }), "Request rejected")}><X size={16} /></button>
          </div>
        ))}
        {approvals.length === 0 && <div className="empty">No pending approvals</div>}
      </div>
      <h3>Team Entries</h3>
      <EntryTable entries={entries} />
    </section>
  );
}

function EntryTable({ entries }: { entries: EntryView[] }) {
  return (
    <div className="tableWrap">
      <table>
        <thead><tr><th>Date</th><th>User</th><th>Team</th><th>Task</th><th>Type</th><th>Status</th><th>Hours</th></tr></thead>
        <tbody>
          {entries.map((entry) => (
            <tr key={entry.id}>
              <td>{entry.workingDate}</td><td>{entry.user}</td><td>{entry.team}</td><td>{entry.task}</td><td>{entry.entryType}</td><td>{entry.approvalStatus}</td><td>{entry.hours}</td>
            </tr>
          ))}
        </tbody>
      </table>
      {entries.length === 0 && <div className="empty">No entries</div>}
    </div>
  );
}

function Select({ value, onChange, items }: { value: string; onChange: (value: string) => void; items: Team[] }) {
  return <select value={value} onChange={(event) => onChange(event.target.value)}>{items.map((item) => <option key={item.id} value={item.id}>{item.name}</option>)}</select>;
}

function TaskSelect({ value, onChange, tasks }: { value: string; onChange: (value: string) => void; tasks: Task[] }) {
  return <select value={value} onChange={(event) => onChange(event.target.value)}>{tasks.map((task) => <option key={task.id} value={task.id}>{task.name}{task.paidTimeOff ? " (PTO)" : ""}</option>)}</select>;
}

function toPayload(line: Line | typeof overtimeShape) {
  return { ...line, teamId: Number(line.teamId), taskId: Number(line.taskId), hours: Number(line.hours) };
}

const overtimeShape = { workingDate: "", teamId: "", taskId: "", comments: "", hours: "" };

createRoot(document.getElementById("root")!).render(<App />);
