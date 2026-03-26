export function StatsPanel({ steps, listSize, status }) {
  const statusColor = {
    idle: 'var(--text-muted)',
    running: 'var(--accent2)',
    done: 'var(--success)',
  }[status];

  return (
    <div style={{
      display: 'flex',
      gap: '24px',
      fontFamily: 'var(--font-mono)',
      fontSize: '13px',
    }}>
      <Stat label="STEPS" value={steps} />
      <Stat label="SIZE" value={listSize} />
      <div style={{ display: 'flex', flexDirection: 'column', gap: '4px' }}>
        <span style={{ color: 'var(--text-muted)', fontSize: '11px', letterSpacing: '0.1em' }}>STATUS</span>
        <span style={{ color: statusColor, fontWeight: 700, textTransform: 'uppercase' }}>{status}</span>
      </div>
    </div>
  );
}

function Stat({ label, value }) {
  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '4px' }}>
      <span style={{ color: 'var(--text-muted)', fontSize: '11px', letterSpacing: '0.1em' }}>{label}</span>
      <span style={{ color: 'var(--text)', fontWeight: 700, fontSize: '20px' }}>{value}</span>
    </div>
  );
}