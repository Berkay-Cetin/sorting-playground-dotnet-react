export function SortBar({ value, max, highlighted, done }) {
  const heightPct = Math.max(4, (value / max) * 100);

  const color = done
    ? 'var(--bar-done)'
    : highlighted
    ? 'var(--bar-hi)'
    : 'var(--bar-base)';

  return (
    <div style={{
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'flex-end',
      gap: '4px',
      flex: 1,
      height: '100%',
    }}>
      <span style={{
        fontSize: '10px',
        fontFamily: 'var(--font-mono)',
        color: highlighted ? 'var(--bar-hi)' : 'var(--text-muted)',
        transition: 'color 0.2s',
        minHeight: '16px',
      }}>
        {value}
      </span>
      <div style={{
        width: '100%',
        height: `${heightPct}%`,
        background: color,
        borderRadius: '4px 4px 0 0',
        transition: 'height 0.15s cubic-bezier(0.4,0,0.2,1), background 0.2s ease',
        boxShadow: highlighted ? `0 0 12px ${color}99` : 'none',
      }} />
    </div>
  );
}